using System.Collections.Generic;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.QuestionElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols
{
    /// <summary>
    /// QuestionProtocolElementResponseDto.
    /// </summary>
    public class QuestionProtocolElementResponseViewModel : ElementResponseViewModel
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
        public IList<AnswerChoiceIdDto> AnswerChoiceIds { get; set; }

        /// <summary>
        /// Localized string with question and audio prompt.
        /// </summary>
        public BaseLocalizedStringDto QuestionElementString { get; set; }

        /// <summary>
        /// Gets or sets the answer set.
        /// </summary>
        /// <value>
        /// The answer set.
        /// </value>
        public AnswerSetElementResponseViewModel AnswerSet { get; set; }
    }
}