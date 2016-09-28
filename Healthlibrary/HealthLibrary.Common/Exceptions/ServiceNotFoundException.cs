using System.Net;

namespace HealthLibrary.Common.Exceptions
{
    public class ServiceNotFoundException : ServiceException
    {
        public ServiceNotFoundException(string serviceUrl)
            : base(serviceUrl, HttpStatusCode.NotFound)
        {
        }

        public ServiceNotFoundException(string serviceUrl, string message)
            : base(serviceUrl, HttpStatusCode.NotFound, message)
        {
        }
    }
}
