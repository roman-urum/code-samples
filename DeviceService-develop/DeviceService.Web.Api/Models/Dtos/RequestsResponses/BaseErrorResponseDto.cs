using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceService.Web.Api.Models.Dtos.RequestsResponses
{
    /// <summary>
    /// Default class for error responses.
    /// </summary>
    public class BaseErrorResponseDto
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string Message { get; set; }
    }
}