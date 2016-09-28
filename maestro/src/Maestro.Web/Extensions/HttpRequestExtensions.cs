using System.Linq;
using System.Web;

namespace Maestro.Web.Extensions
{
    /// <summary>
    /// Extension methods for object of class HttpRequestBase.
    /// </summary>
    public static class HttpRequestExtensions
    {
        private const string DefaultProtocol = "http";
        private const string HostHeaderKey = "HOST";

        /// <summary>
        /// Returns host domain for current request.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static string GetHost(this HttpRequestBase httpRequest)
        {
            return httpRequest.Headers[HostHeaderKey];
        }

        /// <summary>
        /// Returns customer subdomain from host string.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static string GetCustomerSubdomain(this HttpRequestBase httpRequest)
        {
            string host = httpRequest.GetHost();
            int hostEndIndex = host.IndexOf(WebSettings.Domain);

            if (hostEndIndex < 0)
            {
                return null;
            }

            var resultCustomerSubdomain = host.Substring(0, hostEndIndex);

            //MS-2148: we don't think of www like a customer subdomain anymore
            if (resultCustomerSubdomain.ToLower() == "www")
            {
                return null;
            }

            return resultCustomerSubdomain;
        }

        /// <summary>
        /// Returns protocol of current request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetProtocol(this HttpRequestBase request)
        {
            var url = request.Url;

            if (url == null)
            {
                return DefaultProtocol;
            }

            return url.Scheme;
        }

        /// <summary>
        /// Gets the authorization token.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static string GetAuthorizationToken(this HttpRequest request)
        {
            var authHeader = request.Headers.GetValues("Authorization");

            if (authHeader == null || !authHeader.Any())
            {
                return null;
            }

            var header = authHeader.First();

            // remove "Bearer "
            return header.Substring(7, header.Length - 7);
        }
    }
}