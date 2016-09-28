using System;
using Maestro.Web.Areas.Site.Models.Patients.Charts;

namespace Maestro.Web.Areas.Site.Models
{
    /// <summary>
    /// QuestionReadingViewModel.
    /// </summary>
    public class QuestionReadingViewModel : BaseReadingViewModel
    {
        /// <summary>
        /// Gets or sets the question text.
        /// </summary>
        /// <value>
        /// The question text.
        /// </value>
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets the answer text.
        /// </summary>
        /// <value>
        /// The answer text.
        /// </value>
        public string AnswerText { get; set; }

        /// <summary>
        /// Gets or sets the element identifier.
        /// </summary>
        /// <value>
        /// The element identifier.
        /// </value>
        public Guid ElementId { get; set; }
    }
}