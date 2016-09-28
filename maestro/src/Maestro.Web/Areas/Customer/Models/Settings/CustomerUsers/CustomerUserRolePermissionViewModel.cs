using System;
using Maestro.Domain.Constants;
using Maestro.Domain.DbEntities;

namespace Maestro.Web.Areas.Customer.Models.Settings.CustomerUsers
{
    /// <summary>
    /// CustomerUserRolePermissionViewModel.
    /// </summary>
    public class CustomerUserRolePermissionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUserRolePermissionViewModel"/> class.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public CustomerUserRolePermissionViewModel(CustomerUserRoleToPermissionMapping mapping)
        {
            if (mapping == null)
            {
                throw new ArgumentNullException();
            }

            PermissionInfo permission;

            if (PermissionInfo.Infos.TryGetValue(mapping.PermissionCode, out permission))
            {
                Name = permission.Name;
                Description = permission.Description;
                Category = permission.Category;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }
    }
}