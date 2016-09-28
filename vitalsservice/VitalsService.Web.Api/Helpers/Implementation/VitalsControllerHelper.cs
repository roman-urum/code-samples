using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;

namespace VitalsService.Web.Api.Helpers.Implementation
{
    /// <summary>
    /// VitalsControllerHelper.
    /// </summary>
    public class VitalsControllerHelper : IVitalsControllerHelper
    {
        private readonly IMeasurementsService measurementsService;
        private readonly IAlertsService alertsService;
        private readonly IThresholdAggregator thresholdAggregator;
        private readonly IDefaultThresholdsService defaultThresholdsService;
        private readonly IThresholdsService thresholdsService;
        private readonly IPatientConditionsService patientsConditionsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VitalsControllerHelper" /> class.
        /// </summary>
        /// <param name="measurementsService">The measurements service.</param>
        /// <param name="thresholdsService">The thresholds service.</param>
        /// <param name="defaultThresholdsService">The default thresholds service.</param>
        /// <param name="thresholdAggregator">The threshold aggregator.</param>
        /// <param name="alertsService">The alerts service.</param>
        /// <param name="patientsConditionsService">The patients condition service.</param>
        public VitalsControllerHelper(
            IMeasurementsService measurementsService,
            IThresholdsService thresholdsService,
            IDefaultThresholdsService defaultThresholdsService,
            IThresholdAggregator thresholdAggregator,
            IAlertsService alertsService,
            IPatientConditionsService patientsConditionsService
        )
        {
            this.measurementsService = measurementsService;
            this.thresholdsService = thresholdsService;
            this.defaultThresholdsService = defaultThresholdsService;
            this.thresholdsService = thresholdsService;
            this.thresholdAggregator = thresholdAggregator;
            this.alertsService = alertsService;
            this.patientsConditionsService = patientsConditionsService;
        }

        /// <summary>
        /// Gets the vitals.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<MeasurementResponseDto>> GetVitals(
            int customerId, 
            Guid patientId,
            MeasurementsSearchDto request
        )
        {
            var result = await measurementsService.Search(customerId, patientId, request);

           return Mapper.Map<PagedResult<Measurement>, PagedResultDto<MeasurementResponseDto>>(result);
        }

        /// <summary>
        /// Gets the vital.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="measurementId">The measurement identifier.</param>
        /// <returns></returns>
        public async Task<MeasurementResponseDto> GetVital(int customerId, Guid patientId, Guid measurementId)
        {
            var vital = await measurementsService.GetById(customerId, patientId, measurementId);

            return Mapper.Map<Measurement, MeasurementResponseDto>(vital);
        }

        /// <summary>
        /// Creates the vital.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<PostResponseDto<Guid>, CreateMeasurementStatus>> CreateVital(int customerId, Guid patientId, MeasurementRequestDto request)
        {
            var isMeasurementClientIdValid = await ValidateMeasurementClientId(customerId, patientId, request);

            if (!isMeasurementClientIdValid)
            {
                return new OperationResultDto<PostResponseDto<Guid>, CreateMeasurementStatus>(
                    CreateMeasurementStatus.MeasurementWithClientIdAlreadyExists
                );
            }

            var measurement = Mapper.Map<Measurement>(request);
            measurement.CustomerId = customerId;
            measurement.PatientId = patientId;

            var patientConditions = await patientsConditionsService.GetPatientConditions(customerId, patientId);
            var patientConditionIds = patientConditions.Select(pc => pc.Id).ToList();

            List<DefaultThreshold> conditionDefaultThresholds = new List<DefaultThreshold>();
            if (patientConditionIds.Any())
            {
                conditionDefaultThresholds = (await defaultThresholdsService.GetDefaultThresholds(customerId, new DefaultThresholdsSearchDto()
                {
                    DefaultType = ThresholdDefaultType.Condition,
                    ConditionIds = patientConditionIds.ToList()
                })).Results.ToList();
            }

            var customerDefaultThresholds = (await defaultThresholdsService.GetDefaultThresholds(customerId, new DefaultThresholdsSearchDto()
            {
                DefaultType = ThresholdDefaultType.Customer
            })).Results;

            var defaultThresholds = new List<DefaultThreshold>(customerDefaultThresholds);
            defaultThresholds.AddRange(conditionDefaultThresholds);
            
            var patientThresholds = (await thresholdsService.GetThresholds(customerId, patientId, null)).Results;

            var aggregatedThresholds = thresholdAggregator.AggregateThresholds(defaultThresholds, patientThresholds);

            alertsService.CreateViolationAlerts(measurement, aggregatedThresholds);

            var result = await measurementsService.Create(measurement, request.RawJson, true);

            return new OperationResultDto<PostResponseDto<Guid>, CreateMeasurementStatus>(
                CreateMeasurementStatus.Success,
                new PostResponseDto<Guid>()
                {
                    Id = result.Id
                }
            );
        }

        /// <summary>
        /// Updates the vital.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="measurementId">The measurement identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<UpdateMeasurementStatus> UpdateVital(
            int customerId,
            Guid patientId,
            Guid measurementId, 
            UpdateMeasurementRequestDto request
        )
        {
            var entity = Mapper.Map<Measurement>(request);
            entity.PatientId = patientId;
            entity.CustomerId = customerId;
            entity.Id = measurementId;

            return (await measurementsService.Update(customerId, entity)).Status;
        }

        private async Task<bool> ValidateMeasurementClientId(int customerId, Guid patientId, MeasurementRequestDto request)
        {
            if (!string.IsNullOrEmpty(request.ClientId))
            {
                var existingMeasurement = (await measurementsService.Search(
                    customerId,
                    patientId,
                    new MeasurementsSearchDto()
                    {
                        Skip = 0,
                        Take = int.MaxValue
                    },
                    request.ClientId
                ))
                .Results
                .FirstOrDefault();

                if (existingMeasurement != null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}