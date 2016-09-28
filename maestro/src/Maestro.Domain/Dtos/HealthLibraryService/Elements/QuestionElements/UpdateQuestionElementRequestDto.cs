using System;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.QuestionElements
{
    /// <summary>
    /// Container for data to update question element.
    /// </summary>
    [JsonObject]
    public class UpdateQuestionElementRequestDto : BaseQuestionElementRequestDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Localized string with question and audio prompt.
        /// </summary>
        public UpdateLocalizedStringRequestDto QuestionElementString { get; set; }
    }
}