using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using NLog;

using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Dtos.MessagingHub;
using VitalsService.Domain.Enums;
using VitalsService.Domain.Enums.MessagingHub;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Thresholds;

namespace VitalsService.Web.Api.Helpers.Implementation
{
    /// <summary>
    /// ThresholdsControllerHelper.
    /// </summary>
    public class ThresholdsControllerHelper : IThresholdsControllerHelper
    {
        private readonly IThresholdsService thresholdsService;
        private readonly IDefaultThresholdsService defaultThresholdsService;
        private readonly IAlertSeveritiesService alertSeveritiesService;
        private readonly IThresholdAggregator thresholdAggregator;
        private readonly IMessagingHubService messagingHubService;
        private readonly IPatientConditionsService patientsConditionsService;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ThresholdsControllerHelper" /> class.
        /// </summary>
        /// <param name="thresholdsService">The thresholds service.</param>
        /// <param name="defaultThresholdsService">The default thresholds service.</param>
        /// <param name="alertSeveritiesService">The alert severities service.</param>
        /// <param name="thresholdAggregator"></param>
        /// <param name="messagingHubService"></param>
        /// <param name="patientsConditionsService"></param>
        public ThresholdsControllerHelper(
            IThresholdsService thresholdsService,
            IDefaultThresholdsService defaultThresholdsService,
            IAlertSeveritiesService alertSeveritiesService,
            IThresholdAggregator thresholdAggregator,
            IMessagingHubService messagingHubService,
            IPatientConditionsService patientsConditionsService
        )
        {
            this.thresholdsService = thresholdsService;
            this.defaultThresholdsService = defaultThresholdsService;
            this.alertSeveritiesService = alertSeveritiesService;
            this.thresholdAggregator = thresholdAggregator;
            this.messagingHubService = messagingHubService;
            this.patientsConditionsService = patientsConditionsService;
        }

        /// <summary>
        /// Creates the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, CreateUpdateThresholdStatus>> 
            CreateThreshold(int customerId, Guid patientId, ThresholdRequestDto request)
        {
            var threshold = Mapper.Map<ThresholdRequestDto, PatientThreshold>(request);
            threshold.CustomerId = customerId;
            threshold.PatientId = patientId;

            var result = await thresholdsService.CreateThreshold(threshold);

            if (result.Status == CreateUpdateThresholdStatus.Success)
            {
                await messagingHubService.SendPushNotification(new NotificationDto()
                {
                    AllTags = true,
                    Data = new
                    {
                        PatientDeviceNotification = new
                        {
                            Action = "PatientThresholdsChanged",
                            CustmerId = threshold.CustomerId,
                            PatientId = threshold.Id
                        }
                    },
                    Message = null,
                    Sender = string.Format("maestro-customer-{0}", threshold.CustomerId),
                    Tags = new List<string> { string.Format("maestro-customer-{0}", threshold.CustomerId),
                                              string.Format("maestro-patientId-{0}", threshold.PatientId)
                                            },
                    Types = new[] { RegistrationType.APN, RegistrationType.GCM }
                });
            }

            return result;
        }

        /// <summary>
        /// Updates the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<CreateUpdateThresholdStatus> UpdateThreshold(
            int customerId,
            Guid patientId,
            Guid thresholdId,
            ThresholdRequestDto request
        )
        {
            var threshold = Mapper.Map<ThresholdRequestDto, PatientThreshold>(request);
            threshold.Id = thresholdId;
            threshold.CustomerId = customerId;
            threshold.PatientId = patientId;

            var result = await thresholdsService.UpdateThreshold(threshold);

            if (result == CreateUpdateThresholdStatus.Success)
            {
                try
                {
                    await messagingHubService.SendPushNotification(new NotificationDto()
                    {
                        AllTags = true,
                        Data = new
                        {
                            PatientDeviceNotification = new
                            {
                                Action = "PatientThresholdsChanged",
                                CustmerId = threshold.CustomerId,
                                PatientId = threshold.PatientId
                            }
                        },
                        Message = null,
                        Sender = string.Format("maestro-customer-{0}", threshold.CustomerId),
                        Tags = new List<string> { string.Format("maestro-customer-{0}", threshold.CustomerId),
                                                  string.Format("maestro-patientId-{0}", threshold.PatientId)
                                                },
                        Types = new[] { RegistrationType.APN, RegistrationType.GCM }
                    });                    
                }
                catch (Exception ex)
                { 
                    logger.Error(ex, "An error occured when try to SendPushNotification: PatientThresholdsChanged");
                }

            }

            return result;
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
            var deleteThresholdResult = await thresholdsService.DeleteThreshold(customerId, patientId, thresholdId);

            if (deleteThresholdResult == GetDeleteThresholdStatus.Success)
            {
                await messagingHubService.SendPushNotification(new NotificationDto()
                {
                    AllTags = true,
                    Data = new
                    {
                        PatientDeviceNotification = new
                        {
                            Action = "PatientThresholdsChanged",
                            CustmerId = customerId,
                            PatientId = patientId
                        }
                    },
                    Message = null,
                    Sender = string.Format("maestro-customer-{0}", customerId),
                    Tags = new List<string> { string.Format("maestro-customer-{0}", customerId),
                                              string.Format("maestro-patientId-{0}", patientId)
                                            },
                    Types = new[] { RegistrationType.APN, RegistrationType.GCM }
                });
            }

            return deleteThresholdResult;
        }

        /// <summary>
        /// Gets the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<PatientThresholdDto, GetDeleteThresholdStatus>> 
            GetThreshold(int customerId, Guid patientId, Guid thresholdId)
        {
            var result = await thresholdsService.GetThreshold(customerId, patientId, thresholdId);

            if (result == null)
            {
                return await Task.FromResult(
                    new OperationResultDto<PatientThresholdDto, GetDeleteThresholdStatus>()
                    {
                        Status = GetDeleteThresholdStatus.NotFound
                    }
                );
            }

            return await Task.FromResult(
                new OperationResultDto<PatientThresholdDto, GetDeleteThresholdStatus>()
                {
                    Status = GetDeleteThresholdStatus.Success,
                    Content = Mapper.Map<PatientThreshold, PatientThresholdDto>(result)
                }
            );
        }

        /// <summary>
        /// Gets the thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<BaseThresholdDto>> GetThresholds(int customerId, Guid patientId, ThresholdsSearchDto request)
        {
            var customerAlertSeverities = (await alertSeveritiesService.GetAlertSeverities(customerId, null)).Results;

            if (request != null && request.Mode.HasValue)
            {
                switch (request.Mode.Value)
                {
                    case ThresholdSearchType.Patient:
                    {
                        var patientThresholdsPagedResult = await thresholdsService.GetThresholds(customerId, patientId, request);

                        var filteredPatientThresholds = FilterUnusedThresholds(
                            Mapper.Map<IList<PatientThreshold>, IList<PatientThresholdDto>>(patientThresholdsPagedResult.Results).Cast<BaseThresholdDto>().ToList(),
                            customerAlertSeverities
                        );

                        return new PagedResultDto<BaseThresholdDto>
                        {
                            Results = filteredPatientThresholds,
                            Total = patientThresholdsPagedResult.Total
                        };
                    }
                    case ThresholdSearchType.Defaults:
                    {
                        var customerDefaultThresholdsPagedResult = await defaultThresholdsService.GetDefaultThresholds(customerId, new DefaultThresholdsSearchDto()
                        {
                            DefaultType = ThresholdDefaultType.Customer,
                            Q = request.Q
                        });

                        var conditionDefaultThresholdsPagedResult = await defaultThresholdsService.GetDefaultThresholds(customerId, new DefaultThresholdsSearchDto()
                        {
                            DefaultType = ThresholdDefaultType.Condition,
                            ConditionIds = request.ConditionIds ?? new List<Guid>(),
                            Q = request.Q
                        });

                        var defaultThresholds = new List<DefaultThreshold>(customerDefaultThresholdsPagedResult.Results);
                        defaultThresholds.AddRange(conditionDefaultThresholdsPagedResult.Results);

                        var filteredDefaultThresholds = FilterUnusedThresholds(
                            Mapper.Map<IList<DefaultThreshold>, IList<DefaultThresholdDto>>(defaultThresholds).Cast<BaseThresholdDto>().ToList(),
                            customerAlertSeverities
                        );

                        return new PagedResultDto<BaseThresholdDto>
                        {
                            Results = filteredDefaultThresholds.Skip(request.Skip).Take(request.Take).ToList(),
                            Total = filteredDefaultThresholds.Count
                        };
                    }
                    case ThresholdSearchType.All:
                    {
                        var patientThresholdsPagedResult = await thresholdsService.GetThresholds(customerId, patientId, new BaseSearchDto
                        {
                            Q = request.Q
                        });

                        var customerDefaultThresholdsPagedResult = await defaultThresholdsService.GetDefaultThresholds(customerId, new DefaultThresholdsSearchDto()
                        {
                            DefaultType = ThresholdDefaultType.Customer,
                            Q = request.Q
                        });

                        var conditionsDefaultThresholdsPagedResult = await defaultThresholdsService.GetDefaultThresholds(customerId, new DefaultThresholdsSearchDto()
                        {
                            DefaultType = ThresholdDefaultType.Condition,
                            ConditionIds = request.ConditionIds ?? new List<Guid>(),
                            Q = request.Q
                        });

                        var allThresholds = new List<BaseThresholdDto>();
                        allThresholds.AddRange(Mapper.Map<IList<PatientThresholdDto>>(patientThresholdsPagedResult.Results));
                        allThresholds.AddRange(Mapper.Map<IList<DefaultThresholdDto>>(customerDefaultThresholdsPagedResult.Results));
                        allThresholds.AddRange(Mapper.Map<IList<DefaultThresholdDto>>(conditionsDefaultThresholdsPagedResult.Results));

                        var allFilteredThresholds = FilterUnusedThresholds(allThresholds, customerAlertSeverities);

                        return new PagedResultDto<BaseThresholdDto>
                        {
                            Results = allFilteredThresholds.Skip(request.Skip).Take(request.Take).ToList(),
                            Total = allFilteredThresholds.Count
                        };
                    }
                    case ThresholdSearchType.Aggregate:
                    {
                        var patientThresholdsPagedResult = await thresholdsService.GetThresholds(customerId, patientId, new BaseSearchDto
                        {
                            Q = request.Q
                        });

                        var patientConditions = await patientsConditionsService.GetPatientConditions(customerId, patientId);
                        var patientConditionIds = patientConditions.Select(pc => pc.Id).ToList();

                        var conditionDefaultThresholdsPagedResult = new PagedResult<DefaultThreshold>();
                        if (patientConditionIds.Any())
                        {
                            conditionDefaultThresholdsPagedResult = await defaultThresholdsService.GetDefaultThresholds(customerId, new DefaultThresholdsSearchDto()
                            {
                                DefaultType = ThresholdDefaultType.Condition,
                                ConditionIds = patientConditionIds.ToList(),
                                Q = request.Q
                            });                           
                        }

                        var customerDefaultThresholdsPagedResult = await defaultThresholdsService.GetDefaultThresholds(customerId, new DefaultThresholdsSearchDto()
                        {
                            DefaultType = ThresholdDefaultType.Customer,
                            Q = request.Q
                        });

                        var defaultThresholds = new List<DefaultThreshold>(customerDefaultThresholdsPagedResult.Results);
                        if (conditionDefaultThresholdsPagedResult.Results != null)
                        {
                            defaultThresholds.AddRange(conditionDefaultThresholdsPagedResult.Results);
                        }
                        
                        var aggregatedThresholds = thresholdAggregator.AggregateThresholds(defaultThresholds, patientThresholdsPagedResult.Results);
                        
                        var filteredAggregatedThresholds = FilterUnusedThresholds(Mapper.Map<IList<BaseThresholdDto>>(aggregatedThresholds), customerAlertSeverities);

                        return new PagedResultDto<BaseThresholdDto>
                        {
                            Results = filteredAggregatedThresholds.Skip(request.Skip).Take(request.Take).ToList(),
                            Total = filteredAggregatedThresholds.Count
                        };
                    }
                }
            }

            var pagedResult = await thresholdsService.GetThresholds(customerId, patientId, request);

            var filteredResult = FilterUnusedThresholds(
                Mapper.Map<IList<PatientThreshold>, IList<PatientThresholdDto>>(pagedResult.Results).Cast<BaseThresholdDto>().ToList(),
                customerAlertSeverities
            );

            return new PagedResultDto<BaseThresholdDto>
            {
                Results = filteredResult,
                Total = pagedResult.Total
            };
        }

        /// <summary>
        /// Filters the unused thresholds.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="customersAlertSeverities">The customers alert severities.</param>
        /// <returns></returns>
        private IList<BaseThresholdDto> FilterUnusedThresholds(
            IList<BaseThresholdDto> input,
            IList<AlertSeverity> customersAlertSeverities
        )
        {
            if (customersAlertSeverities.Any())
            {
                return input.Where(t => t.AlertSeverity != null).ToList();
            }

            return input;
        }
    }
}