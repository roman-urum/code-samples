using System.Linq;
using System.Web.Mvc;
using Maestro.Common.Extensions;
using Maestro.Web.Security;
using NLog;

namespace Maestro.Web.Filters
{
    /// <summary>
    /// Use this attribute to prevent access to controllers or actions of unauthorized users.
    /// </summary>
    public class MaestroAuthorizeAttribute : ActionFilterAttribute
    {
        private const string RedirectUrlTemplate = "{0}?returnUrl={1}";

        private readonly string[] roles;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaestroAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="roles">The roles.</param>
        public MaestroAuthorizeAttribute(params string[] roles)
        {
            this.roles = roles;
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var eventLogger = LogManager.GetCurrentClassLogger();

            var principal = filterContext.HttpContext.User as IMaestroPrincipal;

            if (principal != null)
            {
                eventLogger.Info("Authenticated: " + principal.Identity.IsAuthenticated);    
            }

            if (principal == null ||
                !principal.Identity.IsAuthenticated ||
                roles.Any() &&
                !principal.IsInRoles(roles)
            )
            {
                var redirect = RedirectUrlTemplate
                    .FormatWith(WebSettings.LoginPage, filterContext.HttpContext.Request.Url.PathAndQuery);

                eventLogger.Info("Redirected: " + redirect);   
                 
                filterContext.Result = new RedirectResult(redirect);
            }
        }
    }
}