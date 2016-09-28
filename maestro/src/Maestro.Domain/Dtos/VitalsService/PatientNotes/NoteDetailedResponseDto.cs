namespace Maestro.Domain.Dtos.VitalsService.PatientNotes
{
    /// <summary>
    /// Model for detailed note response
    /// </summary>
    public class NoteDetailedResponseDto : BaseNoteResponseDto
    {
        /// <summary>
        /// Gets or sets the vital identifier.
        /// </summary>
        /// <value>
        /// The vital identifier.
        /// </value>
        public MeasurementBriefResponseDto Measurement { get; set; }

        /// <summary>
        /// Gets or sets the health session element identifier.
        /// </summary>
        /// <value>
        /// The health session element identifier.
        /// </value>
        public HealthSessionElementDto HealthSessionElement { get; set; }
    }
}