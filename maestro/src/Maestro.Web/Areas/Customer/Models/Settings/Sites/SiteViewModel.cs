using System;

namespace Maestro.Web.Areas.Customer.Models.Settings.Sites
{
    /// <summary>
    /// SiteViewModel.
    /// </summary>
    public class SiteViewModel : CreateUpdateSiteViewModel
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