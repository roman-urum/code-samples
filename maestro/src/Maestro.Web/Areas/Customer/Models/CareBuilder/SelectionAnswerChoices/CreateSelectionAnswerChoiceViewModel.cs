using Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerChoices
{
    /// <summary>
    /// Model of answer from answer set.
    /// </summary>
    public class CreateSelectionAnswerChoiceViewModel
    {
        /// <summary>
        /// Default string for answer choice.
        /// </summary>
        public CreateLocalizedStringViewModel AnswerString { get; set; }

        /// <summary>
        /// Open-ended/closed-ended flag
        /// </summary>
        public bool IsOpenEnded { get; set; }
    }
}