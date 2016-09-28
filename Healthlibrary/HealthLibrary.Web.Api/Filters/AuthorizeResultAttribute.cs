using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Web.Api.Exceptions;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Enums;
using HealthLibrary.Web.Api.Security;
using Microsoft.Practices.ServiceLocation;

namespace HealthLibrary.Web.Api.Filters
{
    /// <summary>
    /// Global attribute to verify access rights using selected type of authorization.
    /// Type of authorization should be defined for action using attributes
    /// TokenAuthorize and Certificate authorize.
    /// </summary>
    public class AuthorizeResultAttribute : AuthorizeAttribute
    {
        private const string CustomerIdRouteName = "customerId";

        /// <summary>
        /// Called when [authorization asynchronous].
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public override async Task OnAuthorizationAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            var isTokenAuthUsed = IsAttributeAppliedToAction<TokenAuthorizeAttribute>(actionContext);
            var isCertificateAuthUsed = IsAttributeAppliedToAction<CertificateAuthorizeAttribute>(actionContext);

            if (isTokenAuthUsed)
            {
                if (await CheckTokenAuthorization(actionContext))
                {
                    return;
                }
            }

            if (isCertificateAuthUsed)
            {
                if (await CheckCertificateAuthorization(actionContext))
                {
                    return;
                }
            }

            if (isTokenAuthUsed || isCertificateAuthUsed)
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.Unauthorized,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidAccessToken,
                        Message = ErrorCode.InvalidAccessToken.Description()
                    }
                );
            }
        }

        /// <summary>
        /// Verifies if attribute with specified type is applied to current action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private bool IsAttributeAppliedToAction<T>(HttpActionContext actionContext) where T : class
        {
            var actionDescriptor = actionContext.ActionDescriptor;
            var controllerDescriptor = actionContext.ControllerContext.ControllerDescriptor;

            return actionDescriptor.GetCustomAttributes<T>().Any() ||
                   controllerDescriptor.GetCustomAttributes<T>().Any();
        }

        /// <summary>
        /// Verifies certificate authorization for current request.
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private async Task<bool> CheckCertificateAuthorization(HttpActionContext actionContext)
        {
            var routeData = actionContext.RequestContext.RouteData;

            var customerIdValue = routeData.Values[CustomerIdRouteName];
            int customerId;

            if (customerIdValue == null || !int.TryParse(customerIdValue.ToString(), out customerId))
            {
                throw new AttributeUsageException("Incorrect customer Id");
            }

            if (CertificateAuthContext.Current.IsAuthorizedRequest)
            {
                return await CertificateAuthContext.Current.HasAccess(customerId);
            }

            return false;
        } 

        /// <summary>
        /// Verifies token authorization for current request.
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private async Task<bool> CheckTokenAuthorization(HttpActionContext actionContext)
        {
            var careElementContext = ServiceLocator.Current.GetInstance<ICareElementRequestContext>();

            careElementContext.Initialize(actionContext.ActionDescriptor.ControllerDescriptor.ControllerName);

            return await careElementContext.IsAuthorizedRequest();
        }
    }
}