using System.Collections.Generic;
using System.Threading.Tasks;
using DeviceService.ApiAccess.ApiClient;
using DeviceService.Domain.Dtos.iHealth;
using RestSharp;

namespace DeviceService.ApiAccess.DataProviders.Implementation
{
    /// <summary>
    /// Provides access to iHealth API.
    /// </summary>
    public class iHealthDataProvider : IiHealthDataProvider
    {
        private const string DefaultApiName = "OpenAPIALL";
        private const string DefaultRedirectUri = "http://localhost/ihealth/createusercallback";

        private readonly IRestApiClient restApiClient;
        private readonly IiHealthSettings iHealthSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingHubDataProvider" /> class.
        /// </summary>
        /// <param name="restApiClientFactory">The rest API client factory.</param>
        public iHealthDataProvider(IRestApiClientFactory restApiClientFactory, IiHealthSettings iHealthSettings)
        {
            this.iHealthSettings = iHealthSettings;

            this.restApiClient = restApiClientFactory.Create(this.iHealthSettings.iHealthApiUrl);
        }

        /// <summary>
        /// Creates new account in iHealth API.
        /// </summary>
        /// <returns></returns>
        public async Task<iHealthUserResponseDto> RegisterUser(CreateiHealthUserRequestDto requestDto)
        {
            requestDto.Sv = this.iHealthSettings.iHealthSv;
            requestDto.Sc = this.iHealthSettings.iHealthSc;
            requestDto.ClientId = this.iHealthSettings.iHealthClientId;
            requestDto.ClientSecret = this.iHealthSettings.iHealthClientSecret;
            requestDto.APIName = DefaultApiName;
            requestDto.RedirectUri = DefaultRedirectUri;

            var headers = new Dictionary<string, string>
            {
                {"Referer", "http://localhost/"}
            };

            return await this.restApiClient.SendRequestAsync<iHealthUserResponseDto>("/user/register.json", requestDto, Method.POST, headers);
        }
    }
}
