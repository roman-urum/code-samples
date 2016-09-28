using System.Threading.Tasks;
using DeviceService.Domain.Dtos.MessagingHub;
using DeviceService.ApiAccess.ApiClient;
using DeviceService.Common;
using RestSharp;

namespace DeviceService.ApiAccess.DataProviders.Implementation
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
        /// <param name="appSettings">The application settings.</param>
        public MessagingHubDataProvider(
            IRestApiClientFactory restApiClientFactory,
            IAppSettings appSettings
        )
        {
            this.restApiClient = restApiClientFactory.Create(appSettings.MessagingHubUrl);
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