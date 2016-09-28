using System.Threading.Tasks;
using DeviceService.ApiAccess.ApiClient;
using RestSharp;
using VitalsService;
using VitalsService.Domain.Dtos.MessagingHub;

namespace Vitals.ApiAccess.DataProviders.Implementation
{
    /// <summary>
    /// NotificationsDataProvider.
    /// </summary>
    public class MessagingHubDataProvider : IMessagingHubDataProvider
    {
        private readonly IRestApiClient restApiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingHubDataProvider" /> class.
        /// </summary>
        /// <param name="restApiClientFactory">The rest API client factory.</param>
        public MessagingHubDataProvider(
            IRestApiClientFactory restApiClientFactory
        )
        {
            this.restApiClient = restApiClientFactory.Create(Settings.MessagingHubUrl);
        }

        /// <summary>
        /// Sends the notification.
        /// </summary>
        /// <param name="notificationDto">The notification dto.</param>
        /// <returns></returns>
        public async Task<NotificationResponseDto> SendNotification(NotificationDto notificationDto)
        {
            return await this.restApiClient.SendRequestAsync<NotificationResponseDto>("/api/notifications/", notificationDto, Method.POST, "");
        }
    }
}
