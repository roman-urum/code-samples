using System.Net;

namespace Maestro.Common.Exceptions
{
    /// <summary>
    /// Raise this exception when api returns error
    /// because of credentials are invalid.
    /// </summary>
    public class AuthorizationException : ServiceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class.
        /// </summary>
        /// <param name="serviceUrl">The service URL.</param>
        /// <param name="message">The message.</param>
        /// <param name="error">The error.</param>
        public AuthorizationException(string serviceUrl, string message, string error)
            : base(serviceUrl, HttpStatusCode.Unauthorized, message, error)
        {
        }
    }
}