using System;
using System.Collections.Generic;
using System.Linq;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Enums;

namespace Maestro.Web.Security
{
    /// <summary>
    /// Container to store info about user access rights.
    /// </summary>
    [Serializable]
    public class PermissionsAuthData
    {
        /// <summary>
        /// Id of customer which accessible for current user.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Subdomain of user customer.
        /// </summary>
        public string Subdomain { get; set; }

        /// <summary>
        /// Permissions of user in customer.
        /// </summary>
        public IEnumerable<CustomerUserRolePermissions> Permissions { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PermissionsAuthData()
        {
        }

        /// <summary>
        /// Initializes permissions data by customer role.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="customerSubdomain"></param>
        public PermissionsAuthData(CustomerUserRole role, string customerSubdomain)
        {
            if (!role.CustomerId.HasValue)
            {
                throw new Exception("Customer id is required for permissions auth data");
            }

            if (string.IsNullOrEmpty(customerSubdomain))
            {
                throw new ArgumentNullException("customerSubdomain");
            }

            CustomerId = role.CustomerId.Value;
            Subdomain = customerSubdomain;
            Permissions = role.Permissions.Select(p => p.PermissionCode).ToList();
        }
    }
}