using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using VitalsService.DataAccess.EF;
using VitalsService.DataAccess.EF.Repositories;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Helpers;

namespace VitalsService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// ThresholdsService.
    /// </summary>
    public class ThresholdsService : IThresholdsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<PatientThreshold> thresholdRepository;
        private readonly IRepository<AlertSeverity> alertSeverityRepository;
        private readonly IRepository<VitalAlert> vitalAlertsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThresholdsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public ThresholdsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.thresholdRepository = unitOfWork.CreateRepository<PatientThreshold>();
            this.alertSeverityRepository = unitOfWork.CreateRepository<AlertSeverity>();
            this.vitalAlertsRepository = unitOfWork.CreateRepository<VitalAlert>();
        }

        /// <summary>
        /// Creates new measurment with a set of vitals.
        /// </summary>
        /// <param name="threshold">The threshold.</param>
        /// <returns>
        /// Created entity.
        /// </returns>
        public async Task<OperationResultDto<Guid, CreateUpdateThresholdStatus>> CreateThreshold(PatientThreshold threshold)
        {
            var existingAlertSeverities = await alertSeverityRepository
                .FindAsync(a => a.CustomerId == threshold.CustomerId);

            if (threshold.AlertSeverityId.HasValue)
            {
                var existingAlertSeverity =
                    existingAlertSeverities.SingleOrDefault(a => a.Id == threshold.AlertSeverityId.Value);

                if (existingAlertSeverity == null)
                {
                    return new OperationResultDto<Guid, CreateUpdateThresholdStatus>()
                    {
                        Status = CreateUpdateThresholdStatus.AlertSeverityDoesNotExist
                    };
                }
            }
            else
            {
                if (existingAlertSeverities.Any())
                {
                    return new OperationResultDto<Guid, CreateUpdateThresholdStatus>()
                    {
                        Status = CreateUpdateThresholdStatus.ExistingAlertSeverityShouldBeUsed
                    };
                }
            }

            var existingPatientVitalThresholds = await thresholdRepository
                .FindAsync(t => 
                    t.CustomerId == threshold.CustomerId &&
                    t.PatientId == threshold.PatientId &&
                    t.Name.ToLower() == threshold.Name.ToLower() &&
                    t.AlertSeverityId == threshold.AlertSeverityId
                );

            if (existingPatientVitalThresholds.Any())
            {
                return new OperationResultDto<Guid, CreateUpdateThresholdStatus>()
                {
                    Status = CreateUpdateThresholdStatus.VitalThresholdAlreadyExists
                };
            }

            thresholdRepository.Insert(threshold);
            await unitOfWork.SaveAsync();

            return await Task.FromResult(
                new OperationResultDto<Guid, CreateUpdateThresholdStatus>()
                {
                    Status = CreateUpdateThresholdStatus.Success,
                    Content = threshold.Id
                }
            );
        }

        /// <summary>
        /// Updates the threshold.
        /// </summary>
        /// <param name="threshold">The threshold.</param>
        /// <returns></returns>
        public async Task<CreateUpdateThresholdStatus> UpdateThreshold(PatientThreshold threshold)
        {
            var existingPatientThresholds = await thresholdRepository
                .FindAsync(t =>
                    t.CustomerId == threshold.CustomerId &&
                    t.PatientId == threshold.PatientId
                );

            var requestedThreshold = existingPatientThresholds.SingleOrDefault(t => t.Id == threshold.Id);

            if (requestedThreshold == null)
            {
                return await Task.FromResult(CreateUpdateThresholdStatus.VitalThresholdDoesNotExist);
            }

            var existingAlertSeverities = await alertSeverityRepository
                .FindAsync(a => a.CustomerId == threshold.CustomerId);

            if (threshold.AlertSeverityId.HasValue)
            {
                var existingAlertSeverity =
                    existingAlertSeverities.SingleOrDefault(a => a.Id == threshold.AlertSeverityId.Value);

                if (existingAlertSeverity == null)
                {
                    return await Task.FromResult(CreateUpdateThresholdStatus.AlertSeverityDoesNotExist);
                }
            }
            else
            {
                if (existingAlertSeverities.Any())
                {
                    return await Task.FromResult(CreateUpdateThresholdStatus.ExistingAlertSeverityShouldBeUsed);
                }
            }

            if (
                requestedThreshold.Name.ToLower() != threshold.Name.ToLower() ||
                requestedThreshold.AlertSeverityId != threshold.AlertSeverityId
            )
            {
                var conflictingPatientThresholds = existingPatientThresholds
                    .Where(t => 
                        t.Id != threshold.Id &&
                        t.Name.ToLower() == threshold.Name.ToLower() &&
                        t.AlertSeverityId == threshold.AlertSeverityId
                    )
                    .ToList();

                if (conflictingPatientThresholds.Any())
                {
                    return await Task.FromResult(CreateUpdateThresholdStatus.VitalThresholdAlreadyExists);
                }
            }

            Mapper.Map(threshold, requestedThreshold, typeof(PatientThreshold), typeof(PatientThreshold));

            thresholdRepository.Update(requestedThreshold);
            await unitOfWork.SaveAsync();

            return CreateUpdateThresholdStatus.Success;
        }

        /// <summary>
        /// Deletes the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <returns></returns>
        public async Task<GetDeleteThresholdStatus> DeleteThreshold(int customerId, Guid patientId, Guid thresholdId)
        {
            var existingPatientVitalThreshold =
                await GetThreshold(customerId, patientId, thresholdId);

            if (existingPatientVitalThreshold != null)
            {
                var vitalAlerts = existingPatientVitalThreshold.VitalAlerts.ToList();

                // Explicitly removing all Vital alerts and alert details dependent on this patient's threshold
                foreach (var vitalAlert in vitalAlerts)
                {
                    vitalAlertsRepository.Delete(vitalAlert);
                }

                thresholdRepository.Delete(existingPatientVitalThreshold);

                await unitOfWork.SaveAsync();

                return await Task.FromResult(GetDeleteThresholdStatus.Success);
            }

            return await Task.FromResult(GetDeleteThresholdStatus.NotFound);
        }

        /// <summary>
        /// Gets the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <returns></returns>
        public async Task<PatientThreshold> GetThreshold(
            int customerId,
            Guid patientId,
            Guid thresholdId
        )
        {
            return (await thresholdRepository
                .FindAsync(
                    t => t.CustomerId == customerId &&
                        t.PatientId == patientId &&
                        t.Id == thresholdId,
                    o => o.OrderBy(e => e.Id),
                    new List<Expression<Func<PatientThreshold, object>>>
                    {
                        e => e.VitalAlerts,
                        e => e.AlertSeverity
                    }
                )
            ).SingleOrDefault();
        }

        /// <summary>
        /// Gets the thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResult<PatientThreshold>> GetThresholds(int customerId, Guid patientId, BaseSearchDto request)
        {
            Expression<Func<PatientThreshold, bool>> expression = t => t.CustomerId == customerId && t.PatientId == patientId;

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(t => t.Name.Contains(term));
                    }
                }
            }

            return await thresholdRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderBy(e => e.Id),
                    new List<Expression<Func<PatientThreshold, object>>>
                    {
                        e => e.VitalAlerts,
                        e => e.AlertSeverity
                    },
                    request != null ? request.Skip : (int?)null,
                    request != null ? request.Take : (int?)null
                );
        }
    }
}