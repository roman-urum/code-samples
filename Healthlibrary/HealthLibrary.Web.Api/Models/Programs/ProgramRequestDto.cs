using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthLibrary.Domain.Constants;
using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Programs
{
    /// <summary>
    /// ProgramRequestDto.
    /// </summary>
    public class ProgramRequestDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null,
            AllowEmptyStrings = false
        )]
        [MaxLength(
            100,
            ErrorMessageResourceName = "ProgramDto_Name_ValidationError",
            ErrorMessageResourceType = typeof (GlobalStrings)
        )]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
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

        /// <summary>
        /// Gets or sets the program elements.
        /// </summary>
        /// <value>
        /// The program elements.
        /// </value>
        [NotEmptyList(
            ErrorMessageResourceName = "NotEmptyListAttribute_ValidationError",
            ErrorMessageResourceType = typeof (GlobalStrings)
        )]
        public ICollection<ProgramElementDto> ProgramElements { get; set; }

        /// <summary>
        /// Gets or sets the recurrences.
        /// </summary>
        /// <value>
        /// The recurrences.
        /// </value>
        public ICollection<RecurrenceDto> Recurrences { get; set; }
    }
}