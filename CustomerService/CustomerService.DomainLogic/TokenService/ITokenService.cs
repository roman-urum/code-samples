using System.Threading.Tasks;
using CustomerService.Domain.Dtos.TokenService;

namespace CustomerService.DomainLogic.TokenService
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
