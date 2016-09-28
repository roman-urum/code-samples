using System;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.TokenService.RequestsResponses;

namespace Maestro.DataAccess.Api.DataProviders.Interfaces
{
    /// <summary>
    /// Declares methods to communicate with users API.
    /// </summary>
    public interface IUsersDataProvider
    {
        /// <summary>
        /// Authenticates user through Token Service.
        /// </summary>
        /// <param name="credentials">User credentials.</param>
        /// <param name="clientIpAddress">The client ip address.</param>
        /// <param name="serverIpAddress">The server ip address.</param>
        /// <returns>
        /// True if user authenticated.
        /// </returns>
        Task<TokenResponseModel> AuthenticateUser(
            GetTokenRequest credentials,
            string clientIpAddress,
            string serverIpAddress
        );

        /// <summary>
        /// Creates new user in token service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PrincipalResponseModel> CreateUser(CreatePrincipalModel request);

        /// <summary>
        /// Gets the principals.
        /// </summary>
        /// <param name="tokenServiceUserId">The token service user identifier.</param>
        /// <returns></returns>
        Task<PrincipalResponseModel> GetPrincipals(string tokenServiceUserId);

        /// <summary>
        /// Updates the principals.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="principals">The principals.</param>
        /// <returns></returns>
        Task UpdatePrincipals(Guid id, UpdatePrincipalModel principals);

        /// <summary>
        /// Verifies the access.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        VerifyAccessResponse VerifyAccess(string route);
    }
}
