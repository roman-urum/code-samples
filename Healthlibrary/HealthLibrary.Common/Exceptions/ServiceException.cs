using System;
using System.Net;
using HealthLibrary.Common.Extensions;

namespace HealthLibrary.Common.Exceptions
{
    /// <summary>
    /// Raise this exception for common errors appears during
    /// communication with external services (APIs).
    /// </summary>
    public class ServiceException : Exception
    {
        private const string DefaultErrorMessage = "Service returns an error in response.";
        private const string ServiceErrorMessageTemplate = "External service exception. Service: {0}. Status Code: {1}. Message: {2}";

        public ServiceException(string serviceUrl, HttpStatusCode code)
            : this(serviceUrl, code, DefaultErrorMessage)
        {
            Code = code;
        }

        public ServiceException(string serviceUrl, HttpStatusCode code, string message)
            : base(ServiceErrorMessageTemplate.FormatWith(serviceUrl, code, message))
        {
            Code = code;
            serviceMessage = message;

            if (string.IsNullOrEmpty(serviceUrl))
            {
                throw new ArgumentNullException("serviceUrl");
            }
        }

        private readonly string serviceMessage;
        public string ServiceMessage { get { return serviceMessage; } }

        public HttpStatusCode Code { get; set; }
    }
}
