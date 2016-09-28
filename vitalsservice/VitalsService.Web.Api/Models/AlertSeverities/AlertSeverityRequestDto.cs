using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Constants;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.AlertSeverities
{
    /// <summary>
    /// AlertSeverityRequestDto.
    /// </summary>
    public class AlertSeverityRequestDto
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
            ErrorMessageResourceName = "RequiredAttribute_ValidationError"
        )]
        [MaxLength(DbConstraints.MaxLength.AlertSeverityName,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color code.
        /// </summary>
        /// <value>
        /// The color code.
        /// </value>
        [RegularExpression(
            @"^#([A-Fa-f0-9]{6})$",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "AlertSeverity_ColorCodeRegExError"
        )]
        public string ColorCode { get; set; }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(GlobalStrings) ,ErrorMessageResourceName = "RequiredAttribute_ValidationError")]
        [Range(
            1,
            int.MaxValue,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "SeverityRangeAttribute_ValidationError"
        )]
        public int Severity { get; set; }
    }
}