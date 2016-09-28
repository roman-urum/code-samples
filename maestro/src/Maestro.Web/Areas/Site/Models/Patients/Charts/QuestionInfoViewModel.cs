using System;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// QuestionInfoViewModel.
    /// </summary>
    public class QuestionInfoViewModel
    {
        /// <summary>
        /// Gets or sets the question identifier.
        /// </summary>
        /// <value>
        /// The question identifier.
        /// </value>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Gets or sets the question text.
        /// </summary>
        /// <value>
        /// The question text.
        /// </value>
        public string QuestionText { get; set; }
    }
}