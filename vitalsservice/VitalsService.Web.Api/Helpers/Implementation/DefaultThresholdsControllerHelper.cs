using System;
using System.Collections.Generic;
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
    /// DefaultThresholdsControllerHelper.
    /// </summary>
    public class DefaultThresholdsControllerHelper : IDefaultThresholdsControllerHelper
    {
        private readonly IDefaultThresholdsService defaultThresholdsService;
        private readonly IMessagingHubService messagingHubService;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultThresholdsControllerHelper" /> class.
        /// </summary>
        /// <param name="defaultThresholdsService">The default thresholds service.</param>
        /// <param name="messagingHubService">The messaging hub service.</param>
        public DefaultThresholdsControllerHelper(IDefaultThresholdsService defaultThresholdsService,
                                                 IMessagingHubService messagingHubService)
        {
            this.defaultThresholdsService = defaultThresholdsService;
            this.messagingHubService = messagingHubService;
        }

        /// <summary>
        /// Creates the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, CreateUpdateDefaultThresholdStatus>>
            CreateDefaultThreshold(int customerId, DefaultThresholdRequestDto request)
        {
            var defaultThreshold = Mapper.Map<DefaultThresholdRequestDto, DefaultThreshold>(request);
            defaultThreshold.CustomerId = customerId;

            var result = await defaultThresholdsService.CreateDefaultThreshold(defaultThreshold);

            if (result.Status == CreateUpdateDefaultThresholdStatus.Success)
            {
                await messagingHubService.SendPushNotification(new NotificationDto()
                {
                    AllTags = true,
                    Data = new
                    {
                        PatientDeviceNotification = new
                        {
                            Action = "PatientThresholdsChanged",
                            CustmerId = defaultThreshold.CustomerId
                        }
                    },
                    Message = null,
                    Sender = string.Format("maestro-customer-{0}", defaultThreshold.CustomerId),
                    Tags = new List<string> { string.Format("maestro-customer-{0}", defaultThreshold.CustomerId) },
                    Types = new[] { RegistrationType.APN, RegistrationType.GCM }
                });
            }

            return result;
        }

        /// <summary>
        /// Updates the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<CreateUpdateDefaultThresholdStatus> UpdateDefaultThreshold(
            int customerId,
            Guid defaultThresholdId,
            DefaultThresholdRequestDto request
        )
        {
            var defaultThreshold = Mapper.Map<DefaultThresholdRequestDto, DefaultThreshold>(request);
            defaultThreshold.Id = defaultThresholdId;
            defaultThreshold.CustomerId = customerId;

            var result = await defaultThresholdsService.UpdateDefaultThreshold(defaultThreshold);

            if (result == CreateUpdateDefaultThresholdStatus.Success)
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
                                CustmerId = defaultThreshold.CustomerId
                            }
                        },
                        Message = null,
                        Sender = string.Format("maestro-customer-{0}", defaultThreshold.CustomerId),
                        Tags = new List<string> { string.Format("maestro-customer-{0}", defaultThreshold.CustomerId) },
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
        /// Deletes the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <returns></returns>
        public async Task<GetDeleteDefaultThresholdStatus> DeleteDefaultThreshold(int customerId, Guid defaultThresholdId)
        {
            var deleteDefaultThresholdResult = await defaultThresholdsService.DeleteDefaultThreshold(customerId, defaultThresholdId);

            if (deleteDefaultThresholdResult == GetDeleteDefaultThresholdStatus.Success)
            {                
                await messagingHubService.SendPushNotification(new NotificationDto()
                {
                    AllTags = true,
                    Data = new
                    {
                        PatientDeviceNotification = new
                        {
                            Action = "PatientThresholdsChanged",
                            CustmerId = defaultThresholdId
                        }
                    },
                    Message = null,
                    Sender = string.Format("maestro-customer-{0}", customerId),
                    Tags = new List<string> { string.Format("maestro-customer-{0}", customerId) },
                    Types = new[] { RegistrationType.APN, RegistrationType.GCM }
                });
                
            }

            return deleteDefaultThresholdResult;
        }

        /// <summary>
        /// Gets the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<DefaultThresholdDto, GetDeleteDefaultThresholdStatus>>
            GetDefaultThreshold(int customerId, Guid defaultThresholdId)
        {
            var result = await defaultThresholdsService.GetDefaultThreshold(customerId, defaultThresholdId);

            if (result == null)
            {
                return await Task.FromResult(
                    new OperationResultDto<DefaultThresholdDto, GetDeleteDefaultThresholdStatus>()
                    {
                        Status = GetDeleteDefaultThresholdStatus.NotFound
                    }
                );
            }

            return await Task.FromResult(
                new OperationResultDto<DefaultThresholdDto, GetDeleteDefaultThresholdStatus>()
                {
                    Status = GetDeleteDefaultThresholdStatus.Success,
                    Content = Mapper.Map<DefaultThreshold, DefaultThresholdDto>(result)
                }
            );
        }

        /// <summary>
        /// Gets the default thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<DefaultThresholdDto>> GetDefaultThresholds(int customerId, DefaultThresholdsSearchDto request)
        {
            var result = await defaultThresholdsService.GetDefaultThresholds(customerId, request);

            return Mapper.Map<PagedResult<DefaultThreshold>, PagedResultDto<DefaultThresholdDto>>(result);
        }
    }
}