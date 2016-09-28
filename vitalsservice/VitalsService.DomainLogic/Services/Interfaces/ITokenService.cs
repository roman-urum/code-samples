using System.Threading.Tasks;
using VitalsService.Domain.Dtos.TokenServiceDtos;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// ITokenService
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Checks the access.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        Task<VerifyAccessResponse> CheckAccess(string route);

        /// <summary>
        /// Returns true if token has access to specified service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> CheckAccess(VerifyTokenRequest request);

        /// <summary>
        /// Send request to Token service to verify that device certificate
        /// has access to patient.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<VerifyCertificateResponse> VerifyCertificate(VerifyCertificateRequest request);
    }
}
