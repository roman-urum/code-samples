using System;
using System.Collections.Generic;

namespace HealthLibrary.Domain.Entities.Program
{
    /// <summary>
    /// ProgramElement.
    /// </summary>
    public class ProgramElement : Entity
    {
        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public int Sort { get; set; }

        /// <summary>
        /// Gets or sets the program identifier.
        /// </summary>
        /// <value>
        /// The program identifier.
        /// </value>
        public Guid ProgramId { get; set; }

        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public virtual Program Program { get; set; }

        /// <summary>
        /// Gets or sets the protocol identifier.
        /// </summary>
        /// <value>
        /// The protocol identifier.
        /// </value>
        public Guid ProtocolId { get; set; }

        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        /// <value>
        /// The protocol.
        /// </value>
        public virtual Protocol.Protocol Protocol { get; set; }

        /// <summary>
        /// Gets or sets the program day elements.
        /// </summary>
        /// <value>
        /// The program day elements.
        /// </value>
        public virtual ICollection<ProgramDayElement> ProgramDayElements { get; set; }
    }
}