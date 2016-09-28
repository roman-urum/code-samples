using System;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols
{
    public class SelectionAnswerChoiceElementResponseViewModel
    {
        /// <summary>
        /// Id of answer choice.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Localized answer strings.
        /// </summary>
        public LocalizedStringResponseDto AnswerString { get; set; }
    }
}
