using System;
using System.Collections.Generic;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// QuestionElement.
    /// </summary>
    public class QuestionElement : Element, IBaseAnalyticsEntity
    {
        /// <summary>
        /// Gets or sets the default string identifier.
        /// </summary>
        /// <value>
        /// The default string identifier.
        /// </value>
        public virtual Guid AnswerSetId { get; set; }

        /// <summary>
        /// Gets or sets the internal question id.
        /// </summary>
        public string InternalId { get; set; }

        /// <summary>
        /// Gets or sets the external questino id.
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the localized strings.
        /// </summary>
        /// <value>
        /// The localized strings.
        /// </value>
        public virtual ICollection<QuestionElementString> LocalizedStrings { get; set; }

        /// <summary>
        /// Gets or sets the answer set.
        /// </summary>
        /// <value>
        /// The answer set.
        /// </value>
        public virtual AnswerSet AnswerSet { get; set; }

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public virtual ICollection<QuestionElementToScaleAnswerChoice> QuestionElementToScaleAnswerChoices { get; set; }

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public virtual ICollection<QuestionElementToSelectionAnswerChoice> QuestionElementToSelectionAnswerChoices { get; set; }
    }
}