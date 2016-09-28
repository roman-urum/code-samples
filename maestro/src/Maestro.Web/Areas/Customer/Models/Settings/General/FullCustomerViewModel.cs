using System.Collections.Generic;
using Maestro.Web.Areas.Customer.Models.Settings.Sites;

namespace Maestro.Web.Areas.Customer.Models.Settings.General
{
    /// <summary>
    /// Includes lists with customer sites and organizations.
    /// </summary>
    public class FullCustomerViewModel : GeneralSettingsViewModel
    {
        /// <summary>
        /// Models of customer organizations.
        /// </summary>
        public IList<OrganizationViewModel> Organizations { get; set; }

        /// <summary>
        /// Models of customer sites.
        /// </summary>
        public IList<SiteViewModel> Sites { get; set; }
    }
}