using System;

namespace VitalsService.Web.Api.Models.PatientNotes
{
    /// <summary>
    /// NoteRequestDto.
    /// </summary>
    public class NoteRequestDto: BaseNoteDto
    {
        /// <summary>
        /// Gets or sets the vital identifier.
        /// </summary>
        /// <value>
        /// The vital identifier.
        /// </value>
        public Guid? MeasurementId { get; set; }

        /// <summary>
        /// Gets or sets the health session element identifier.
        /// </summary>
        /// <value>
        /// The health session element identifier.
        /// </value>
        public Guid? HealthSessionElementId { get; set; }
    }
}