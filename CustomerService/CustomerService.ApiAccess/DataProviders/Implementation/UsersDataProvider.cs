using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CustomerService.ApiAccess.ApiClient;
using CustomerService.Common;
using CustomerService.Common.Exceptions;
using CustomerService.Domain.Dtos.TokenService;
using Newtonsoft.Json;
using RestSharp;

namespace CustomerService.ApiAccess.DataProviders.Implementation
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
            return await _apiClient.SendRequestAsync<GetTokenResponse>("api/tokens", credentials, Method.POST);
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
        public VerifyAccessResponse VerifyAccess(string route)
        {
            try
            {
                return _apiClient.SendRequest<VerifyAccessResponse>(string.Format("api/tokens/{0}", route), null, Method.GET);
            }
            catch (ServiceNotFoundException)
            {
                return null;
            }
        }
    }
}
