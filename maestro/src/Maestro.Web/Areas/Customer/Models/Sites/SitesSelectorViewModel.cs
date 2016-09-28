using System.Collections.Generic;
using Maestro.Domain.Dtos.CustomerService;

namespace Maestro.Web.Areas.Customer.Models.Sites
{
    /// <summary>
    /// Model for panel to select active site.
    /// </summary>
    public class SitesSelectorViewModel
    {
        /// <summary>
        /// Name of current active site.
        /// </summary>
        public string CurrentSiteName { get; set; }

        /// <summary>
        /// List of remaining available sites except current.
        /// </summary>
        public IList<SiteResponseDto> AvailableSites { get; set; }
    }
}