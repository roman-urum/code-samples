using System;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// SelectionAnswerChoiceString.
    /// </summary>
    public class SelectionAnswerChoiceString : LocalizedString
    {
        /// <summary>
        /// Gets or sets the selection answer choice identifier.
        /// </summary>
        /// <value>
        /// The selection answer choice identifier.
        /// </value>
        public Guid SelectionAnswerChoiceId { get; set; }

        /// <summary>
        /// Gets or sets the selection answer choice.
        /// </summary>
        /// <value>
        /// The selection answer choice.
        /// </value>
        public virtual SelectionAnswerChoice SelectionAnswerChoice { get; set; }
    }
}