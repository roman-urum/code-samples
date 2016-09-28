using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maestro.Common.Extensions;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.MessagingHub;
using Maestro.Domain.Dtos.MessagingHub.Enums;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.DomainLogic.Services.Interfaces;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Provides methods to send notifications using messaging hub.
    /// </summary>
    public class MessagingHubService : IMessagingHubService
    {
        private const string CustomerTagTemplate = "maestro-customer-{0}";
        private const string PatientIdTagTemplate = "maestro-patientid-{0}";
        private const string VideoCallNotificationTitleTemplate = "{0} is calling you";
        private const string SenderNameTemplate = "PROV{0}";

        private static readonly RegistrationType[] MobileRegistrationTypes = new RegistrationType[]
        {
            RegistrationType.APN,
            RegistrationType.GCM
        };

        private readonly IMessagingHubDataProvider messagingHubDataProvider;

        public MessagingHubService(IMessagingHubDataProvider messagingHubDataProvider)
        {
            this.messagingHubDataProvider = messagingHubDataProvider;
        }

        /// <summary>
        /// Sends notification to user device about starting video call.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="meetingId"></param>
        /// <param name="patient"></param>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task SendVideoCallNotification(int customerId, long meetingId, PatientDto patient,
            User sender, object data = null)
        {
            var tags = new List<string>
            {
                CustomerTagTemplate.FormatWith(customerId),
                PatientIdTagTemplate.FormatWith(patient.Id)
            };
            var senderFullName = string.Join(" ", sender.FirstName, sender.LastName);
            var title = VideoCallNotificationTitleTemplate.FormatWith(senderFullName);
            var requestDto = new NotificationDto
            {
                AllTags = true,
                Tags = NormalizeTags(tags),
                Types = MobileRegistrationTypes,
                Data = new
                {
                    call = new
                    {
                        customerID = customerId,
                        patientID = patient.Id,
                        meetingID = meetingId,
                        title = title,
                        data = data
                    }
                },
                Sender = SenderNameTemplate.FormatWith(customerId),
                Message = null
            };

            await this.messagingHubDataProvider.SendNotification(requestDto);
        }

        #region Private methods

        /// <summary>
        /// Converts provided list of tags to lowercase.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        private static string[] NormalizeTags(IEnumerable<string> tags)
        {
            var list = new List<string>(tags);

            return list.Select(x => x.ToLowerInvariant()).ToArray();
        }

        public async Task SendPushNotification(NotificationDto notification)
        {
            await this.messagingHubDataProvider.SendNotification(notification);
        }

        #endregion
    }
}
