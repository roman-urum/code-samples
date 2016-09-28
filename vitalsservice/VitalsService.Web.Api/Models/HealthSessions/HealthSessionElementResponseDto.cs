using System;
using VitalsService.Web.Api.Models.Alerts;

namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// HealthSessionElementResponseDto.
    /// </summary>
    public class HealthSessionElementResponseDto : BaseHealthSessionElementDto
    {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        /// <value>
        /// The identifier
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// The UTC date/time this element was answered.
        /// </summary>
        public DateTime? AnsweredUtc { get; set; }

        /// <summary>
        /// Gets or sets the answered timezone.
        /// </summary>
        /// <value>
        /// The answered timezone.
        /// </value>
        public string AnsweredTz { get; set; }

        /// <summary>
        /// Gets or sets the health session element alert.
        /// </summary>
        /// <value>
        /// The health session element alert.
        /// </value>
        public HealthSessionElementAlertResponseDto HealthSessionElementAlert { get; set; }
    }
}