using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NLog;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Filters
{
    /// <summary>
    /// LogIpAddressesAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LogIpAddressesAttribute : ActionFilterAttribute
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private const string IpAddressesLogFormat = "Authentication attempt: ClientIP - {0}, ForwardedClientIP - {1}";

        /// <summary>
        /// Called when [action executing].
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var headers = actionContext.Request.Headers;

            IEnumerable<string> clientIpValues;
            IEnumerable<string> forwardedClientIpValues;

            if (headers.TryGetValues("Client-IP", out clientIpValues) &&
                headers.TryGetValues("Forwarded-For", out forwardedClientIpValues) &&
                clientIpValues.Any() &&
                forwardedClientIpValues.Any()
            )
            {
                logger.Info(
                    IpAddressesLogFormat,
                    clientIpValues.First(),
                    forwardedClientIpValues.First()
                );
            }
        }
    }
}