using System.Collections.Generic;
using HealthLibrary.Domain.Entities.Program;

namespace HealthLibrary.Domain.Entities.Protocol
{
    /// <summary>
    /// Protocol.
    /// </summary>
    public class Protocol : CustomerAggregateRoot, ISoftDelitable
    {
        /// <summary>
        /// Indicating whether entity deleted or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is private.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is private; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Gets or sets the name localized strings.
        /// </summary>
        /// <value>
        /// The name localized strings.
        /// </value>
        public virtual ICollection<ProtocolString> NameLocalizedStrings { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public virtual ICollection<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the protocol elements.
        /// </summary>
        /// <value>
        /// The protocol elements.
        /// </value>
        public virtual ICollection<ProtocolElement> ProtocolElements { get; set; }

        /// <summary>
        /// Gets or sets the program elements.
        /// </summary>
        /// <value>
        /// The program elements.
        /// </value>
        public virtual ICollection<ProgramElement> ProgramElements { get; set; }
    }
}