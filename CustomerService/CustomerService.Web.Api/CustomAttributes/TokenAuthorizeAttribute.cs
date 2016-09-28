using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using CustomerService.Common.Extensions;
using CustomerService.DomainLogic.TokenService;
using CustomerService.Web.Api.Extensions;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.Enums;
using Microsoft.Practices.ServiceLocation;

namespace CustomerService.Web.Api.CustomAttributes
{
    /// <summary>
    /// TokenAuthorizeAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        private const string TokenServiceUrl = "{0}?action={1}&service=customers&controller={2}&customer={3}";

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
        /// Called when [authorization].
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
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

                if (response == null || !response.Allowed)
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
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.Unauthorized,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.TokenNotProvided,
                        Message = ErrorCode.TokenNotProvided.Description()
                    }
                );
            }
        }
    }
}