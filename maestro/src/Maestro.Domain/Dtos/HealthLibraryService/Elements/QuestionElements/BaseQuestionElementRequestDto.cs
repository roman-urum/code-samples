using System;
using System.Collections.Generic;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.QuestionElements
{
    /// <summary>
    /// Contains base properties for question element dtos.
    /// </summary>
    public abstract class BaseQuestionElementRequestDto
    {
        /// <summary>
        /// Reference to answer set which should be used to answer on question.
        /// </summary>
        public Guid AnswerSetId { get; set; }

        /// <summary>
        /// Answer set tags.
        /// </summary>
        public List<string> Tags { get; set; }

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
        public List<AnswerChoiceIdDto> AnswerChoiceIds { get; set; }
    }
}