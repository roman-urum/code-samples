using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace HealthLibrary.Web.Api.Extensions
{
    /// <summary>
    /// Extension methods for RouteData collection.
    /// </summary>
    public static class RouteDataExtensions
    {
        private const string WebApiRouteDataKey = "MS_SubRoutes";

        public static IDictionary<string, object> GetWebApiRouteData(this RouteData routeData)
        {
            if (!routeData.Values.ContainsKey(WebApiRouteDataKey))
                return new Dictionary<string, object>();

            return
                ((IHttpRouteData[]) routeData.Values[WebApiRouteDataKey])
                    .SelectMany(x => x.Values)
                    .Distinct()
                    .ToDictionary(x => x.Key, y => y.Value);
        }
    }
}