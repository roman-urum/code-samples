namespace HealthLibrary.Web.Api.Models
{
    /// <summary>
    /// Model for search program result.
    /// </summary>
    public class SearchProgramResponseDto : SearchEntryDto
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