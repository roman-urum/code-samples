using System;
using System.Collections.Generic;

namespace HealthLibrary.Web.Api.Models.Protocols
{
    /// <summary>
    /// BranchDto.
    /// </summary>
    public class BranchDto
    {
        /// <summary>
        /// Gets or sets the next protocol element identifier.
        /// </summary>
        /// <value>
        /// The next protocol element identifier.
        /// </value>
        public Guid? NextProtocolElementId { get; set; }

        /// <summary>
        /// Gets or sets the threshold alert severity identifier.
        /// </summary>
        /// <value>
        /// The threshold alert severity identifier.
        /// </value>
        public Guid? ThresholdAlertSeverityId { get; set; }

        /// <summary>
        /// Gets or sets the conditions.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        public IList<ConditionDto> Conditions { get; set; }
    }
}