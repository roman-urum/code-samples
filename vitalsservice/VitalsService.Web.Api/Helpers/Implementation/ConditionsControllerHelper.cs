using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;

using NLog;

using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos.MessagingHub;
using VitalsService.Domain.Enums.MessagingHub;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Conditions;

namespace VitalsService.Web.Api.Helpers.Implementation
{
    /// <summary>
    /// ConditionsControllerHelper.
    /// </summary>
    public class ConditionsControllerHelper: IConditionsControllerHelper
    {
        private readonly IConditionsService conditionsService;
        private readonly ITagService tagService;
        private readonly IMessagingHubService messagingHubService;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conditionsService"></param>
        /// <param name="tagService"></param>
        /// <param name="messagingHubService"></param>
        public ConditionsControllerHelper(
            IConditionsService conditionsService,
            ITagService tagService,
            IMessagingHubService messagingHubService)
        {
            this.conditionsService = conditionsService;
            this.tagService = tagService;
            this.messagingHubService = messagingHubService;
        }

        /// <summary>
        /// Gets the condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="conditionId">The condition identifier</param>
        /// <returns>Condition entity or null</returns>
        public async Task<OperationResultDto<ConditionResponseDto, ConditionStatus>> GetCondition(int customerId, Guid conditionId)
        {
            var resultConditon = await conditionsService.GetCondition(customerId, conditionId);

            if (resultConditon == null)
            {
                return new OperationResultDto<ConditionResponseDto, ConditionStatus>()
                {
                    Content = null,
                    Status = ConditionStatus.NotFound
                };
            }

            return new OperationResultDto<ConditionResponseDto, ConditionStatus>()
            {
                Content = Mapper.Map<ConditionResponseDto>(resultConditon),
                Status = ConditionStatus.Success
            };
        }

        /// <summary>
        /// Get the list of conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The search request.</param>
        /// <returns>The list of conditions.</returns>
        public async Task<PagedResultDto<ConditionResponseDto>> GetConditions(int customerId, ConditionSearchDto request)
        {
            var resultConditions = await conditionsService.GetConditions(customerId, request);

            return Mapper.Map<PagedResult<Condition>, PagedResultDto<ConditionResponseDto>>(resultConditions);
        }

        /// <summary>
        /// Creates condition.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request">The condition entity.</param>
        /// <returns>The creation result.</returns>
        public async Task<OperationResultDto<Guid, ConditionStatus>> CreateCondition(int customerId, ConditionRequestDto request)
        {
            var conditionEntity = Mapper.Map<Condition>(request);
            conditionEntity.Tags = await tagService.BuildTagsList(request.Tags, customerId);
            conditionEntity.CustomerId = customerId;

            return await conditionsService.CreateCondition(conditionEntity);
        }

        /// <summary>
        /// Updates the condition.
        /// </summary>
        /// <param name="conditionId"></param>
        /// <param name="request">The condition entity.</param>
        /// <param name="customerId"></param>
        /// <returns>The update result.</returns>
        public async Task<ConditionStatus> UpdateCondition(int customerId, Guid conditionId, ConditionRequestDto request)
        {
            var conditionEntity = Mapper.Map<Condition>(request);
            conditionEntity.Tags = await tagService.BuildTagsList(request.Tags, customerId);
            conditionEntity.CustomerId = customerId;
            conditionEntity.Id = conditionId;

            return await conditionsService.UpdateCondition(conditionEntity);
        }

        /// <summary>
        /// Deletes the condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="conditionId">The condition identifier.</param>
        /// <returns>The delete result.</returns>
        public async Task<ConditionStatus> DeleteCondition(int customerId, Guid conditionId)
        {
            var conditionToBeDeteled = await conditionsService.GetCondition(customerId, conditionId);

            if (conditionToBeDeteled == null)
            {
                return ConditionStatus.NotFound;
            }

            var affectedPatientIds = conditionToBeDeteled.PatientConditions.Select(cp => cp.PatientId).ToList();

            var deleteResult = await conditionsService.DeleteCondition(customerId, conditionId);

            if (deleteResult == ConditionStatus.Success && affectedPatientIds.Any())
            {
                foreach (var patientId in affectedPatientIds)
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
                    catch (Exception ex)
                    {
                        logger.Error(ex, "An error occured whyn try to send push notification to patient's device");
                    }                    
                }
            }

            return deleteResult;
        }
    }
}