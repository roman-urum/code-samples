using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthLibrary.Domain.Constants;
using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Elements.SelectionAnswerSets
{
    /// <summary>
    /// Contains base properties for Selection answer set.
    /// </summary>
    public abstract class BaseSelectionAnswerSetDto
    {
        /// <summary>
        /// Name of new answerset.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null,
            AllowEmptyStrings = false
        )]
        [MaxLength(
            50,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Name { get; set; }

        /// <summary>
        /// Type of answer selection.
        /// </summary>
        public bool IsMultipleChoice { get; set; }

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