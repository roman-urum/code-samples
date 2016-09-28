using System;
using System.Net;
using System.Threading.Tasks;
using DeviceService.ApiAccess.ApiClient;
using Newtonsoft.Json;
using RestSharp;
using VitalsService;
using VitalsService.Domain.Dtos.TokenServiceDtos;
using VitalsService.Exceptions;

namespace Vitals.ApiAccess.DataProviders.Implementation
{
    /// <summary>
    /// Default implementations of service to load data from Token Service.
    /// </summary>
    public class UsersDataProvider : IUsersDataProvider
    {
        private readonly IRestApiClient _apiClient;

        public UsersDataProvider(IRestApiClientFactory factory)
        {
            _apiClient = factory.Create(Settings.TokenServiceUrl);
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
            var restClient = new RestClient(Settings.TokenServiceUrl);

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
                return
                    await
                        _apiClient.SendRequestAsync<VerifyAccessResponse>(string.Format("api/tokens/{0}", route), null,
                            Method.GET, "");

            }
            catch (ServiceNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns info if token has access to specified service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<VerifyAccessResponse> VerifyAccess(VerifyTokenRequest request)
        {
            try
            {
                return
                    await
                        _apiClient.SendRequestAsync<VerifyAccessResponse>("api/tokens/{Id}", request, Method.GET, null);
            }
            catch (ServiceNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// Send request to Token service to verify that device certificate
        /// has access to patient.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<VerifyCertificateResponse> VerifyCertificate(VerifyCertificateRequest request)
        {
            return
                await
                    _apiClient.SendRequestAsync<VerifyCertificateResponse>("api/certificates/{Thumbprint}",
                        request, Method.GET, null);
        }
    }
}
