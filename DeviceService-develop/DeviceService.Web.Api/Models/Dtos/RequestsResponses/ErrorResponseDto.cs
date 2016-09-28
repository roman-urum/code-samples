using DeviceService.Web.Api.Models.Dtos.Enums;

namespace DeviceService.Web.Api.Models.Dtos.RequestsResponses
{
    /// <summary>
    /// ErrorResponseDto.
    /// </summary>
    public class ErrorResponseDto : BaseErrorResponseDto
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public ErrorCode Error { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public string Details { get; set; }
    }
}