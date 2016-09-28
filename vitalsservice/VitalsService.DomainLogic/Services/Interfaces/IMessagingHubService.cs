using System.Threading.Tasks;
using VitalsService.Domain.Dtos.MessagingHub;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Provides methods to send notifications using messaging hub.
    /// </summary>
    public interface IMessagingHubService
    {
        /// <summary>
        /// Sends notification to user device about starting video call.
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        Task SendPushNotification(NotificationDto notification);
    }
}