using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos.TokenService;

namespace HealthLibrary.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Contains business rules to validate authorization data.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Returns true if token has access to specified service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> VerifyAccess(VerifyTokenRequest request);

        /// <summary>
        /// Send request to Token service to verify that device certificate
        /// has access to patient.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<VerifyCertificateResponse> VerifyCertificate(VerifyCertificateRequest request);
    }
}
