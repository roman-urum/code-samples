using System;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// Contains scale answer choice ids and scores for questions.
    /// </summary>
    public class QuestionElementToScaleAnswerChoice : Entity, IAnalyticsEntity
    {
        /// <summary>
        /// Id of answerset.
        /// </summary>
        public Guid QuestionElementId { get; set; }

        /// <summary>
        /// Gets or sets the external id.
        /// </summary>
        /// <value>
        /// String.
        /// </value>
        public string ExternalId { get; set; }

        /// <summary>
        /// Value for which 
        /// </summary>
        public int Value { get; set; }

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
        /// Gets or sets related question element.
        /// </summary>
        public virtual QuestionElement QuestionElement { get; set; }
    }
}