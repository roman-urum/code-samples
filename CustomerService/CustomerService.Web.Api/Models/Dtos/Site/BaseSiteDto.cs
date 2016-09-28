using System;
using System.ComponentModel.DataAnnotations;
using CustomerService.Web.Api.Resources;

namespace CustomerService.Web.Api.Models.Dtos.Site
{
    /// <summary>
    /// Contains common fields for customers' sites.
    /// Used in request to create new customer.
    /// </summary>
    public class BaseSiteDto
    {
        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>
        /// The name of the site.
        /// </value>
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "RequiredAttribute_ValidationError", 
            ErrorMessage = null
        )]
        [StringLength(
            250,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        [RegularExpression(@"^[^!@#$%\^&]+$",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RegularExpressionAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the contact phone.
        /// </summary>
        /// <value>
        /// The contact phone.
        /// </value>
        [StringLength(
            20,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string ContactPhone { get; set; }

        /// <summary>
        /// Gets or sets the parent organization identifier.
        /// </summary>
        /// <value>
        /// The parent organization identifier.
        /// </value>
        public Guid? ParentOrganizationId { get; set; }
    }
}