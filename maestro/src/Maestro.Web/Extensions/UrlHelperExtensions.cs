using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Maestro.Domain.Constants;
using Maestro.Domain.Enums;
using Maestro.Web.Security;
using Microsoft.Practices.ServiceLocation;

namespace Maestro.Web.Extensions
{
    /// <summary>
    /// Extension methods for url helper
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Generates link to action using customer subdomain.
        /// </summary>
        /// <param name="urlHelper"></param>
        /// <param name="subdomain"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static string CustomerAction(this UrlHelper urlHelper, string subdomain, string actionName,
            string controllerName, object routeValues)
        {
            if (string.IsNullOrEmpty(subdomain))
            {
                throw new ArgumentNullException("subdomain");
            }

            var protocol = urlHelper.RequestContext.HttpContext.Request.GetProtocol();
            var host = string.Concat(subdomain, WebSettings.Domain);

            return urlHelper.Action(actionName, controllerName, new RouteValueDictionary(routeValues), protocol, host);
        }

        /// <summary>
        /// Generates link to specified page in site area or to page with site selection.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <returns></returns>
        public static string SiteAction(this UrlHelper urlHelper, string actionName, string controllerName)
        {
            var authData = ServiceLocator.Current.GetInstance<IAuthDataStorage>().GetUserAuthData();
            var customerSites = CustomerContext.Current.Customer.Sites.Where(s => s.IsActive).OrderBy(s => s.Name);

            if (!authData.Role.Equals(Roles.SuperAdmin) && !authData.Sites.Any() ||
                !customerSites.Any())
            {
                return WebSettings.NoAccessSitesPage;
            }

            var routeData = new
            {
                siteId = SiteContext.Current.Site == null
                    ? customerSites.First().Id
                    : SiteContext.Current.Site.Id,
                area = SiteContext.SiteAreaName
            };

            return urlHelper.Action(actionName, controllerName, routeData);
        }

        /// <summary>
        /// Returns url to redirect user after login.
        /// </summary>
        /// <param name="urlHelper"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string SiteDefaultUrl(this UrlHelper urlHelper, IMaestroPrincipal user)
        {
            if (!user.Sites.Any())
            {
                return urlHelper.CustomerAction(user.CustomerSubdomain, "NoAccess", "Sites", null);
            }

            if (user.IsInCustomerRoles(CustomerUserRoles.CustomerAdmin))
            {
                return urlHelper.CustomerAction(user.CustomerSubdomain, "General", "Settings", null);
            }

            if (user.IsInCustomerRoles(CustomerUserRoles.HealthContentManager))
            {
                return urlHelper.CustomerAction(user.CustomerSubdomain, "CareElements", "CareBuilder", null);
            }

            return urlHelper.CustomerAction(
                user.CustomerSubdomain,
                "Index", 
                "Dashboard",
                new { siteId = user.Sites.First(), area = "Site" }
            );
        }
    }
}