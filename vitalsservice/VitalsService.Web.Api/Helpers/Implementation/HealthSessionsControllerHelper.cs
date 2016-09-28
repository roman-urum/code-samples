using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NLog;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Extensions;
using VitalsService.Helpers;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.HealthSessions;

namespace VitalsService.Web.Api.Helpers.Implementation
{
    /// <summary>
    /// Contains health sessions models mapping and presentation
    /// layer logic.
    /// </summary>
    public class HealthSessionsControllerHelper : IHealthSessionsControllerHelper
    {
        private readonly IHealthSessionsService healthSessionsService;
        private readonly IMeasurementsService measurementsService;
        private readonly IEsb esbService;
        private readonly IAlertSeveritiesService alertSeveritiesService;
        private readonly IAlertsService alertsService;
        private readonly IThresholdAggregator thresholdAggregator;
        private readonly IDefaultThresholdsService defaultThresholdsService;
        private readonly IThresholdsService thresholdsService;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthSessionsControllerHelper" /> class.
        /// </summary>
        /// <param name="healthSessionsService">The health sessions service.</param>
        /// <param name="measurementsService">The measurements service.</param>
        /// <param name="esbService">The esb service.</param>
        /// <param name="alertSeveritiesService">The alert severities service.</param>
        /// <param name="alertsService">The alerts service.</param>
        /// <param name="thresholdsService">The thresholds service.</param>
        /// <param name="defaultThresholdsService">The default thresholds service.</param>
        /// <param name="thresholdAggregator">The threshold aggregator.</param>
        public HealthSessionsControllerHelper(
            IHealthSessionsService healthSessionsService,
            IMeasurementsService measurementsService,
            IEsb esbService,
            IAlertSeveritiesService alertSeveritiesService,
            IAlertsService alertsService,
            IThresholdsService thresholdsService,
            IDefaultThresholdsService defaultThresholdsService,
            IThresholdAggregator thresholdAggregator
        )
        {
            this.healthSessionsService = healthSessionsService;
            this.measurementsService = measurementsService;
            this.esbService = esbService;
            this.alertSeveritiesService = alertSeveritiesService;
            this.alertsService = alertsService;
            this.thresholdAggregator = thresholdAggregator;
            this.defaultThresholdsService = defaultThresholdsService;
            this.thresholdsService = thresholdsService;
        }

        /// <summary>
        /// Creates entity from request model and save data.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<OperationResultDto<PostResponseDto<Guid>, CreateHealthSessionStatus>>  Create(
            HealthSessionRequestDto model,
            int customerId, 
            Guid patientId
        )
        {
            var isHealthSessionClientIdValid = await ValidateHealthSessionClientId(customerId, patientId, model);

            if (!isHealthSessionClientIdValid)
            {
                return new OperationResultDto<PostResponseDto<Guid>, CreateHealthSessionStatus>(
                    CreateHealthSessionStatus.HealthSessionWithClientIdAlreadyExists);
            }

            var healthSession = Mapper.Map<HealthSession>(model);
            healthSession.CustomerId = customerId;
            healthSession.PatientId = patientId;

            var buildHealthSessionElementsResult = await BuildHealthSessionElements(
                customerId,
                patientId,
                model.Elements,
                healthSession);

            if (buildHealthSessionElementsResult.Status != CreateHealthSessionStatus.Success)
            {
                return new OperationResultDto<PostResponseDto<Guid>, CreateHealthSessionStatus>(
                    buildHealthSessionElementsResult.Status
                );
            }

            healthSession.Elements.AddRange(buildHealthSessionElementsResult.Content);

            var result = await this.healthSessionsService.Create(healthSession);

            if (result.Status != CreateHealthSessionStatus.Success)
            {
                return new OperationResultDto<PostResponseDto<Guid>, CreateHealthSessionStatus>(result.Status);
            }

            try
            {
                var esbModel = Mapper.Map<HealthSessionEsbDto>(model);
                esbModel.Id = result.Content.Id;
                esbModel.CustomerId = customerId;
                esbModel.PatientId = patientId;
                esbModel.Elements.Each(ResetRawJson);

                await this.esbService.PublishHealthSession(esbModel);
            }
            catch (Exception e)
            {
                logger.Error(e, "An error occured when try to publish health session to esb");                
            }

            var resultModel = new PostResponseDto<Guid>
            {
                Id = result.Content.Id
            };

            return new OperationResultDto<PostResponseDto<Guid>, CreateHealthSessionStatus>()
            {
                Content = resultModel,
                Status = CreateHealthSessionStatus.Success
            };
        }

        /// <summary>
        /// Builds the list of health session elements.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="healthSessionElementDtos">The health session elements dtos.</param>
        /// <param name="healthSession">The health session instance.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<IList<HealthSessionElement>, CreateHealthSessionStatus>> 
            BuildHealthSessionElements(
                int customerId,
                Guid patientId,
                IList<HealthSessionElementRequestDto> healthSessionElementDtos,
                HealthSession healthSession)
        {
            var resultHealthSessionElements = new List<HealthSessionElement>();
            var customerAlertSeverities = (await alertSeveritiesService
                            .GetAlertSeverities(customerId, new AlertSeveritiesSearchDto() { Take = int.MaxValue }))
                            .Results;

            foreach (var elementModel in healthSessionElementDtos)
            {
                var hlElement = await InitSessionElement(elementModel, customerId, patientId);

                if (elementModel.Alert && hlElement.Values.Any(el => el.Type == HealthSessionElementValueType.SelectionAnswer ||
                                                                     el.Type == HealthSessionElementValueType.ScaleAnswer))
                {
                    if (elementModel.AlertSeverityId.HasValue)
                    {
                        if (customerAlertSeverities.Any())
                        {
                            var requestedAlertSeverity = customerAlertSeverities.FirstOrDefault(s => s.Id == elementModel.AlertSeverityId.Value);

                            if (requestedAlertSeverity == null)
                            {
                                // Using Highest severity
                                elementModel.AlertSeverityId = customerAlertSeverities.OrderBy(s => s.Severity).Last().Id;
                            }
                        }
                        else
                        {
                            // Ignoring provided invalid alert severity
                            resultHealthSessionElements.Add(hlElement);

                            continue;
                        }
                    }
                    else
                    {
                        if (customerAlertSeverities.Any())
                        {
                            // Using Highest severity
                            elementModel.AlertSeverityId = customerAlertSeverities.OrderBy(s => s.Severity).Last().Id;
                        }
                    }

                    var answersText = hlElement
                        .Values
                        .Select(v => v is SelectionAnswer ? ((SelectionAnswer)v).Text : ((ScaleAnswer)v).Value.ToString())
                        .Aggregate((s1, s2) => string.Format("{0}, {1}", s1, s2));

                    var alertTitle = string.Format("{0} {1}", elementModel.Text, answersText);

                    hlElement.HealthSessionElementAlert = 
                        new HealthSessionElementAlert()
                        {
                            Id = SequentialGuidGenerator.Generate(),
                            HealthSessionElement = hlElement,
                            CustomerId = customerId,
                            PatientId = patientId,
                            Type = AlertType.ResponseViolation,
                            AlertSeverityId = elementModel.AlertSeverityId,
                            Title = alertTitle,
                            Body = string.Empty,
                            OccurredUtc = elementModel.AnsweredUtc ?? DateTime.MinValue,
                            ExpiresUtc = null,
                            Weight = 0
                        };
                }
                
                resultHealthSessionElements.Add(hlElement);
            }

            return new OperationResultDto<IList<HealthSessionElement>, CreateHealthSessionStatus>()
            {
                Status = CreateHealthSessionStatus.Success,
                Content = resultHealthSessionElements
            };
        }

        /// <summary>
        /// Returns all health sessions for patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="searchDto">The search dto.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<HealthSessionResponseDto>> Find(
            int customerId,
            Guid patientId,
            HealthSessionsSearchDto searchDto
        )
        {
            var healthSessions = await this.healthSessionsService.Find(customerId, patientId, searchDto);

            return Mapper.Map<PagedResultDto<HealthSessionResponseDto>>(healthSessions);
        }

        /// <summary>
        /// Returns health session with specified id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="healthSessionId"></param>
        /// <returns></returns>
        public async Task<OperationResultDto<HealthSessionResponseDto, GetHealthSessionStatus>> GetById(int customerId, Guid patientId, Guid healthSessionId)
        {
            var resultHealthSession = await this.healthSessionsService.GetById(customerId, patientId, healthSessionId);

            if (resultHealthSession == null)
            {
                return new OperationResultDto<HealthSessionResponseDto, GetHealthSessionStatus>()
                {
                    Status = GetHealthSessionStatus.HealthSessionNotFound,
                    Content = null
                };
            }

            return new OperationResultDto<HealthSessionResponseDto, GetHealthSessionStatus>()
            {
                Status = GetHealthSessionStatus.Success,
                Content = Mapper.Map<HealthSessionResponseDto>(resultHealthSession)
            };
        }

        #region private methods

        /// <summary>
        /// Initializes health session element by model.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        private async Task<HealthSessionElement> InitSessionElement(HealthSessionElementRequestDto model, int customerId, Guid patientId)
        {
            var elementEntity = Mapper.Map<HealthSessionElementRequestDto, HealthSessionElement>(model);

            if (model.Values == null)
            {
                return elementEntity;
            }

            foreach (var elementValueModel in model.Values)
            {
                var measurementValueModel = elementValueModel as MeasurementValueRequestDto;

                if (measurementValueModel == null)
                {
                    elementEntity.Values.Add(Mapper.Map<HealthSessionElementValueDto, HealthSessionElementValue>(elementValueModel));
                }
                else
                {
                    elementEntity.Values.Add(
                        await CreateMeasurementValue(measurementValueModel.Value, customerId, patientId));
                }
            }

            return elementEntity;
        }

        /// <summary>
        /// Creates new measurement using model.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        private async Task<MeasurementValue> CreateMeasurementValue(MeasurementRequestDto model, int customerId, Guid patientId)
        {
            var measurement = Mapper.Map<MeasurementRequestDto, Measurement>(model);
            measurement.CustomerId = customerId;
            measurement.PatientId = patientId;
            var defaultThresholds = (await defaultThresholdsService.GetDefaultThresholds(customerId, null)).Results;
            var patientThresholds = (await thresholdsService.GetThresholds(customerId,patientId, null)).Results;
            var aggregatedThresholds = thresholdAggregator.AggregateThresholds(defaultThresholds, patientThresholds);

            alertsService.CreateViolationAlerts(measurement, aggregatedThresholds);

            var createdMeasurement = await measurementsService.Create(measurement, model.RawJson, false);

            return new MeasurementValue
            {
                Measurement = createdMeasurement,
                Type = HealthSessionElementValueType.MeasurementAnswer
            };
        }

        /// <summary>
        /// Resets RawJson for measurement element if exists.
        /// </summary>
        /// <param name="model"></param>
        private static void ResetRawJson(HealthSessionElementRequestDto model)
        {
            if (model.Values == null)
            {
                return;
            }

            model.Values.Each(val =>
            {
                var measurementVal = val as MeasurementValueRequestDto;

                if (measurementVal != null)
                {
                    measurementVal.Value.RawJson = null;
                }
            });
        }

        /// <summary>
        /// Uppdate the health session.
        /// </summary>
        /// <param name="model">The update model.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="healthSessionId">The health session identifier to be updated</param>
        /// <returns></returns>
        public async Task<UpdateHealthSessionStatus> Update(UpdateHealthSessionRequestDto model, int customerId, Guid patientId, Guid healthSessionId)
        {
            var existedHealthSession = await healthSessionsService.GetById(customerId, patientId, healthSessionId);

            if (existedHealthSession == null)
            {
                return UpdateHealthSessionStatus.HealthSessionNotFound;
            }

            existedHealthSession.IsIncomplete = model.IsIncomplete;
            existedHealthSession.CompletedUtc = model.CompletedUtc ?? existedHealthSession.CompletedUtc;
            
            var buildHealthSessionElementsResult = await BuildHealthSessionElements(
                customerId,
                patientId,
                model.Elements,
                existedHealthSession);

            if (buildHealthSessionElementsResult.Status != CreateHealthSessionStatus.Success)
            {
                return (UpdateHealthSessionStatus)buildHealthSessionElementsResult.Status;
            }

            existedHealthSession.Elements.AddRange(buildHealthSessionElementsResult.Content);

            await healthSessionsService.Update(existedHealthSession);

            try
            {
                var esbModel = Mapper.Map<HealthSessionEsbDto>(existedHealthSession);
                esbModel.Elements = model.Elements;
                esbModel.Elements.Each(ResetRawJson);
                await this.esbService.PublishHealthSession(esbModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occured when try to publish health session to Esb.");
            }

            return UpdateHealthSessionStatus.Success;
        }

        private async Task<bool> ValidateHealthSessionClientId(int customerId, Guid patientId, HealthSessionRequestDto request)
        {
            if (!string.IsNullOrEmpty(request.ClientId))
            {
                var existingHealthSession = (await healthSessionsService.Find(
                    customerId, 
                    patientId,
                    new HealthSessionsSearchDto()
                    {
                        Skip = 0,
                        Take = int.MaxValue,
                        IncludePrivate = true
                    },
                    request.ClientId
                ))
                .Results
                .FirstOrDefault();

                if (existingHealthSession != null)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}