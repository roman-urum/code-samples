using System;
using System.Collections.Generic;

namespace HealthLibrary.Domain.Entities.Protocol
{
    /// <summary>
    /// Branch.
    /// </summary>
    public class Branch : Entity
    {
        /// <summary>
        /// Gets or sets the threshold alert severity identifier.
        /// </summary>
        /// <value>
        /// The threshold alert severity identifier.
        /// </value>
        public Guid? ThresholdAlertSeverityId { get; set; }

        /// <summary>
        /// Gets or sets the protocol element identifier.
        /// </summary>
        /// <value>
        /// The protocol element identifier.
        /// </value>
        public virtual Guid ProtocolElementId { get; set; }

        /// <summary>
        /// Gets or sets the protocol element.
        /// </summary>
        /// <value>
        /// The protocol element.
        /// </value>
        public virtual ProtocolElement ProtocolElement { get; set; }

        /// <summary>
        /// Gets or sets the next protocol element identifier.
        /// </summary>
        /// <value>
        /// The next protocol element identifier.
        /// </value>
        public virtual Guid? NextProtocolElementId { get; set; }

        /// <summary>
        /// Gets or sets the next protocol element.
        /// </summary>
        /// <value>
        /// The next protocol element.
        /// </value>
        public virtual ProtocolElement NextProtocolElement { get; set; }

        /// <summary>
        /// Gets or sets the conditions.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        public virtual ICollection<Condition> Conditions { get; set; }
    }
}