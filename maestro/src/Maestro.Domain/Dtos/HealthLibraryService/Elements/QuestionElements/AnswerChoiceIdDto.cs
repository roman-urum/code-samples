using System;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.QuestionElements
{
    /// <summary>
    /// Dto to specify ids for selection answer choice.
    /// </summary>
    public class AnswerChoiceIdDto
    {
        /// <summary>
        /// Id of selection answer choice.
        /// Required if value not specified.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Value of scale answer choice.
        /// Required if id not specified.
        /// </summary>
        public int? Value { get; set; }

        /// <summary>
        /// Internal answer choice id.
        /// </summary>
        public string InternalId { get; set; }

        /// <summary>
        /// External answer choice id.
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Internal answer choice score.
        /// </summary>
        public int? InternalScore { get; set; }

        /// <summary>
        /// External answer choice score.
        /// </summary>
        public int? ExternalScore { get; set; }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(InternalId) && string.IsNullOrEmpty(ExternalId)
                   && !InternalScore.HasValue && !ExternalScore.HasValue;
        }
    }
}