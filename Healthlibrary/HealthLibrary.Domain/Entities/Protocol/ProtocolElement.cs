using System;
using System.Collections.Generic;

namespace HealthLibrary.Domain.Entities.Protocol
{
    /// <summary>
    /// ProtocolElement.
    /// </summary>
    public class ProtocolElement : Entity
    {
        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public int Sort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is first protocol element.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is first protocol element; otherwise, <c>false</c>.
        /// </value>
        public bool IsFirstProtocolElement { get; set; }

        /// <summary>
        /// Gets or sets the protocol identifier.
        /// </summary>
        /// <value>
        /// The protocol identifier.
        /// </value>
        public virtual Guid ProtocolId { get; set; }

        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        /// <value>
        /// The protocol.
        /// </value>
        public virtual Protocol Protocol { get; set; }

        /// <summary>
        /// Gets or sets the element identifier.
        /// </summary>
        /// <value>
        /// The element identifier.
        /// </value>
        public virtual Guid ElementId { get; set; }

        /// <summary>
        /// Gets or sets the element.
        /// </summary>
        /// <value>
        /// The element.
        /// </value>
        public virtual Element.Element Element { get; set; }

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
        /// Gets or sets the branches.
        /// </summary>
        /// <value>
        /// The branches.
        /// </value>
        public virtual ICollection<Branch> Branches { get; set; }

        /// <summary>
        /// Gets or sets the alerts.
        /// </summary>
        /// <value>
        /// The alerts.
        /// </value>
        public virtual ICollection<Alert> Alerts { get; set; }
    }
}