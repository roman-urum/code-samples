using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using DeviceService.Common;
using DeviceService.Common.Extensions;
using DeviceService.Domain.Dtos.Enums;
using DeviceService.DomainLogic.Services.Interfaces;
using DeviceService.Web.Api.Extensions;
using DeviceService.Web.Api.Models.Dtos.Enums;
using DeviceService.Web.Api.Models.Dtos.RequestsResponses;
using DeviceService.Web.Api.Security;
using Microsoft.Practices.ServiceLocation;

namespace DeviceService.Web.Api.Filters
{
    /// <summary>
    /// Global attribute to verify access rights using selected type of authorization.
    /// Type of authorization should be defined for action using attributes
    /// TokenAuthorize and Certificate authorize.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class AuthorizeResultAttribute : AuthorizeAttribute
    {
        private const string DeviceIdRouteName = "deviceId";
        private const string TokenServiceUrl = "{0}?action={1}&service=devices&controller={2}&customer={3}&patientId={4}";

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
                if (CheckCertificateAuthorization(actionContext))
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
        /// <param name="actionContext">The action context.</param>
        /// <returns></returns>
        private bool CheckCertificateAuthorization(HttpActionContext actionContext)
        {
            object deviceIdValue = actionContext.RequestContext.RouteData.Values[DeviceIdRouteName];
            Guid deviceId;

            if (deviceIdValue == null || !Guid.TryParse(deviceIdValue.ToString(), out deviceId))
            {
                return false;
            }

            var deviceContext = CertificateAuthContext.Current;
            var result = deviceContext.IsAuthorizedRequest &&
                deviceContext.DeviceId == deviceId;

            return result;
        }

        /// <summary>
        /// Verifies token authorization for current request.
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private async Task<bool> CheckTokenAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                return false;
            }

            var method = actionContext.Request.Method.Method;

            var controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var customerContext = ServiceLocator.Current.GetInstance<ICustomerContext>();

            var header = actionContext.Request.Headers.Authorization.ToString();
            
            var token = header.Substring("Bearer ".Length, header.Length - "Bearer ".Length);
            var route = string.Format(TokenServiceUrl, token, method, controller, customerContext.CustomerId, GetRequestedPatientId());
            var tokenService = ServiceLocator.Current.GetInstance<ITokenService>();
            var response = await tokenService.CheckAccess(route);

            return response != null && response.Allowed;
        }

        private Guid? GetRequestedPatientId()
        {
            var webApiRouteData = HttpContext.Current.Request.RequestContext.RouteData.GetWebApiRouteData();

            object patientIdRouteValue;

            if (webApiRouteData.TryGetValue("patientId", out patientIdRouteValue))
            {
                Guid patientId;

                if (Guid.TryParse(patientIdRouteValue.ToString(), out patientId))
                {
                    return patientId;
                }
            }

            return null;
        }
    }
}