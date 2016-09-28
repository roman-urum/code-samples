using System.Threading.Tasks;

using Vitals.ApiAccess.DataProviders;

using VitalsService.Domain.Dtos.MessagingHub;
using VitalsService.DomainLogic.Services.Interfaces;

namespace VitalsService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Provides methods to send notifications using messaging hub.
    /// </summary>
    public class MessagingHubService : IMessagingHubService
    {
        private readonly IMessagingHubDataProvider messagingHubDataProvider;

        public MessagingHubService(IMessagingHubDataProvider messagingHubDataProvider)
        {
            this.messagingHubDataProvider = messagingHubDataProvider;
        }

        public async Task SendPushNotification(NotificationDto notification)
        {
            await this.messagingHubDataProvider.SendNotification(notification);
        }
    }
}
