using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DeviceService.Web.Api.Filters
{
    /// <summary>
    /// Base class for authorization attributes.
    /// </summary>
    public abstract class BaseAuthorizeAttribute : AuthorizeAttribute
    {
        private const string AuthorizedFlagKey = "IsAuthorized";

        /// <summary>
        /// Identifies if authorization already completed and provides result.
        /// </summary>
        public static bool? IsAuthorized
        {
            get
            {
                var value = HttpContext.Current.Items[AuthorizedFlagKey];

                return value as bool?;
            }
            private set
            {
                HttpContext.Current.Items[AuthorizedFlagKey] = value;
            }
        }

        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                IsAuthorized.HasValue && IsAuthorized.Value)
            {
                return;
            }

            IsAuthorized = await this.IsAuthorizedRequest(actionContext);
        }

        protected abstract Task<bool> IsAuthorizedRequest(HttpActionContext actionContext);
    }
}