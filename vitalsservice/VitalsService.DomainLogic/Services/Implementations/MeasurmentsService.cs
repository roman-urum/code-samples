using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VitalsService.DataAccess.Document;
using VitalsService.DataAccess.Document.Repository;
using VitalsService.DataAccess.EF;
using VitalsService.DataAccess.EF.Repositories;
using VitalsService.Domain.DbEntities;
using VitalsService.DomainLogic.Services.Interfaces;
using AutoMapper;

using NLog;

using VitalsService.Domain.DocumentDb;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Domain.EsbEntities;
using VitalsService.Helpers;

namespace VitalsService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Provides methods to manage measurments and vitals.
    /// </summary>
    public class MeasurmentsService : IMeasurementsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Measurement> measurementRepository;
        private readonly IDocumentDbRepository<RawMeasurement> rawMeasurementRepository;
        private readonly IEsb esb;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurmentsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="documentRepositoryFactory">The document repository factory.</param>
        /// <param name="esb">The esb.</param>
        public MeasurmentsService(
            IUnitOfWork unitOfWork,
            IDocumentRepositoryFactory documentRepositoryFactory,
            IEsb esb
        )
        {
            this.unitOfWork = unitOfWork;
            this.measurementRepository = unitOfWork.CreateRepository<Measurement>();
            this.rawMeasurementRepository = documentRepositoryFactory.Create<RawMeasurement>(VitalsServiceCollection.RawMeasurment);
            this.esb = esb;
        }

        /// <summary>
        /// Creates new measurment with a set of vitals.
        /// </summary>
        /// <param name="measurement"></param>
        /// <param name="rawData">Raw data received from peripheral</param>
        /// <param name="sendMeasurementToEsb">bool flag specifies measurement should be sent to esb or not</param>
        /// <returns>
        /// Created entity.
        /// </returns>
        public async Task<Measurement> Create(Measurement measurement, object rawData, bool sendMeasurementToEsb)
        {
            measurementRepository.Insert(measurement);

            await unitOfWork.SaveAsync();

            var rawMeasurement = new RawMeasurement
            {
                Id = measurement.Id,
                CustomerId = measurement.CustomerId,
                PatientId = measurement.PatientId,
                Observed = measurement.ObservedUtc,
                RawJson = rawData
            };

            await rawMeasurementRepository.CreateItemAsync(rawMeasurement);

            if (sendMeasurementToEsb)
            {
                try
                {
                    var measurementMessage = Mapper.Map<Measurement, MeasurementMessage>(measurement);
                    await esb.PublishMeasurement(measurementMessage);
                }
                catch (Exception e)
                {
                    logger.Error(e, "An error occured when try to publish measurement to Esb");
                }
            }

            return measurement;
        }

        /// <summary>
        /// Updates measurement data.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="measurement">The measurement.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Measurement, UpdateMeasurementStatus>> Update(int customerId, Measurement measurement)
        {
            var existingMeasurement = await GetById(customerId, measurement.PatientId, measurement.Id);

            if (existingMeasurement == null)
            {
                return new OperationResultDto<Measurement, UpdateMeasurementStatus>()
                {
                    Content = null,
                    Status = UpdateMeasurementStatus.MeasurementNotFound
                };
            }

            existingMeasurement.IsInvalidated = measurement.IsInvalidated;            

            measurementRepository.Update(existingMeasurement);
            await unitOfWork.SaveAsync();

            return new OperationResultDto<Measurement, UpdateMeasurementStatus>()
            {
                Content = existingMeasurement,
                Status = UpdateMeasurementStatus.Success
            };
        }

        /// <summary>
        /// Returs patients with appropriate search criteria.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="filterMeasurement">The filter.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public async Task<PagedResult<Measurement>> Search(
            int customerId, 
            Guid patientId, 
            MeasurementsSearchDto filterMeasurement,
            string clientId = null
        )
        {
            Expression<Func<Measurement, bool>> expression = m => m.PatientId == patientId && m.CustomerId == customerId;

            if (filterMeasurement != null)
            {
                if (!string.IsNullOrEmpty(filterMeasurement.Q))
                {
                    var terms = filterMeasurement.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(m => m.Vitals.Any(v => v.Name.Contains(term)));
                    }
                }

                if (filterMeasurement.ObservedFrom.HasValue)
                {
                    expression = expression
                        .And(m => m.ObservedUtc >= filterMeasurement.ObservedFrom.Value);
                }

                if (filterMeasurement.ObservedTo.HasValue)
                {
                    expression = expression
                        .And(m => m.ObservedUtc <= filterMeasurement.ObservedTo.Value);
                }

                if (filterMeasurement.IsInvalidated.HasValue)
                {
                    expression = expression
                        .And(m => m.IsInvalidated == filterMeasurement.IsInvalidated.Value);
                }

                if (!string.IsNullOrEmpty(filterMeasurement.ObservedTz))
                {
                    expression = expression.And(m => m.ObservedTz.ToLower() == filterMeasurement.ObservedTz.ToLower());
                }
            }

            if (!string.IsNullOrEmpty(clientId))
            {
                expression = expression.And(hs => hs.ClientId.ToLower() == clientId.ToLower());
            }

            var result = await measurementRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderBy(e => e.Id),
                    new List<Expression<Func<Measurement, object>>>
                    {
                        m => m.MeasurementNotes,
                        m => m.Vitals,
                        m => m.Vitals.Select(v => v.VitalAlert.Threshold),
                        m => m.Vitals.Select(v => v.VitalAlert.AlertSeverity),
                        m => m.Device
                    },
                    filterMeasurement != null ? filterMeasurement.Skip : (int?)null,
                    filterMeasurement != null ? filterMeasurement.Take : (int?)null
                );

            return result;
        }

        /// <summary>
        /// Returns vital by id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="measurementId">The measurement identifier.</param>
        /// <returns>
        /// Updated entity.
        /// </returns>
        public async Task<Measurement> GetById(int customerId, Guid patientId, Guid measurementId)
        {
            var includes = new List<Expression<Func<Measurement, object>>>
            {
                m => m.MeasurementNotes,
                m => m.Vitals,
                m => m.Vitals.Select(v => v.VitalAlert.Threshold),
                m => m.Vitals.Select(v => v.VitalAlert.AlertSeverity),
                m => m.Device
            };

            var result = await this.measurementRepository
                .FindAsync(
                    m => m.CustomerId == customerId && m.PatientId == patientId && m.Id == measurementId,
                    null,
                    includes
                );

            return result.FirstOrDefault();
        }
    }
}