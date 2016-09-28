namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// MeasurementValueResponseDto.
    /// </summary>
    public class MeasurementValueResponseDto : HealthSessionElementValueDto
    {
        /// <summary>
        /// Measurement data.
        /// </summary>
        public MeasurementResponseDto Value { get; set; }
    }
}