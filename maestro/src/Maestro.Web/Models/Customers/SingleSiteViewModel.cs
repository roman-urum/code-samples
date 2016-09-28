namespace Maestro.Web.Models.Customers
{
    /// <summary>
    /// SingleSiteViewModel.
    /// </summary>
    /// <seealso cref="Maestro.Web.Models.Customers.BaseCustomerViewModel" />
    public class SingleSiteViewModel : BaseCustomerViewModel
    {
        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        public CustomerWebsiteViewModel Website { get; set; }
    }
}