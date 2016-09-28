using System.Threading.Tasks;
using DeviceService.Domain.Dtos.TokenService;

namespace DeviceService.DomainLogic.Services.Interfaces
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
    }
}