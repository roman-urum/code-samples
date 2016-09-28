using Newtonsoft.Json;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// AnswerInfoViewModel.
    /// </summary>
    public class AnswerInfoViewModel
    {
        /// <summary>
        /// Gets or sets the answer identifier.
        /// </summary>
        /// <value>
        /// The answer identifier.
        /// </value>
        public string AnswerId { get; set; }

        /// <summary>
        /// Gets or sets the answer text.
        /// </summary>
        /// <value>
        /// The answer text.
        /// </value>
        public string AnswerText { get; set; }
    }
}