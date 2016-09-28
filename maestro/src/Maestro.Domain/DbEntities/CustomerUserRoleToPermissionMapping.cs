using System;
using Maestro.Domain.Enums;

namespace Maestro.Domain.DbEntities
{
    /// <summary>
    /// CustomerRoleToPermissionMapping.
    /// </summary>
    public class CustomerUserRoleToPermissionMapping : Entity
    {
        /// <summary>
        /// Gets or sets the customer role identifier.
        /// </summary>
        /// <value>
        /// The customer role identifier.
        /// </value>
        public Guid CustomerUserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the permission code.
        /// </summary>
        /// <value>
        /// The permission code.
        /// </value>
        public CustomerUserRolePermissions PermissionCode { get; set; }

        /// <summary>
        /// Gets or sets the customer role.
        /// </summary>
        /// <value>
        /// The customer role.
        /// </value>
        public virtual CustomerUserRole CustomerUserRole { get; set; }
    }
}