using System;
using System.Threading.Tasks;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Domain.Dtos.MessagingHub;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Provides methods to send notifications using messaging hub.
    /// </summary>
    public interface IMessagingHubService
    {
        /// <summary>
        /// Sends notification to user device about starting video call.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="meetingId"></param>
        /// <param name="patient"></param>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task SendVideoCallNotification(int customerId, long meetingId, PatientDto patient,
            User sender, object data = null);

        /// <summary>
        /// Sends notification to user device about starting video call.
        /// </summary>
        /// <param name="notificationDto">The notification.</param>
        /// <returns></returns>
        Task SendPushNotification(NotificationDto notificationDto);
    }
}
