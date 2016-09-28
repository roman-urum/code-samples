using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace Maestro.Web.Extensions
{
    /// <summary>
    /// Extension method for RouteData objects.
    /// </summary>
    public static class RouteDataExtensions
    {
        private const string AreaRouteValueName = "area";
        private const string ControllerRouteValueName = "controller";
        private const string ActionRouteValueName = "action";
        private const string WebApiRouteDataKey = "MS_SubRoutes";

        /// <summary>
        /// Returns area name for current request.
        /// Returns null if area not specified.
        /// </summary>
        /// <param name="routeData"></param>
        /// <returns></returns>
        public static string GetArea(this RouteData routeData)
        {
            var value = routeData.DataTokens[AreaRouteValueName];

            return value == null ? null : value.ToString();
        }

        /// <summary>
        /// Returns area name for virtual path.
        /// Returns null if area not specified.
        /// </summary>
        /// <param name="pathData"></param>
        /// <returns></returns>
        public static string GetArea(this VirtualPathData pathData)
        {
            var value = pathData.DataTokens[AreaRouteValueName];

            return value == null ? null : value.ToString();
        }

        /// <summary>
        /// Updates route data with specified area name.
        /// </summary>
        /// <param name="routeData"></param>
        /// <param name="areaName"></param>
        public static void SetArea(this RouteData routeData, string areaName)
        {
            if (string.IsNullOrEmpty(areaName))
            {
                throw new ArgumentNullException("areaName");
            }

            routeData.DataTokens[AreaRouteValueName] = areaName;
        }

        /// <summary>
        /// Removes area value from routes.
        /// </summary>
        /// <param name="routeData"></param>
        public static void RemoveArea(this RouteData routeData)
        {
            routeData.DataTokens.Remove(AreaRouteValueName);
        }

        /// <summary>
        /// Returns controller name for current request.
        /// Returns null if controller not specified.
        /// </summary>
        /// <param name="routeData"></param>
        /// <returns></returns>
        public static string GetController(this RouteData routeData)
        {
            var value = routeData.Values[ControllerRouteValueName];

            return value == null ? null : value.ToString();
        }

        /// <summary>
        /// Returns action name for current request.
        /// Returns null if action not specified.
        /// </summary>
        /// <param name="routeData"></param>
        /// <returns></returns>
        public static string GetAction(this RouteData routeData)
        {
            var value = routeData.Values[ActionRouteValueName];

            return value == null ? null : value.ToString();
        }

        /// <summary>
        /// Gets the web API route data.
        /// </summary>
        /// <param name="routeData">The route data.</param>
        /// <returns></returns>
        public static IDictionary<string, object> GetWebApiRouteData(this RouteData routeData)
        {
            if (!routeData.Values.ContainsKey(WebApiRouteDataKey))
                return new Dictionary<string, object>();

            return
                ((IHttpRouteData[])routeData.Values[WebApiRouteDataKey])
                    .SelectMany(x => x.Values)
                    .Distinct()
                    .ToDictionary(x => x.Key, y => y.Value);
        }
    }
}