using System;

namespace VitalsService.Web.Api.Models.Alerts
{
    /// <summary>
    /// HealthSessionElementAlertResponseDto.
    /// </summary>
    public class HealthSessionElementAlertResponseDto : BaseAlertResponseDto
    {
        /// <summary>
        /// Gets or sets the question text
        /// </summary>
        /// <value>
        /// The question text
        /// </value>
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets the answer text
        /// </summary>
        /// <value>
        /// The answer text
        /// </value>
        public string AnswerText { get; set; }

        /// <summary>
        /// Gets or sets the answered date UTC.
        /// </summary>
        /// <value>
        /// The answered date UTC.
        /// </value>
        public DateTime AnsweredUtc { get; set; }

        /// <summary>
        /// Gets or sets the answered timezone.
        /// </summary>
        /// <value>
        /// The answered timezone.
        /// </value>
        public string AnsweredTz { get; set; }

        /// <summary>
        /// Gets or sets the health library element identifier
        /// </summary>
        /// <value>
        /// The health library element identifier
        /// </value>
        public Guid ElementId { get; set; }
    }
}