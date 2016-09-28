using System;
using System.Net;
using Maestro.Common.Extensions;

namespace Maestro.Common.Exceptions
{
    /// <summary>
    /// Raise this exception for common errors appears during
    /// communication with external services (APIs).
    /// </summary>
    public class ServiceException : Exception
    {
        private const string DefaultErrorMessage = "Service returns an error in response.";
        private const string ServiceErrorMessageTemplate = "External service exception. Service: {0}. Status Code: {1}. Message: {2}";

        /// <summary>
        /// Contains error key for service excetion.
        /// </summary>
        public string ErrorKey { get; private set; }

        /// <summary>
        /// Description of error details.
        /// </summary>
        public string ServiceMessage { get; private set; }

        /// <summary>
        /// Code from response of http request.
        /// </summary>
        public HttpStatusCode Code { get; set; }

        public ServiceException(string serviceUrl, HttpStatusCode code)
            : this(serviceUrl, code, DefaultErrorMessage)
        {
            Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceException"/> class.
        /// </summary>
        /// <param name="serviceUrl">The service URL.</param>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentNullException">serviceUrl</exception>
        public ServiceException(string serviceUrl, HttpStatusCode code, string message)
            : base(ServiceErrorMessageTemplate.FormatWith(serviceUrl, code, message))
        {
            Code = code;
            ServiceMessage = message;

            if (string.IsNullOrEmpty(serviceUrl))
            {
                throw new ArgumentNullException("serviceUrl");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceException"/> class.
        /// </summary>
        /// <param name="serviceUrl">The service URL.</param>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <param name="errorKey">The error key.</param>
        public ServiceException(string serviceUrl, HttpStatusCode code, string message, string errorKey)
            : this(serviceUrl, code, message)
        {
            this.ErrorKey = errorKey;
        }
    }
}