using System;
using System.Linq;
using System.Web.Mvc;
using Maestro.Domain.Constants;
using Maestro.Domain.Enums;
using Maestro.Web.Extensions;
using Maestro.Web.Helpers;
using Maestro.Web.Security;

namespace Maestro.Web.Areas.Customer
{
    /// <summary>
    /// Attribute to set access by customer user roles permissions.
    /// Works only for customer users in customer area.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomerAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly CustomerUserRolePermissions[] permissions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        public CustomerAuthorizeAttribute(params CustomerUserRolePermissions[] permissions)
        {
            if (permissions.Any())
            {
                this.permissions = permissions;
            }
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var principal = filterContext.HttpContext.User as IMaestroPrincipal;

            if (principal == null || !principal.IsInRole(Roles.CustomerUser))
            {
                return;
            }

            if (permissions == null)
            {
                ValidateByPagePermissions(filterContext, principal);

                return;
            }

            if (!principal.HasPermissions(CustomerContext.Current.Customer.Id, permissions))
            {
                filterContext.Result = new RedirectResult(WebSettings.LoginPage);
            }
        }

        /// <summary>
        /// Validates access rights assigned to page route.
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="user"></param>
        private void ValidateByPagePermissions(ActionExecutingContext filterContext, IMaestroPrincipal user)
        {
            var helper = new PagePermissionsHelper(user);

            if (!helper.IsAvailable(
                filterContext.ActionDescriptor.ActionName,
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.HttpContext.Request.RequestContext.RouteData.GetArea()))
            {
                filterContext.Result = new RedirectResult(WebSettings.LoginPage);
            }
        }
    }
}