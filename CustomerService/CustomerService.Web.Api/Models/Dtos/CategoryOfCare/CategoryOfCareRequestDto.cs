using System.ComponentModel.DataAnnotations;
using CustomerService.Web.Api.Resources;

namespace CustomerService.Web.Api.Models.Dtos.CategoryOfCare
{
    /// <summary>
    /// CategoryOfCareRequestDto.
    /// </summary>
    public class CategoryOfCareRequestDto
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
        [RegularExpression(@"^[^!@#$%\^&]+$",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RegularExpressionAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Name { get; set; }
    }
}