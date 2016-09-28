using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Maestro.Domain.Enums;
using NLog;

namespace Maestro.Web.Security
{
    /// <summary>
    /// Implementation of custom Maestro principal
    /// </summary>
    public class MaestroPrincipal : IMaestroPrincipal
    {
        private readonly string role;
        private readonly string customerRole;
        private readonly PermissionsAuthData permissionsAuthData;

        public IIdentity Identity { get; private set; }

        /// <summary>
        /// Returns subdomain of customer to which current user assigned.
        /// </summary>
        public string CustomerSubdomain
        {
            get { return permissionsAuthData == null ? null : permissionsAuthData.Subdomain; }
        }

        /// <summary>
        /// Contains list of accessible sites ids.
        /// </summary>
        public IEnumerable<Guid> Sites { get; private set; }

        public MaestroPrincipal(UserAuthData userAuthData)
        {
            if (userAuthData == null)
            {
                var eventLogger = LogManager.GetCurrentClassLogger();
                eventLogger.Info("userauthdata is null :(");
                Identity = new MaestroIdentity();
            }
            else
            {
                Identity = new MaestroIdentity(userAuthData.FirstName);
                this.role = userAuthData.Role;
                this.customerRole = userAuthData.CustomerRole;
                this.permissionsAuthData = userAuthData.Permissions;
                this.Sites = userAuthData.Sites;
                Id = userAuthData.UserId;
            }
        }

        public Guid Id { get; set; }

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <param name="role">The name of the role for which to check membership.</param>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        public bool IsInRole(string role)
        {
            return this.IsInRoles(role);
        }

        /// <summary>
        /// Determines if authenticated user has one of specified roles.
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool IsInRoles(params string[] roles)
        {
            return roles.Contains(role);
        }

        public bool IsInCustomerRole(string customerRole)
        {
            return this.IsInCustomerRoles(customerRole);
        }

        /// <summary>
        /// Determines if authenticated user has one of specified customer roles.
        /// </summary>
        /// <param name="customerRoles"></param>
        /// <returns></returns>
        public bool IsInCustomerRoles(params string[] customerRoles)
        {
            return customerRoles.Contains(customerRole);
        }

        /// <summary>
        /// Determines if authenticated user has access to specified functionality in customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public bool HasPermissions(int customerId, params CustomerUserRolePermissions[] permissions)
        {
            if (permissionsAuthData == null)
            {
                return false;
            }

            return this.permissionsAuthData.CustomerId == customerId &&
                   this.permissionsAuthData.Permissions.Intersect(permissions).Any();
        }
    }
}