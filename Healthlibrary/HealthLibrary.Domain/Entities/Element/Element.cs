using System.Collections.Generic;
using HealthLibrary.Domain.Entities.Enums;
using HealthLibrary.Domain.Entities.Protocol;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// Base class for all elements.
    /// </summary>
    public abstract class Element : CustomerAggregateRoot
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ElementType Type { get; set; }

        /// <summary>
        /// Identifies if element was deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Reference to related protocol elements.
        /// </summary>
        public virtual ICollection<ProtocolElement> ProtocolElements { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public virtual ICollection<Tag> Tags { get; set; }
    }
}