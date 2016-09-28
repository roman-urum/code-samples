using System;
using System.Collections.Generic;
using Maestro.Domain.Enums;

namespace Maestro.Web.Security
{
    /// <summary>
    /// Container for route permissions.
    /// </summary>
    public class PagePermissions
    {
        public string Action { get; private set; }

        public string Controller { get; private set; }

        public string Area { get; private set; }

        public IEnumerable<CustomerUserRolePermissions> Permissions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagePermissions"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="area">The area.</param>
        /// <param name="permissions">The permissions.</param>
        /// <exception cref="System.ArgumentNullException">
        /// action
        /// or
        /// controller
        /// </exception>
        public PagePermissions(
            string action,
            string controller, 
            string area,
            IEnumerable<CustomerUserRolePermissions> permissions
        )
        {
            if (string.IsNullOrEmpty(action))
            {
                throw new ArgumentNullException("action");
            }

            if (string.IsNullOrEmpty(controller))
            {
                throw new ArgumentNullException("controller");
            }

            this.Action = action;
            this.Controller = controller;
            this.Area = area;
            this.Permissions = permissions;
        }

        /// <summary>
        /// Verifies that route data specified in entity matches to
        /// data specified in method attributes.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        public bool EqualsRoute(string action, string controller, string area)
        {
            return this.Action.Equals(action) && this.Controller.Equals(controller) && Area.Equals(area);
        }
    }
}