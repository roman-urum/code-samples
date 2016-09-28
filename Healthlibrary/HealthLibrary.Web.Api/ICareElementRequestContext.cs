using System.Threading.Tasks;
using HealthLibrary.Common;

namespace HealthLibrary.Web.Api
{
    /// <summary>
    /// Provides methods for request context of care element.
    /// </summary>
    public interface ICareElementRequestContext : ICareElementContext
    {
        /// <summary>
        /// Adds required data to context to allow use
        /// authorization checks.
        /// </summary>
        /// <param name="controllerName"></param>
        void Initialize(string controllerName);

        /// <summary>
        /// Verifies if request has token with appropriate
        /// access rights.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsAuthorizedRequest();
    }
}
