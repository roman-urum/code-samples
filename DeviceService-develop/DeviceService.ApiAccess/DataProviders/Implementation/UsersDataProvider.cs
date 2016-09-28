using System.Net;
using System.Threading.Tasks;
using DeviceService.ApiAccess.ApiClient;
using DeviceService.Common;
using DeviceService.Common.Exceptions;
using DeviceService.Domain.Dtos.TokenService;
using Newtonsoft.Json;
using RestSharp;

namespace DeviceService.ApiAccess.DataProviders.Implementation
{
    /// <summary>
    /// Default implementations of service to load data from Token Service.
    /// </summary>
    public class UsersDataProvider : IUsersDataProvider
    {
        private readonly IRestApiClient _apiClient;
        private readonly IAppSettings appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersDataProvider"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="appSettings">The application settings.</param>
        public UsersDataProvider(
            IRestApiClientFactory factory,
            IAppSettings appSettings
        )
        {
            _apiClient = factory.Create(appSettings.TokenServiceUrl);
            this.appSettings = appSettings;
        }

        /// <summary>
        /// Authenticates user through Token Service.
        /// </summary>
        /// <param name="credentials">User credentials.</param>
        /// <returns>True if user authenticated.</returns>
        public async Task<GetTokenResponse> AuthenticateUser(GetTokenRequest credentials)
        {
            return await _apiClient.SendRequestAsync<GetTokenResponse>("api/tokens", credentials, Method.POST, "");
        }

        /// <summary>
        /// Creates new user in token service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CreateUser(CreateUserRequest request)
        {
            var restClient = new RestClient(appSettings.TokenServiceUrl);

            var requestToSend = new RestRequest("api/principals", Method.POST);

            string jsonToSend = JsonConvert.SerializeObject(request);

            requestToSend.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            requestToSend.RequestFormat = DataFormat.Json;

            var result = await restClient.ExecuteTaskAsync(requestToSend);

            return result.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// Verifies the access.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        public async Task<VerifyAccessResponse> VerifyAccess(string route)
        {
            try
            {
                return await _apiClient.SendRequestAsync<VerifyAccessResponse>(string.Format("api/tokens/{0}", route), null, Method.GET, "");
            }
            catch (ServiceNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// Save info about patient device certificate
        /// in token service.
        /// </summary>
        /// <returns></returns>
        public async Task CreateCertificate(CreateCertificateRequest request)
        {
            await _apiClient.SendRequestAsync("api/certificates", request, Method.POST, null);
        }

        /// <summary>
        /// Save info about patient device certificate
        /// in token service.
        /// </summary>
        /// <returns></returns>
        public async Task DeleteCertificate(DeleteCertificateRequest request)
        {
            var url = string.Format("api/certificates/{0}", request.Thumbprint);
            await _apiClient.SendRequestAsync(url, request, Method.DELETE, null);
        }
    }
}