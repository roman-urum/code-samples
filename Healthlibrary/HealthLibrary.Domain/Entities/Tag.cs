using System.Collections.Generic;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.Domain.Entities
{
    /// <summary>
    /// Tag.
    /// </summary>
    public class Tag : CustomerAggregateRoot
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        public virtual ICollection<Element.Element> Elements { get; set; }

        /// <summary>
        /// Gets or sets the localized medias.
        /// </summary>
        /// <value>
        /// The localized medias.
        /// </value>
        public virtual ICollection<Media> LocalizedMedias { get; set; }

        /// <summary>
        /// Gets or sets the answer sets.
        /// </summary>
        /// <value>
        /// The answer sets.
        /// </value>
        public virtual ICollection<AnswerSet> AnswerSets { get; set; }

        /// <summary>
        /// Gets or sets the protocols.
        /// </summary>
        /// <value>
        /// The protocols.
        /// </value>
        public virtual ICollection<Protocol.Protocol> Protocols { get; set; }

        /// <summary>
        /// Gets or sets the programs.
        /// </summary>
        /// <value>
        /// The programs.
        /// </value>
        public virtual ICollection<Program.Program> Programs { get; set; }
    }
}