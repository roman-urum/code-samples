using System.Collections.Generic;
using System.Threading.Tasks;

using Maestro.Domain.Dtos.MessagingHub;
using Maestro.Domain.Dtos.MessagingHub.Enums;
using Maestro.Domain.Dtos.VitalsService.PatientNotes;
using Maestro.Web.Areas.Site.Models.Patients.ChatMessages;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// PatientsControllerManager.ChatMessages
    /// </summary>
    public partial class PatientsControllerManager
    {
        /// <summary>
        /// Sends push notification to patient. 
        /// </summary>
        /// <param name="messageRequest">The notification request.</param>
        /// <returns></returns>
        public async Task SendChatMessage(ChatMessageViewModel messageRequest)
        {
            var notification = new NotificationDto
            {
                AllTags = true,
                Tags = new List<string>
                {
                    string.Format("maestro-customer-{0}", CustomerContext.Current.Customer.Id),
                    string.Format("maestro-patientid-{0}", messageRequest.PatientId)
                },
                Types = new List<RegistrationType>
                {
                    RegistrationType.APN,
                    RegistrationType.GCM
                },
                Data = new
                {
                    PatientDeviceNotification = new
                    {
                        Action = "CaringNoteAdded",
                        CustomerID = CustomerContext.Current.Customer.Id,
                        PatientID = messageRequest.PatientId,
                        Data = new
                        {
                            text = messageRequest.Message
                        }
                    }
                },
                Sender = string.Format("maestro-customer-{0}", CustomerContext.Current.Customer.Id),
                Message = null
            };

            await messagingHubService.SendPushNotification(notification);

            var note = new CreateNoteRequestDto()
            {
                Notables = new []{ "Text Message" },
                Text = messageRequest.Message,
                CreatedBy = authDataStorage.GetUserAuthData().FullName
            };

            await notesService.CreateNote(CustomerContext.Current.Customer.Id,
                                          messageRequest.PatientId,
                                          note,
                                          authDataStorage.GetToken());
        }
    }
}