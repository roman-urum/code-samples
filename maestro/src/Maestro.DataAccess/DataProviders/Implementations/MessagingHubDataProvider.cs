using System.Threading.Tasks;
using Maestro.Common;
using Maestro.DataAccess.Api.ApiClient;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos.MessagingHub;
using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// MessagingHubDataProvider.
    /// </summary>
    /// <seealso cref="Maestro.DataAccess.Api.DataProviders.Interfaces.IMessagingHubDataProvider" />
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
            return await this.restApiClient.SendRequestAsync<NotificationResponseDto>("/api/notifications/", notificationDto, Method.POST, null);
        }
    }
}