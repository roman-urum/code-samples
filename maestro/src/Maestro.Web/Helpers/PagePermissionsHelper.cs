using System.Collections.Generic;
using System.Linq;
using Maestro.Domain.Constants;
using Maestro.Domain.Enums;
using Maestro.Web.Security;

namespace Maestro.Web.Helpers
{
    /// <summary>
    /// Helper to verify access to pages and links in UI.
    /// </summary>
    public class PagePermissionsHelper
    {
        private readonly IMaestroPrincipal user;
        private static readonly List<PagePermissions> PagesPermissionsMap = new List<PagePermissions>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PagePermissionsHelper"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public PagePermissionsHelper(IMaestroPrincipal user)
        {
            this.user = user;
        }

        /// <summary>
        /// Static method to create permissions map for specified page.
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="areaName"></param>
        /// <param name="permissions"></param>
        public static void RegisterPage(
            string actionName, 
            string controllerName, 
            string areaName,
            params CustomerUserRolePermissions[] permissions
        )
        {
            var pagePermissionsModel = new PagePermissions(actionName, controllerName, areaName, permissions);

            PagesPermissionsMap.Add(pagePermissionsModel);
        }

        /// <summary>
        /// Validates if current user has access to specified customer page.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        public bool IsAvailable(string action, string controller, string area)
        {
            if (CustomerContext.Current.Customer == null)
            {
                return false;
            }

            if (user.IsInRole(Roles.SuperAdmin))
            {
                return true;
            }

            var pagePermissions = PagesPermissionsMap.FirstOrDefault(p => p.EqualsRoute(action, controller, area));

            if (pagePermissions == null)
            {
                return true;
            }

            return user.HasPermissions(CustomerContext.Current.Customer.Id, pagePermissions.Permissions.ToArray());
        }
    }
}