using System;

namespace Maestro.Web.Areas.Customer.Models.Settings.Sites
{
    /// <summary>
    /// OrganizationViewModel.
    /// </summary>
    public class OrganizationViewModel : CreateUpdateOrganizationViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }
    }
}