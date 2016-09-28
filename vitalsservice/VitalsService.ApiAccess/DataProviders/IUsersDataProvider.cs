using System.Threading.Tasks;
using VitalsService.Domain.Dtos.TokenServiceDtos;

namespace Vitals.ApiAccess.DataProviders
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
        /// Returns info if token has access to specified service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<VerifyAccessResponse> VerifyAccess(VerifyTokenRequest request);

        /// <summary>
        /// Send request to Token service to verify that device certificate
        /// has access to patient.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<VerifyCertificateResponse> VerifyCertificate(VerifyCertificateRequest request);
    }
}
