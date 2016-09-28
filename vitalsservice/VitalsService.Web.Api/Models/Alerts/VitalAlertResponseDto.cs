namespace VitalsService.Web.Api.Models.Alerts
{
    /// <summary>
    /// VitalAlertResponseDto.
    /// </summary>
    /// <seealso cref="BaseAlertResponseDto" />
    public class VitalAlertResponseDto : VitalAlertBriefResponseDto
    {
        /// <summary>
        /// Gets or sets the measurement.
        /// </summary>
        /// <value>
        /// The measurement.
        /// </value>
        public MeasurementBriefResponseDto Measurement { get; set; }
    }
}