using System.Threading.Tasks;
using DeviceService.Domain.Dtos.TokenService;

namespace DeviceService.ApiAccess.DataProviders
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
        Task<VerifyAccessResponse> VerifyAccess(string route);

        /// <summary>
        /// Save info about patient device certificate
        /// in token service.
        /// </summary>
        /// <returns></returns>
        Task CreateCertificate(CreateCertificateRequest request);

        /// <summary>
        /// Save info about patient device certificate
        /// in token service.
        /// </summary>
        /// <returns></returns>
        Task DeleteCertificate(DeleteCertificateRequest request);
    }
}