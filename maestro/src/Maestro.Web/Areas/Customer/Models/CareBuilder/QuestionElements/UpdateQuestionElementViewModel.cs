using System;
using Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.QuestionElements
{
    /// <summary>
    /// UpdateQuestionElementModel.
    /// </summary>
    public class UpdateQuestionElementViewModel : BaseQuestionElementViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the question string.
        /// </summary>
        /// <value>
        /// The question string.
        /// </value>
        public new CreateLocalizedStringViewModel QuestionElementString { get; set; }
    }
}