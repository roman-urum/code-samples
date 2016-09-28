using System;

namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// The model for posting health session to ESB
    /// </summary>
    public class HealthSessionEsbDto : HealthSessionRequestDto
    {
        /// <summary>
        /// Gets or sets the health session identifier.
        /// </summary>
        /// <value>
        /// The health session identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Get or sets the patient identifier.
        /// </summary>
        public Guid PatientId { get; set; }
    }
}