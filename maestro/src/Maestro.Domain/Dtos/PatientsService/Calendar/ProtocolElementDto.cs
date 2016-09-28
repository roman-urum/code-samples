using System;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// ProtocolElementDto.
    /// </summary>
    public class ProtocolElementDto
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
