using System.Threading.Tasks;
using CustomerService.Domain.Dtos.TokenService;

namespace CustomerService.ApiAccess.DataProviders
{
    public interface IUsersDataProvider
    {
        /// <summary>
        /// Authenticates user through Token Service.
        /// </summary>
        /// <param name="credentials">User credentials.</param>
        /// <returns>True if user authenticated.</returns>
        Task<GetTokenResponse> AuthenticateUser(GetTokenRequest credentials);

        /// <summary>
        /// Creates new user in token service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> CreateUser(CreateUserRequest request);

        /// <summary>
        /// Verifies the access.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        VerifyAccessResponse VerifyAccess(string route);
    }
}
