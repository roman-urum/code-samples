namespace Maestro.Domain.Dtos.HealthLibraryService
{
    /// <summary>
    /// The model for search program result.
    /// </summary>
    public class SearchProgramResultDto : SearchEntryDto
    {
        /// <summary>
        /// Gets ot set the duration in days
        /// </summary>
        /// <value>
        /// The duration in days
        /// </value>
        public int DurationDays { get; set; }
    }
}
