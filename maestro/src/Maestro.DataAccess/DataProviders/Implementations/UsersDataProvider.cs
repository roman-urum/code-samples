using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Common;
using Maestro.DataAccess.Api.ApiClient;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos.TokenService.RequestsResponses;
using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// Default implementations of service to load data from Token Service.
    /// </summary>
    public class UsersDataProvider : IUsersDataProvider
    {
        private readonly IRestApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersDataProvider"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public UsersDataProvider(IRestApiClientFactory factory)
        {
            _apiClient = factory.Create(Settings.TokenServiceUrl);
        }

        /// <summary>
        /// Authenticates user through Token Service.
        /// </summary>
        /// <param name="credentials">User credentials.</param>
        /// <param name="clientIpAddress">The client ip address.</param>
        /// <param name="serverIpAddress">The server ip address.</param>
        /// <returns>
        /// True if user authenticated.
        /// </returns>
        public async Task<TokenResponseModel> AuthenticateUser(
            GetTokenRequest credentials,
            string clientIpAddress,
            string serverIpAddress
        )
        {
            return await _apiClient
                .SendRequestAsync<TokenResponseModel>(
                    "/api/tokens", 
                    credentials, 
                    Method.POST,
                    new Dictionary<string, string>()
                    {
                        { "Client-IP", serverIpAddress },
                        { "Forwarded-For", clientIpAddress }
                    }
                );
        }

        /// <summary>
        /// Creates new user in token service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PrincipalResponseModel> CreateUser(CreatePrincipalModel request)
        {
            return await _apiClient.SendRequestAsync<PrincipalResponseModel>("/api/principals", request, Method.POST);
        }

        /// <summary>
        /// Gets the principals.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<PrincipalResponseModel> GetPrincipals(string id)
        {
            return await _apiClient.SendRequestAsync<PrincipalResponseModel>(
                string.Format("api/principals/{0}", id), null, Method.GET);
        }

        /// <summary>
        /// Updates the principals.
        /// </summary>
        /// <param name="principals">The principals.</param>
        /// <returns></returns>
        public async Task UpdatePrincipals(Guid id, UpdatePrincipalModel principals)
        {
            await _apiClient.SendRequestAsync(
                string.Format("api/principals/{0}", id),
                principals, Method.PUT);
        }

        /// <summary>
        /// Verifies the access.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        public VerifyAccessResponse VerifyAccess(string route)
        {
            return _apiClient.SendRequest<VerifyAccessResponse>(string.Format("api/tokens/{0}", route), null, Method.GET);
        }
    }
}