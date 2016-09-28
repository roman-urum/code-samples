using System;
using System.Collections.Generic;

namespace Maestro.Web.Areas.Customer.Models.Settings.CustomerUsers
{
    /// <summary>
    /// CustomerUserRoleViewModel.
    /// </summary>
    public class CustomerUserRoleViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

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
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public ICollection<CustomerUserRolePermissionViewModel> Permissions { get; set; }
    }
}