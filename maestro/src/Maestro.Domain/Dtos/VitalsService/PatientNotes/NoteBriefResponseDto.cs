using System;

namespace Maestro.Domain.Dtos.VitalsService.PatientNotes
{
    /// <summary>
    /// Model for brief note response
    /// </summary>
    public class NoteBriefResponseDto : BaseNoteResponseDto
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