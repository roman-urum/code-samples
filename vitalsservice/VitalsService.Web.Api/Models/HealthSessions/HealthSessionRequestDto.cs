using System.Collections.Generic;
using VitalsService.Web.Api.DataAnnotations;

namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// Model for request to create new health session record.
    /// </summary>
    public class HealthSessionRequestDto : BaseHealthSessionDto
    {
        /// <summary>
        /// The actual elements presented to the patient.
        /// </summary>
        [ItemsRequired]
        public IList<HealthSessionElementRequestDto> Elements { get; set; }
    }
}