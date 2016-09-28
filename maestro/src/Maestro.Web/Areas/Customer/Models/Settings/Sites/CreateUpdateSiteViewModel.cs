using System;
using System.ComponentModel.DataAnnotations;
using Maestro.Web.DataAnnotations;
using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Customer.Models.Settings.Sites
{
    /// <summary>
    /// CreateUpdateSiteViewModel.
    /// </summary>
    public class CreateUpdateSiteViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [RegularExpressionLocalized(@"^[^!@#$%\^&]+$", "Edit_Customer_SiteNameError")]
        [DisplayNameLocalized("Edit_Customer_DisplayNameFieldLabel")]
        [Required(ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "Edit_Customer_SiteNameRequiredError")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [DisplayNameLocalized("StateFieldLabel")]
        [StringLength(100, ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [DisplayNameLocalized("CityFieldLabel")]
        [StringLength(50, ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the Address1.
        /// </summary>
        /// <value>
        /// The Address1.
        /// </value>
        [DisplayNameLocalized("Address1FieldLabel")]
        [StringLength(250, ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the Address2.
        /// </summary>
        /// <value>
        /// The Address2.
        /// </value>
        [DisplayNameLocalized("Address2FieldLabel")]
        [StringLength(250, ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the Address3.
        /// </summary>
        /// <value>
        /// The Address3.
        /// </value>
        [DisplayNameLocalized("Address3FieldLabel")]
        [StringLength(250, ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [DisplayNameLocalized("ZipCodeFieldLabel")]
        [StringLength(10, ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the National Provider Identificator.
        /// </summary>
        /// <value>
        /// The National Provider Identificator.
        /// </value>
        [DisplayNameLocalized("NationalProviderIdentificatorFieldLabel")]
        [StringLength(100, ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        [RegularExpressionLocalized(@"^[a-zA-Z0-9\-]+$", "NationalProviderIdentificatorFieldSpecCharactersError")]
        public string NationalProviderIdentificator { get; set; }

        /// <summary>
        /// Gets or sets the customer site identifier.
        /// </summary>
        /// <value>
        /// The customer site identifier.
        /// </value>
        [DisplayNameLocalized("CustomerSiteIdFieldLabel")]
        [StringLength(100, ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        [RegularExpressionLocalized(@"^[a-zA-Z0-9\-]+$", "CustomerSiteIdFieldSpecCharactersError")]
        public string CustomerSiteId { get; set; }

        /// <summary>
        /// Gets or sets the contact phone.
        /// </summary>
        /// <value>
        /// The contact phone.
        /// </value>
        [DisplayNameLocalized("Edit_Customer_ContactPhoneFieldLabel")]
        public string ContactPhone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is published.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is published; otherwise, <c>false</c>.
        /// </value>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the organization identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        public Guid? ParentOrganizationId { get; set; }
    }
}