using System.Collections.Generic;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// SelectionAnswerSet.
    /// </summary>
    public class SelectionAnswerSet : AnswerSet
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is multiple choice.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is multiple choice; otherwise, <c>false</c>.
        /// </value>
        public bool IsMultipleChoice { get; set; }

        /// <summary>
        /// Gets or sets the selection answer choices.
        /// </summary>
        /// <value>
        /// The selection answer choices.
        /// </value>
        public virtual ICollection<SelectionAnswerChoice> SelectionAnswerChoices { get; set; }
    }
}