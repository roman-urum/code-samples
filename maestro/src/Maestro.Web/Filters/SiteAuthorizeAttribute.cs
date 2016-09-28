using System.Linq;
using System.Web.Mvc;
using Maestro.Domain.Constants;
using Maestro.Web.Extensions;
using Maestro.Web.Security;

namespace Maestro.Web.Filters
{
    /// <summary>
    /// Attributes to verify if user has access to current site.
    /// Should be used only for site area.
    /// Validates user role, site assignment, site enabled state.
    /// </summary>
    public class SiteAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User as IMaestroPrincipal;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                return;
            }

            if (!SiteContext.Current.Site.IsActive)
            {
                filterContext.Result = new RedirectResult(WebSettings.NoAccessSitesPage);
            }

            if (!user.Identity.IsAuthenticated || user.IsInRole(Roles.SuperAdmin))
            {
                return;
            }

            if (!user.CustomerSubdomain.Equals(CustomerContext.Current.Customer.Subdomain) ||
                user.Sites.All(s => s != SiteContext.Current.Site.Id))
            {
                var urlHelper = new UrlHelper(filterContext.HttpContext.Request.RequestContext);

                filterContext.Result = new RedirectResult(urlHelper.SiteDefaultUrl(user));
            }
        }
    }
}