using System;
using System.Collections.Generic;
using VitalsService.Web.Api.DataAnnotations;

namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// Model for response of GET health session requests.
    /// </summary>
    public class HealthSessionResponseDto : BaseHealthSessionDto
    {
        /// <summary>
        /// Id of created 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Utc date and time when health session was submitted.
        /// </summary>
        public DateTime SubmittedUtc { get; set; }

        /// <summary>
        /// The actual elements presented to the patient.
        /// </summary>
        [ItemsRequired]
        public IList<HealthSessionElementResponseDto> Elements { get; set; }
    }
}