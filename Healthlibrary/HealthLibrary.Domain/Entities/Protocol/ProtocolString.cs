using System;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.Domain.Entities.Protocol
{
    /// <summary>
    /// ProtocolString.
    /// </summary>
    public class ProtocolString : LocalizedString
    {
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
    }
}