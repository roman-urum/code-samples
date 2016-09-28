using System.Collections.Generic;

namespace Maestro.Reporting.Models.PatientTrends
{
    /// <summary>
    /// QuestionAnswersModel.
    /// </summary>
    public class QuestionAnswersModel
    {
        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        /// <value>
        /// The question.
        /// </value>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the question answers.
        /// </summary>
        /// <value>
        /// The question answers.
        /// </value>
        public IList<AnswerModel> Answers { get; set; }
    }
}