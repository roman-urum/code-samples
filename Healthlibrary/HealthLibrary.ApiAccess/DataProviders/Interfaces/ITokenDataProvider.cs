using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos.TokenService;

namespace HealthLibrary.ApiAccess.DataProviders.Interfaces
{
    /// <summary>
    /// Provides access to TokenService methods.
    /// </summary>
    public interface ITokenDataProvider
    {
        /// <summary>
        /// Returns info if token has access to specified service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<VerifyTokenResponse> VerifyToken(VerifyTokenRequest request);

        /// <summary>
        /// Send request to Token service to verify that device certificate
        /// has access to patient.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<VerifyCertificateResponse> VerifyCertificate(VerifyCertificateRequest request);
    }
}
