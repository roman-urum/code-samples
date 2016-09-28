using System.Collections.Generic;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Elements.QuestionElements
{
    /// <summary>
    /// Container for data of get question response.
    /// </summary>
    public class QuestionElementResponseDto : ElementDto
    {
        /// <summary>
        /// Answer set tags.
        /// </summary>
        public IList<string> Tags { get; set; }

        /// <summary>
        /// External question id.
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Internal question id.
        /// </summary>
        public string InternalId { get; set; }

        /// <summary>
        /// Ids for answer choices in context of question.
        /// </summary>
        public IEnumerable<AnswerChoiceIdDto> AnswerChoiceIds { get; set; }

        /// <summary>
        /// Localized string with question and audio prompt.
        /// </summary>
        public LocalizedStringWithAudioFileMediaResponseDto QuestionElementString { get; set; }

        /// <summary>
        /// Gets or sets the answer set.
        /// </summary>
        /// <value>
        /// The answer set.
        /// </value>
        public OpenEndedAnswerSetResponseDto AnswerSet { get; set; }
    }
}