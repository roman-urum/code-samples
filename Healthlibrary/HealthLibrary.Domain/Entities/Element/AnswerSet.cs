using System.Collections.Generic;
using HealthLibrary.Domain.Entities.Enums;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// Base class for AnswerSet.
    /// </summary>
    public class AnswerSet : CustomerAggregateRoot
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public AnswerSetType Type { get; set; }

        /// <summary>
        /// Flag to mark answer as deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the question elements collection.
        /// </summary>
        /// <value>
        /// The question element.
        /// </value>
        public virtual ICollection<QuestionElement> QuestionElements { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public virtual ICollection<Tag> Tags { get; set; }
    }
}