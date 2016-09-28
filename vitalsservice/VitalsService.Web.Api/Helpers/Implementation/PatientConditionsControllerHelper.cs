using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using NLog;

using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos.MessagingHub;
using VitalsService.Domain.Enums;
using VitalsService.Domain.Enums.MessagingHub;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models.Conditions;
using VitalsService.Web.Api.Models.PatientConditions;

namespace VitalsService.Web.Api.Helpers.Implementation
{
    /// <summary>
    /// PatientConditionsControllerHelper.
    /// </summary>
    public class PatientConditionsControllerHelper : IPatientConditionsControllerHelper
    {
        private readonly IPatientConditionsService patientConditionsService;
        private readonly IMessagingHubService messagingHubService;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientConditionsControllerHelper"/> class.
        /// </summary>
        /// <param name="patientConditionsService">The patient conditions service.</param>
        /// <param name="messagingHubService">The messaging hub service.</param>
        public PatientConditionsControllerHelper(
            IPatientConditionsService patientConditionsService,
            IMessagingHubService messagingHubService)
        {
            this.patientConditionsService = patientConditionsService;
            this.messagingHubService = messagingHubService;
        }

        #region Implementation of IPatientConditionsControllerHelper

        /// <summary>
        /// Creates the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<CreateUpdatePatientConditionsStatus> CreatePatientConditions(
            int customerId,
            Guid patientId, 
            PatientConditionsRequest request
        )
        {
            var creationResult = await patientConditionsService.CreatePatientConditions(customerId, patientId, request.PatientConditionsIds);

            if (creationResult == CreateUpdatePatientConditionsStatus.Success)
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

            return creationResult;
        }

        /// <summary>
        /// Gets the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<IList<ConditionResponseDto>> GetPatientConditions(int customerId, Guid patientId)
        {
            var patientConditions = await patientConditionsService.GetPatientConditions(customerId, patientId);

            return Mapper.Map<IList<Condition>, List<ConditionResponseDto>>(patientConditions);
        }

        #endregion
    }
}