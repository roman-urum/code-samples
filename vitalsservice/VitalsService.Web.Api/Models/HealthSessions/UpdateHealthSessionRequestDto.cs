using System;
using System.Collections.Generic;
using VitalsService.Web.Api.DataAnnotations;

namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// The update health session model.
    /// </summary>
    public class UpdateHealthSessionRequestDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is incomplete.
        /// </summary>
        public bool IsIncomplete { get; set; }

        /// <summary>
        /// The UTC date/time this health session was completed by patient
        /// </summary>
        public DateTime? CompletedUtc { get; set; }

        /// <summary>
        /// The elements to be inserted into elements array of health session.
        /// </summary>
        [ItemsRequired]
        public IList<HealthSessionElementRequestDto> Elements { get; set; }
    }
}