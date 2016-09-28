using System;

namespace Maestro.Domain.Dtos.PatientsService.DefaultSessions
{
    /// <summary>
    /// Model to transfer data for default session protocols.
    /// </summary>
    public class DefaultSessionProtocolDto
    {
        /// <summary>
        /// Gets or sets the protocol identifier.
        /// </summary>
        /// <value>
        /// The protocol identifier.
        /// </value>
        public Guid ProtocolId { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }
    }
}
