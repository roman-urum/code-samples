using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Maestro.Common.Extensions;
using Maestro.DomainLogic.Services.Interfaces;
using Maestro.Web.Extensions;
using Maestro.Web.Models.Api.Dtos.Enums;
using Maestro.Web.Models.Api.Dtos.RequestsResponses;
using Microsoft.Practices.ServiceLocation;

namespace Maestro.Web.Filters
{
    /// <summary>
    /// TokenAuthorizeAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        private const string TokenServiceUrl = "{0}?action={1}&service=users&controller={2}&customer={3}";

        /// <summary>
        /// Processes requests that fail authorization.
        /// </summary>
        /// <param name="actionContext">The context.</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.Unauthorized,
                new ErrorResponseDto()
                {
                    Error = ErrorCode.InvalidAccessToken,
                    Message = ErrorCode.InvalidAccessToken.Description()
                });
        }

        /// <summary>
        /// Indicates whether the specified control is authorized.
        /// </summary>
        /// <param name="actionContext">The context.</param>
        /// <returns>
        /// true if the control is authorized; otherwise, false.
        /// </returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var method = actionContext.Request.Method.Method;

                var controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                int? customer = null;

                var webApiRouteData = HttpContext.Current.Request.RequestContext.RouteData.GetWebApiRouteData();

                object customerIdRouteValue;

                if (webApiRouteData.TryGetValue("customerId", out customerIdRouteValue))
                {
                    int customerId;

                    if (int.TryParse(customerIdRouteValue.ToString(), out customerId))
                    {
                        customer = customerId;
                    }
                }

                var header = actionContext.Request.Headers.Authorization.ToString();

                // remove "Bearer "
                var token = header.Substring(7, header.Length - 7);

                var route = string.Format(TokenServiceUrl, token, method, controller, customer);

                var tokenService = ServiceLocator.Current.GetInstance<ITokenService>();

                var response = tokenService.CheckAccess(route);

                return response != null && response.Allowed;
            }

            return false;
        }
    }
}