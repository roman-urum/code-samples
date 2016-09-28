namespace Maestro.Domain.Dtos.VitalsService
{
    /// <summary>
    /// MeasurementValueResponseDto.
    /// </summary>
    public class MeasurementValueResponseDto : HealthSessionElementValueDto
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public MeasurementDto Value { get; set; }
    }
}