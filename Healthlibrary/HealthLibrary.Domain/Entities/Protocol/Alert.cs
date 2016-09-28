using System;
using System.Collections.Generic;

namespace HealthLibrary.Domain.Entities.Protocol
{
    /// <summary>
    /// Alert.
    /// </summary>
    public class Alert : Entity
    {
        /// <summary>
        /// Gets or sets the alert severity identifier.
        /// </summary>
        /// <value>
        /// The alert severity identifier.
        /// </value>
        public Guid? AlertSeverityId { get; set; }

        /// <summary>
        /// Gets or sets the protocol element identifier.
        /// </summary>
        /// <value>
        /// The protocol element identifier.
        /// </value>
        public Guid ProtocolElementId { get; set; }

        /// <summary>
        /// Gets or sets the protocol element.
        /// </summary>
        /// <value>
        /// The protocol element.
        /// </value>
        public virtual ProtocolElement ProtocolElement { get; set; }

        /// <summary>
        /// Gets or sets the conditions.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        public virtual ICollection<Condition> Conditions { get; set; }
    }
}