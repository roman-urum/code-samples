using System;
using System.Collections.Generic;

namespace HealthLibrary.Web.Api.Models.Protocols
{
    /// <summary>
    /// AlertDto.
    /// </summary>
    public class AlertDto
    {
        /// <summary>
        /// Gets or sets the alert severity identifier.
        /// </summary>
        /// <value>
        /// The alert severity identifier.
        /// </value>
        public Guid? AlertSeverityId { get; set; }

        /// <summary>
        /// Gets or sets the conditions.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        public IList<ConditionDto> Conditions { get; set; }
    }
}