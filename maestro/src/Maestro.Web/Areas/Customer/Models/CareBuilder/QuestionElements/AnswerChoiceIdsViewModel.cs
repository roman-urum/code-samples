using System;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.QuestionElements
{
    /// <summary>
    /// AnswerChoiceIdsModel.
    /// </summary>
    public class AnswerChoiceIdsViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int? Value { get; set; }

        /// <summary>
        /// Gets or sets the internal identifier.
        /// </summary>
        /// <value>
        /// The internal identifier.
        /// </value>
        public string InternalId { get; set; }

        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the internal score.
        /// </summary>
        /// <value>
        /// The internal score.
        /// </value>
        public int? InternalScore { get; set; }

        /// <summary>
        /// Gets or sets the external score.
        /// </summary>
        /// <value>
        /// The external score.
        /// </value>
        public int? ExternalScore { get; set; }
    }
}