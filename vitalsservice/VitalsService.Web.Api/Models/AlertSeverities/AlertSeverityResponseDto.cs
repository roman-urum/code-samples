using System;

namespace VitalsService.Web.Api.Models.AlertSeverities
{
    /// <summary>
    /// AlertSeverityResponseDto.
    /// </summary>
    public class AlertSeverityResponseDto : AlertSeverityRequestDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }
    }
}