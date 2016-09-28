using Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.QuestionElements
{
    /// <summary>
    /// CreateQuestionElementModel.
    /// </summary>
    public class CreateQuestionElementViewModel : BaseQuestionElementViewModel
    {
        /// <summary>
        /// Gets or sets the question string.
        /// </summary>
        /// <value>
        /// The question string.
        /// </value>
        public new CreateLocalizedStringViewModel QuestionElementString { get; set; }
    }
}