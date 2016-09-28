namespace HealthLibrary.Domain.Dtos
{
    /// <summary>
    /// Default type of error response.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public int Error { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public string Details { get; set; }
    }
}
