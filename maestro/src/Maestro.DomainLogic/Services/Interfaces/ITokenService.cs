using Maestro.Domain.Dtos.TokenService.RequestsResponses;

namespace Maestro.DomainLogic.Services.Interfaces
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
        VerifyAccessResponse CheckAccess(string route);
    }
}
