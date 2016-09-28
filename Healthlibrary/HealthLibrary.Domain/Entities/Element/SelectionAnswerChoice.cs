using System;
using System.Collections.Generic;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// SelectionAnswerChoice.
    /// </summary>
    public class SelectionAnswerChoice : Entity
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is open ended.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is open ended; otherwise, <c>false</c>.
        /// </value>
        public bool IsOpenEnded { get; set; }

        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public int Sort { get; set; }

        /// <summary>
        /// Gets or sets the selection answer set identifier.
        /// </summary>
        /// <value>
        /// The selection answer set identifier.
        /// </value>
        public virtual Guid SelectionAnswerSetId { get; set; }

        /// <summary>
        /// Gets or sets the selection answer set.
        /// </summary>
        /// <value>
        /// The selection answer set.
        /// </value>
        public virtual SelectionAnswerSet SelectionAnswerSet { get; set; }

        /// <summary>
        /// Gets or sets the localized strings.
        /// </summary>
        /// <value>
        /// The localized strings.
        /// </value>
        public virtual ICollection<SelectionAnswerChoiceString> LocalizedStrings { get; set; }

        /// <summary>
        /// Gets or sets the map between answer sets and selection answer choices.
        /// </summary>
        public virtual ICollection<QuestionElementToSelectionAnswerChoice> QuestionElementToSelectionAnswerChoices { get; set; } 
    }
}