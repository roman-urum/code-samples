using System;

namespace Maestro.Domain.Dtos.VitalsService
{
    /// <summary>
    /// SelectionAnswerDto.
    /// </summary>
    public class SelectionAnswerDto : HealthSessionElementValueDto
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public Guid Value { get; set; }

        /// <summary>
        /// Gets or sets the internal identifier.
        /// </summary>
        /// <value>
        /// The internal identifier.
        /// </value>
        public string InternalId { get; set; }

        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the External score.
        /// </summary>
        /// <value>
        /// Int.
        /// </value>
        public int? ExternalScore { get; set; }

        /// <summary>
        /// Gets or sets the External score.
        /// </summary>
        /// <value>
        /// Int.
        /// </value>
        public int? InternalScore { get; set; }
    }
}
