using System;

namespace Maestro.Web.Areas.Customer.Models.Settings.Sites
{
    /// <summary>
    /// CreateUpdateOrganizationViewModel.
    /// </summary>
    public class CreateUpdateOrganizationViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent organization identifier.
        /// </summary>
        /// <value>
        /// The parent organization identifier.
        /// </value>
        public Guid? ParentOrganizationId { get; set; }
    }
}