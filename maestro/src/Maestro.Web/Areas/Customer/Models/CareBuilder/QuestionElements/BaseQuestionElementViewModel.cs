using System;
using System.Collections.Generic;
using Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.QuestionElements
{
    /// <summary>
    /// BaseQuestionElementModel.
    /// </summary>
    public class BaseQuestionElementViewModel
    {
        /// <summary>
        /// Gets or sets the question string.
        /// </summary>
        /// <value>
        /// The question string.
        /// </value>
        public BaseLocalizedStringViewModel QuestionElementString { get; set; }

        /// <summary>
        /// Gets or sets the answer set identifier.
        /// </summary>
        /// <value>
        /// The answer set identifier.
        /// </value>
        public Guid AnswerSetId { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the internal identifier.
        /// </summary>
        /// <value>
        /// The internal identifier.
        /// </value>
        public string InternalId { get; set; }

        /// <summary>
        /// Gets or sets the answer choice ids.
        /// </summary>
        /// <value>
        /// The answer choice ids.
        /// </value>
        public List<AnswerChoiceIdsViewModel> AnswerChoiceIds { get; set; }
    }
}