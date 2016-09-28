using System;

namespace Maestro.Reporting.Models.PatientTrends
{
    /// <summary>
    /// AnswerModel.
    /// </summary>
    public class AnswerModel
    {
        /// <summary>
        /// Gets or sets the answer text.
        /// </summary>
        /// <value>
        /// The answer text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the answered.
        /// </summary>
        /// <value>
        /// The answered.
        /// </value>
        public DateTimeOffset Answered { get; set; }
    }
}