using System;
using System.Linq;
using System.Web;

namespace Maestro.Web.Extensions
{
    /// <summary>
    /// Extension methods for Uri class.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Returns route segmets for specified uri.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string[] GetRouteSegments(this Uri uri)
        {
            return uri.PathAndQuery.Split('/').Where(s => !string.IsNullOrEmpty(s)).ToArray();
        }

        /// <summary>
        /// Indicates whether the supplied url matches the specified controller and action values based on the MVC routing table defined in global.asax.
        /// </summary>
        /// <param name="uri">A Uri object containing the url to evaluate</param>
        /// <param name="actionName">The name of the action method to match</param>
        /// <param name="controllerName">The name of the controller class to match</param>
        /// <param name="areaName">Name of the area.</param>
        /// <returns>
        /// True if the supplied url is mapped to the supplied controller class and action method, false otherwise.
        /// </returns>
        public static bool IsRouteMatch(this Uri uri, string actionName, string controllerName, string areaName = null)
        {
            var routeData = HttpContext.Current.Request.RequestContext.RouteData;

            var area = routeData.DataTokens["area"] != null ? routeData.DataTokens["area"].ToString() : string.Empty;
            var controller = routeData.Values["controller"].ToString();
            var action = routeData.Values["action"].ToString();

            if (!string.IsNullOrEmpty(areaName))
            {
                return areaName == area && controller == controllerName && action == actionName;
            }

            return controller == controllerName && action == actionName;
        }
    }
}