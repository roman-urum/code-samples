using HealthLibrary.Web.Api.Models.Enums;

namespace HealthLibrary.Web.Api.Models
{
    /// <summary>
    /// ErrorResponseDto.
    /// </summary>
    public class ErrorResponseDto
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public ErrorCode Error { get; set; }

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