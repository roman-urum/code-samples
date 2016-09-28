namespace Maestro.Domain.Dtos.VitalsService.Alerts
{
    /// <summary>
    /// VitalAlertResponseDto.
    /// </summary>
    /// <seealso cref="Maestro.Domain.Dtos.VitalsService.Alerts.VitalAlertBriefResponseDto" />
    /// <seealso cref="Maestro.Domain.IConvertibleVital" />
    public class VitalAlertResponseDto : VitalAlertBriefResponseDto, IConvertibleVital
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