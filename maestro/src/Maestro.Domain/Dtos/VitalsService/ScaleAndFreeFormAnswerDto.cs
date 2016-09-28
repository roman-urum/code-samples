namespace Maestro.Domain.Dtos.VitalsService
{
    /// <summary>
    /// ScaleAndFreeFormAnswerDto. This strange name is because there are two almost similar answer types scaleanswer and freeeformanswer.
    /// Because of that there were troubles in knowntype converter, namely there was no way to detect to what type the JSON has to be deserialized.
    /// </summary>
    public class ScaleAndFreeFormAnswerDto : HealthSessionElementValueDto
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

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
        /// Gets or sets the external score.
        /// </summary>
        /// <value>
        /// The external score.
        /// </value>
        public int? ExternalScore { get; set; }

        /// <summary>
        /// Gets or sets the internal score.
        /// </summary>
        /// <value>
        /// The internal score.
        /// </value>
        public int? InternalScore { get; set; }
    }
}