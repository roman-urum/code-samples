using System.Net;

namespace Maestro.Common.Exceptions
{
    /// <summary>
    /// ServiceNotFoundException.
    /// </summary>
    /// <seealso cref="Maestro.Common.Exceptions.ServiceException" />
    public class ServiceNotFoundException : ServiceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException"/> class.
        /// </summary>
        /// <param name="serviceUrl">The service URL.</param>
        public ServiceNotFoundException(string serviceUrl) : base(serviceUrl, HttpStatusCode.NotFound)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException"/> class.
        /// </summary>
        /// <param name="serviceUrl">The service URL.</param>
        /// <param name="message">The message.</param>
        public ServiceNotFoundException(string serviceUrl, string message) : base(serviceUrl, HttpStatusCode.NotFound, message)
        {
        }
    }
}