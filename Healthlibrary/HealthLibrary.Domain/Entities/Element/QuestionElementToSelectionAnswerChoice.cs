using System;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// Contains selection answer choice ids and scores for questions.
    /// </summary>
    public class QuestionElementToSelectionAnswerChoice : IAnalyticsEntity
    {
        /// <summary>
        /// Id of answer set.
        /// </summary>
        public Guid QuestionElementId { get; set; }

        /// <summary>
        /// Id of answer choice.
        /// </summary>
        public Guid SelectionAnswerChoiceId { get; set; }

        /// <summary>
        /// Gets or sets the external id.
        /// </summary>
        /// <value>
        /// String.
        /// </value>
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the internal id.
        /// </summary>
        /// <value>
        /// String.
        /// </value>
        public string InternalId { get; set; }

        /// <summary>
        /// Gets or sets the External score.
        /// </summary>
        /// <value>
        /// Int.
        /// </value>
        public int? ExternalScore { get; set; }

        /// <summary>
        /// Gets or sets the External score.
        /// </summary>
        /// <value>
        /// Int.
        /// </value>
        public int? InternalScore { get; set; }

        /// <summary>
        /// Gets or sets the question element.
        /// </summary>
        public virtual QuestionElement QuestionElement { get; set; }

        /// <summary>
        /// Gets or sets the SelectionAnswerChoice.
        /// </summary>
        public virtual SelectionAnswerChoice SelectionAnswerChoice { get; set; }
    }
}