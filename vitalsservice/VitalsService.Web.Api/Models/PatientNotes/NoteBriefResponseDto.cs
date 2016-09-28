using System;

namespace VitalsService.Web.Api.Models.PatientNotes
{
    /// <summary>
    /// NoteDto.
    /// </summary>
    public class NoteBriefResponseDto : NoteRequestDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created UTC.
        /// </summary>
        /// <value>
        /// The created UTC.
        /// </value>
        public DateTime CreatedUtc { get; set; }
    }
}