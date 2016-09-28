using System;
using VitalsService.Web.Api.Models.HealthSessions;

namespace VitalsService.Web.Api.Models.PatientNotes
{
    /// <summary>
    /// The model for detailed note response
    /// </summary>
    public class NoteDetailedResponseDto : BaseNoteDto
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

        /// <summary>
        /// Gets or set the assigned to the note vital
        /// </summary>
        /// <value>
        /// The assigned to the note vital
        /// </value>
        public MeasurementBriefResponseDto Measurement { get; set; }

        /// <summary>
        /// Gets or sets the health session element reading assiged to the note.
        /// </summary>
        /// <value>
        /// The health session element reading assiged to the note.
        /// </value>
        public HealthSessionElementResponseDto HealthSessionElement { get; set; }
    }
}