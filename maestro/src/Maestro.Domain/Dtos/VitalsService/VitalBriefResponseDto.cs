using Maestro.Domain.Dtos.VitalsService.Alerts;

namespace Maestro.Domain.Dtos.VitalsService
{
    /// <summary>
    /// VitalBriefResponseDto.
    /// </summary>
    public class VitalBriefResponseDto : VitalDto
    {
        /// <summary>
        /// Gets or sets the vital alert.
        /// </summary>
        /// <value>
        /// The vital alert.
        /// </value>
        public VitalAlertBriefResponseDto VitalAlert { get; set; }
    }
}