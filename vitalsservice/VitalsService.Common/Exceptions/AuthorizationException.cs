using System.Net;

namespace VitalsService.Exceptions
{
    /// <summary>
    /// Raise this exception when api returns error
    /// because of credentials are invalid.
    /// </summary>
    public class AuthorizationException : ServiceException
    {
        public AuthorizationException(string serviceUrl, string message)
            : base(serviceUrl, HttpStatusCode.Unauthorized, message)
        {
        }
    }
}
