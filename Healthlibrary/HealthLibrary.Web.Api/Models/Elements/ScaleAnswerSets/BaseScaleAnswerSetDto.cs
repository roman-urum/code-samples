using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FoolproofWebApi;
using HealthLibrary.Domain.Constants;
using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Elements.ScaleAnswerSets
{
    /// <summary>
    /// BaseScaleAnswerSetDto.
    /// </summary>
    public class BaseScaleAnswerSetDto
    {
        /// <summary>
        /// Gets or sets the low value.
        /// </summary>
        /// <value>
        /// The low value.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [Range(
            0,
            100,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "BaseScaleAnswerSetDto_LowValue_Bounds"
        )]
        [LessThan(
            "HighValue",
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "BaseScaleAnswerSetDto_LowValue_Low_value_must_be_less_then_high_value"
        )]
        public int LowValue { get; set; }

        /// <summary>
        /// Gets or sets the high value.
        /// </summary>
        /// <value>
        /// The high value.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [Range(
            0,
            100,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "BaseScaleAnswerSetDto_HighValue_Bounds"
        )]
        [GreaterThan(
            "LowValue",
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "BaseScaleAnswerSetDto_HighValue_Greater"
        )]
        public int HighValue { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null,
            AllowEmptyStrings = false
        )]
        [MaxLength(
            100,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "BaseScaleAnswerSetDto_Name_is_too_long"
        )]
        public string Name { get; set; }

        /// <summary>
        /// Answer set tags.
        /// </summary>
        [ElementStringLength(
            30,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        [UniqueStringsList(
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "UniqueStringsListAttribute_ValidationError",
            ErrorMessage = null
        )]
        [ListRegularExpression(
            ValidationExpressions.Tag,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "TagsValidationMessage",
            ErrorMessage = null
        )]
        public IList<string> Tags { get; set; }
    }
}