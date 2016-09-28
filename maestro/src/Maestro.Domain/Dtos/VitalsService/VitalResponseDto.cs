using Maestro.Domain.Dtos.VitalsService.Alerts;

namespace Maestro.Domain.Dtos.VitalsService
{
    /// <summary>
    /// VitalResponseDto.
    /// </summary>
    public class VitalResponseDto : VitalDto
    {
        /// <summary>
        /// Gets or sets the vital alert.
        /// </summary>
        /// <value>
        /// The vital alert.
        /// </value>
        public VitalAlertResponseDto VitalAlert { get; set; }
    }
}