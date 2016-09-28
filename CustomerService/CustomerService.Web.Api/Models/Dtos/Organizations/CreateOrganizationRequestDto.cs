using System;
using System.ComponentModel.DataAnnotations;
using CustomerService.Web.Api.Resources;

namespace CustomerService.Web.Api.Models.Dtos.Organizations
{
    /// <summary>
    /// CreateOrganizationRequestDto.
    /// </summary>
    public class CreateOrganizationRequestDto
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
            100,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        [RegularExpression(@"^[^!@#$%\^&\.,]+$",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RegularExpressionAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent organization identifier.
        /// </summary>
        /// <value>
        /// The parent organization identifier.
        /// </value>
        public Guid? ParentOrganizationId { get; set; }
    }
}