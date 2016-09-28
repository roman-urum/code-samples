using System;
using System.Web.Mvc;
using Maestro.Domain.Constants;
using Maestro.Web.Extensions;
using Maestro.Web.Security;

namespace Maestro.Web.Filters
{
    /// <summary>
    /// Aply this attribute to action/class which should be available
    /// only for anonymouse users.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AnonymousOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            var user = filterContext.HttpContext.User as IMaestroPrincipal;

            if (user == null)
                return;

            if (user.IsInRoles(Roles.SuperAdmin))
            {
                filterContext.Result = new RedirectResult("/");
            }
            else
            {
                var urlHelper = new UrlHelper(filterContext.RequestContext);
                
                filterContext.Result =
                    new RedirectResult(urlHelper.SiteDefaultUrl(user));
            }
        }
    }
}