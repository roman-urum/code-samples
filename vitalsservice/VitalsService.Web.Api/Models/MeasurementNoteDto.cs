using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Constants;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models
{
    /// <summary>
    /// MeasurementNoteDto.
    /// </summary>
    public class MeasurementNoteDto
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [MaxLength(
            DbConstraints.MaxLength.NoteTitle,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [MaxLength(
            DbConstraints.MaxLength.NoteText,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Text { get; set; }
    }
}