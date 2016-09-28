using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VitalsService.Web.Api.DataAnnotations;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.PatientNotes
{
    /// <summary>
    /// The base model for note
    /// </summary>
    public class BaseNoteDto
    {
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
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
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError"
        )]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [StringLength(
            1000,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError"
        )]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the notables.
        /// </summary>
        /// <value>
        /// The notables.
        /// </value>
        [UniqueList(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "UniqueListAttribute_ValidationError",
            ErrorMessage = null
        )]
        [ElementStringLength(
            1,
            50,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "ElementStringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public IList<string> Notables { get; set; }
    }
}