namespace VitalsService.Domain.Dtos
{
    /// <summary>
    /// AlertSeveritiesSearchDto.
    /// </summary>
    public class AlertSeveritiesSearchDto : BaseSearchDto
    {
        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        public int? Severity { get; set; }
    }
}