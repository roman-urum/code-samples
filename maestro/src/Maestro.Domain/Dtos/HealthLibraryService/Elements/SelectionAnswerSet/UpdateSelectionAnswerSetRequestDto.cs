using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerChoices;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerSet
{
    /// <summary>
    /// Selection answer set dto to update answer set.
    /// </summary>
    [JsonObject]
    public class UpdateSelectionAnswerSetRequestDto : BaseSelectionAnswerSetDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Possible answers for current answerset.
        /// </summary>
        public List<UpdateSelectionAnswerChoiceRequestDto> SelectionAnswerChoices { get; set; }
    }
}