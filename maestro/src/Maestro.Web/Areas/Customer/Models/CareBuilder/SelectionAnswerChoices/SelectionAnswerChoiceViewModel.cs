using System;
using Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerChoices
{
    /// <summary>
    /// Model for responses with selection answer choices.
    /// </summary>
    public class SelectionAnswerChoiceViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Default string for answer choice.
        /// </summary>
        public LocalizedStringViewModel AnswerString { get; set; }

        /// <summary>
        /// Open-ended/closed-ended flag
        /// </summary>
        public bool IsOpenEnded { get; set; }
    }
}