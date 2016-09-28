using System;
using VitalsService.Web.Api.Models.Alerts;

namespace VitalsService.Web.Api.Models
{
    /// <summary>
    /// VitalBriefResponseDto.
    /// </summary>
    public class VitalBriefResponseDto : VitalDto
    {
        /// <summary>
        /// Vital unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the vital alert.
        /// </summary>
        /// <value>
        /// The vital alert.
        /// </value>
        public VitalAlertBriefResponseDto VitalAlert { get; set; }
    }
}