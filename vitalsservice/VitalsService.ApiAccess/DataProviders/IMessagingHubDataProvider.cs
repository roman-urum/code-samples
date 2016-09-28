using System.Threading.Tasks;

using VitalsService.Domain.Dtos.MessagingHub;

namespace Vitals.ApiAccess.DataProviders
{
    /// <summary>
    /// INotificationsDataProvider.
    /// </summary>
    public interface IMessagingHubDataProvider
    {
        /// <summary>
        /// Sends the notification.
        /// </summary>
        /// <param name="notificationDto">The notification dto.</param>
        /// <returns></returns>
        Task<NotificationResponseDto> SendNotification(NotificationDto notificationDto);
    }
}