using System;
using VitalsService.Web.Api.Models.Alerts;

namespace VitalsService.Web.Api.Models
{
    /// <summary>
    /// VitalResponseDto.
    /// </summary>
    public class VitalResponseDto : VitalDto
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
        public VitalAlertResponseDto VitalAlert { get; set; }
    }
}