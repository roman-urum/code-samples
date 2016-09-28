using System.Collections.Generic;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerChoices;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerSet
{
    /// <summary>
    /// Container for new request data.
    /// </summary>
    [JsonObject]
    public class CreateSelectionAnswerSetRequestDto : BaseSelectionAnswerSetDto
    {
        /// <summary>
        /// Possible answers for current answerset.
        /// </summary>
        public List<CreateSelectionAnswerChoiceRequestDto> SelectionAnswerChoices { get; set; }
    }
}