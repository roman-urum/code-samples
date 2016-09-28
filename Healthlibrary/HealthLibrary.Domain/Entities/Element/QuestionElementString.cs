using System;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// QuestionElementString.
    /// </summary>
    public class QuestionElementString : LocalizedString
    {
        /// <summary>
        /// Gets or sets the question element identifier.
        /// </summary>
        /// <value>
        /// The question element identifier.
        /// </value>
        public Guid QuestionElementId { get; set; }

        /// <summary>
        /// Gets or sets the question element.
        /// </summary>
        /// <value>
        /// The question element.
        /// </value>
        public virtual QuestionElement QuestionElement { get; set; }
    }
}