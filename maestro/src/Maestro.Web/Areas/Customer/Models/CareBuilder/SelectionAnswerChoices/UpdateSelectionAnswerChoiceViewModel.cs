using System;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerChoices
{
    /// <summary>
    /// UpdateSelectionAnswerChoiceDto.
    /// </summary>
    public class UpdateSelectionAnswerChoiceViewModel : CreateSelectionAnswerChoiceViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }
    }
}