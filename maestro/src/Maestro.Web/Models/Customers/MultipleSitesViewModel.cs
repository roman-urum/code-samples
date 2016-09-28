using System.Collections.Generic;

namespace Maestro.Web.Models.Customers
{
    /// <summary>
    /// MultipleSitesViewModel.
    /// </summary>
    /// <seealso cref="Maestro.Web.Models.Customers.BaseCustomerViewModel" />
    public class MultipleSitesViewModel : BaseCustomerViewModel
    {
        /// <summary>
        /// Gets or sets the websites.
        /// </summary>
        /// <value>
        /// The websites.
        /// </value>
        public IList<CustomerWebsiteViewModel> Websites { get; set; }
    }
}