using System.Collections.Generic;

using Maestro.Web.DataAnnotations;

namespace Maestro.Web.Models.Customers
{
    /// <summary>
    /// CreateCustomerViewModel.
    /// </summary>
    public class CreateCustomerViewModel
    {
        //[RemoteWithoutArea("IsCustomerNameExists", "Customers",
        //    ErrorMessageResourceType = typeof(GlobalStrings),
        //    ErrorMessageResourceName = "Edit_Customer_NameExistsMessage")]
        [RequiredLocalized("Create_Customer_CusromerNameRequiredError")]
        [RegularExpressionLocalized(@"^[^!@#$%\^&]+$", "Create_Customer_CustomerNameSpecCharactersError")]
        [DisplayNameLocalized("Create_Customer_CustomerNameFieldTitle")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the subdomain.
        /// </summary>
        /// <value>
        /// The name of the subdomain.
        /// </value>
        [RequiredLocalized("Create_Customer_SubdomainNameFieldRequiredError")]
        [RegularExpressionLocalized(@"^[a-vx-zA-VX-Z0-9][a-vx-zA-VX-Z0-9-]+[a-vx-zA-VX-Z0-9]$", "Create_Customer_SubDomainNameFieldSpecCharactersError")]
        [DisplayNameLocalized("Create_Customer_SubdomainNameFieldTitle")]        
        public string SubdomainName { get; set; }

        /// <summary>
        /// Gets or sets the first name of the site.
        /// </summary>
        /// <value>
        /// The first name of the site.
        /// </value>
        [RegularExpressionLocalized(@"^[^!@#$%\^&]+$", "Create_Customer_FirstSiteValidationError")]
        [RequiredLocalized("Create_Customer_FirstSiteNameFieldRequiredError")]
        [DisplayNameLocalized("Create_Customer_SiteNameFieldTitle")]
        public string FirstSiteName { get; set; }

        /// <summary>
        /// Gets or sets the contact phone.
        /// </summary>
        /// <value>
        /// The contact phone.
        /// </value>
        [RegularExpressionLocalized(@"^[^!@#$%\^&]+$", "Create_Customer_ContactPhoneFieldSpecCharactersError")]
        [DisplayNameLocalized("Create_Customer_ContactPhoneFieldTitle")]
        public string ContactPhone { get; set; }
    }
}