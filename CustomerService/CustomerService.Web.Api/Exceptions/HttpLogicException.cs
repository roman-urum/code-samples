using System;
using System.Net;
using System.Web;

namespace CustomerService.Web.Api.Exceptions
{
    /// <summary>
    /// HttpLogicException.
    /// </summary>
    public class HttpLogicException : HttpException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpLogicException"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="message">The message.</param>
        public HttpLogicException(HttpStatusCode statusCode, string message) :
            base((int)statusCode, message)
        {
        }


    }
}