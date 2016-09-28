using System.Threading.Tasks;
using Maestro.Domain.Dtos.MessagingHub;

namespace Maestro.DataAccess.Api.DataProviders.Interfaces
{
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
