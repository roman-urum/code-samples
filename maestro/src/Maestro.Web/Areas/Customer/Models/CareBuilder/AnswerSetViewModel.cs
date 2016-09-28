using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;
using Maestro.Web.DataAnnotations;
using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder
{
    /// <summary>
    /// AnswerSetDto.
    /// </summary>
    public abstract class AnswerSetViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null)]
        [MaxLength(50,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null)]
        public string Name { get; set; }

        /// <summary>
        /// Answer set tags.
        /// </summary>
        [UniqueStringsList(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "UniqueStringsListAttribute_ValidationError",
            ErrorMessage = null
            )]
        public IList<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public AnswerSetType Type { get; set; }
    }
}