using System.Collections.Generic;

namespace Maestro.Domain.DbEntities
{
    /// <summary>
    /// CustomerUserRole.
    /// </summary>
    public class CustomerUserRole : Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public virtual ICollection<CustomerUserRoleToPermissionMapping> Permissions { get; set; }

        /// <summary>
        /// Gets or sets the customer users.
        /// </summary>
        /// <value>
        /// The customer users.
        /// </value>
        public virtual ICollection<CustomerUser> CustomerUsers { get; set; }
    }
}