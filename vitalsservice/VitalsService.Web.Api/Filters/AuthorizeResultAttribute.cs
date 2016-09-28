using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Practices.ServiceLocation;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Web.Api.Exceptions;
using VitalsService.Web.Api.Extensions;
using VitalsService.Web.Api.Security;
using NLog;
using JWT;
using System.Collections.Generic;
using NLog.Internal;
using VitalsService.Extensions;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Enums;

namespace VitalsService.Web.Api.Filters
{
    /// <summary>
    /// Global attribute to verify access rights using selected type of authorization.
    /// Type of authorization should be defined for action using attributes
    /// TokenAuthorize and Certificate authorize.
    /// </summary>
    public class AuthorizeResultAttribute : AuthorizeAttribute
    {
        private const string CustomerIdRouteName = "customerId";
        private const string PatientIdRouteName = "patientId";
        private const string Service = "Vitals";
        private const string TokenServiceUrl = "{0}?action={1}&service=vitals&controller={2}&customer={3}&patientId={4}";

        private static Logger logger = LogManager.GetCurrentClassLogger();

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
            var patientIdValue = routeData.Values[PatientIdRouteName];
            Guid patientId;

            if (patientIdValue == null || !Guid.TryParse(patientIdValue.ToString(), out patientId))
            {
                throw new AttributeUsageException("Incorrect patient Id");
            }

            var customerIdValue = routeData.Values[CustomerIdRouteName];
            int customerId;

            if (customerIdValue == null || !int.TryParse(customerIdValue.ToString(), out customerId))
            {
                throw new AttributeUsageException("Incorrect customer Id");
            }

            if (CertificateAuthContext.Current.IsAuthorizedRequest)
            {
                return await CertificateAuthContext.Current.HasAccess(customerId, patientId);
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


            if (string.IsNullOrWhiteSpace(token))
                return false;

            var parts = token.Split('.');

            if (parts.Length == 3)
            {
                return VerifyJwtLocal(token, method, controller, customerContext.CustomerId, GetRequestedPatientId());
            }
            else
            {
                var tokenService = ServiceLocator.Current.GetInstance<ITokenService>();
                var response = await tokenService.CheckAccess(route);

                var userContext = ServiceLocator.Current.GetInstance<IUserContext>();
                userContext.Initialize(actionContext.ActionDescriptor.ControllerDescriptor.ControllerName);

                return response != null && response.Allowed;
            }
        }

        private bool VerifyJwtLocal(string token, string action, string controller, int? customerId, Guid? patientId)
        {
            var jwtCustomerKey = System.Configuration.ConfigurationManager.AppSettings["JWT_KEY_" + customerId.Value] as string;

            if (jwtCustomerKey == null)
            {
                logger.Fatal("JSON Web Token (JWT) was used but [JWT_KEY_{0}] is not set.", customerId);
                return false;
            }

            // Local JWT token
            var payload = new Dictionary<string, string>();

            try
            {
                payload = JsonWebToken.DecodeToObject<Dictionary<string, string>>(token, jwtCustomerKey, true);
            }
            catch (Exception e)
            {
                logger.Error(e, "Error decoding JWT: {0}", token);
            }

            var customer = Convert.ToInt32(payload["customer"]);

            if (customerId.Value != customer)
            {
                logger.Error("Requested customer {0} does not match token customer {1}.", customerId.Value, customer);
                return false;
            }

            if (!Service.Equals(payload["service"], StringComparison.InvariantCultureIgnoreCase))
            {
                logger.Error("Requested service {0} does not match token service {1}.", Service, payload["service"]);
                return false;

            }

            if (!controller.Equals(payload["controller"], StringComparison.InvariantCultureIgnoreCase))
            {
                logger.Error("Requested controller {0} does not match token controller {1}.", controller, payload["controller"]);
                return false;
            }

            if (!action.Equals(payload["action"], StringComparison.InvariantCultureIgnoreCase))
            {
                logger.Error("Requested action {0} does not match token action {1}.", action, payload["action"]);
                return false;
            }

            if (patientId.HasValue)
            {
                Guid payloadPatientId;

                if (Guid.TryParse(payload["patient"], out payloadPatientId))
                {
                    if (patientId.Value != payloadPatientId)
                    {
                        logger.Error("Requested patient {0} does not match token patient {1}.", patientId.Value, payload["patient"]);
                        return false;
                    }
                }
            }

            return true;
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