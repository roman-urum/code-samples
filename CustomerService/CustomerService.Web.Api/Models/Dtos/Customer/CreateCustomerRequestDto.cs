using System.ComponentModel.DataAnnotations;

using CustomerService.Web.Api.CustomAttributes;
using CustomerService.Web.Api.Resources;

namespace CustomerService.Web.Api.Models.Dtos.Customer
{
    /// <summary>
    /// Create customer request Dto.
    /// </summary>
    public class CreateCustomerRequestDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
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
        /// Gets or sets the subdomain.
        /// </summary>
        /// <value>
        /// The subdomain.
        /// </value>
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [StringLength(
            63,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        [RegularExpression(@"^[a-vx-zA-VX-Z0-9][a-vx-zA-VX-Z0-9-]+[a-vx-zA-VX-Z0-9]$",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RegularExpressionAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Subdomain { get; set; }

        /// <summary>
        /// Gets or sets the logo path.
        /// </summary>
        /// <value>
        /// The logo path.
        /// </value>
        [StringLength(
            1022,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string LogoPath { get; set; }

        /// <summary>
        /// Gets or sets the password expiration days (days).
        /// </summary>
        /// <value>
        /// The password expiration days.
        /// </value>
        [Range(
            1,
            9999,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RangeAttribute_ValidationError",
            ErrorMessage = null
        )]
        public int PasswordExpirationDays { get; set; }

        /// <summary>
        /// Gets or sets the iddle session timeout (minutes).
        /// </summary>
        /// <value>
        /// The iddle session timeout.
        /// </value>
        [Range(
            1,
            9999,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RangeAttribute_ValidationError",
            ErrorMessage = null
        )]
        public int IddleSessionTimeout { get; set; }
    }
}