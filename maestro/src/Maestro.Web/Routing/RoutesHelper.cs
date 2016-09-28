using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Maestro.Web.Extensions;

namespace Maestro.Web.Routing
{
    public class RoutesHelper
    {
        /// <summary>
        /// Contains list of controllers which should be available for different areas.
        /// </summary>
        private static readonly List<string> PublicControllers = new List<string>();

        /// <summary>
        /// Saves controller name which should be available in and without area.
        /// </summary>
        /// <param name="controllerName"></param>
        public static void RegisterPublicController(string controllerName)
        {
            if (string.IsNullOrEmpty(controllerName))
            {
                throw new ArgumentNullException("controllerName");
            }

            PublicControllers.Add(controllerName);
        }

        /// <summary>
        /// Determines if subdomain should be ignored for specified request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool IsPublicController(HttpRequestBase request)
        {
            if (request.Url == null)
            {
                return false;
            }

            string[] segments = request.Url.GetRouteSegments();

            return segments.Any() && PublicControllers.Any(m => m.Equals(segments.First()));
        }

        /// <summary>
        /// Generates HttpContextBase instance for specified url.
        /// </summary>
        /// <param name="path">Path for new request context.</param>
        /// <param name="currentRequest">Info about current request.</param>
        /// <returns></returns>
        public HttpContextBase GetContextFor(string path, HttpRequestBase currentRequest)
        {
            var uriBuilder = new UriBuilder(currentRequest.Url.Scheme, currentRequest.Url.Host, currentRequest.Url.Port, path);
            var request = new HttpRequest(null, uriBuilder.ToString(), null);
            var context = new HttpContext(request, new HttpResponse(new StringWriter()));

            return new HttpContextWrapper(context);
        }

        /// <summary>
        /// Identifies if site area requested.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool IsSiteRequest(HttpRequestBase request)
        {
            if (request.Url == null)
            {
                return false;
            }

            string[] segments = request.Url.GetRouteSegments();
            Guid siteId;

            return segments.Any() && Guid.TryParse(segments.First(), out siteId);
        }

        /// <summary>
        /// Verifies if request should be handled in customer area.
        /// </summary>
        /// <returns></returns>
        public bool IsCustomerRequest(HttpRequestBase request)
        {
            string subdomain = request.GetCustomerSubdomain();

            return !this.IsSiteRequest(request) &&
                   !string.IsNullOrEmpty(subdomain) &&
                   request.Url != null &&
                   !this.IsPublicController(request);
        }
    }
}