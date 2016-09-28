using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Maestro.Web.Extensions;

namespace Maestro.Web.Routing
{
    /// <summary>
    /// Route to generate url using subdomain and site id.
    /// </summary>
    public class MaestroRoute : Route
    {
        private const string DefaultPagePath = "/";

        private readonly RoutesHelper routes;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaestroRoute"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="defaults">The defaults.</param>
        /// <param name="dataTokens">The data tokens.</param>
        /// <param name="constraints">The constraints.</param>
        public MaestroRoute(
            string url,
            RouteValueDictionary defaults,
            RouteValueDictionary dataTokens,
            RouteValueDictionary constraints
        ) : base(url, defaults, constraints, dataTokens, new MvcRouteHandler())
        {
            routes = new RoutesHelper();
        }

        /// <summary>
        /// Generates route data by request.
        /// Maps customer subdomain and site id to area.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            HttpRequestBase request = httpContext.Request;

            if (!routes.IsCustomerRequest(request))
            {
                return base.GetRouteData(httpContext);
            }

            string areaUrlPart = CustomerContext.AreaPath;
            string pageUrl = httpContext.Request.Path.Equals(DefaultPagePath)
                ? WebSettings.CustomerDefaultPage
                : httpContext.Request.Path;
            string internalPath = string.Concat(areaUrlPart, pageUrl);

            return base.GetRouteData(routes.GetContextFor(internalPath, request));
        }

        /// <summary>
        /// Generates virtual path for link by provided route values.
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            string subdomain = requestContext.HttpContext.Request.GetCustomerSubdomain();
            VirtualPathData result = base.GetVirtualPath(requestContext, values);

            if (string.IsNullOrEmpty(subdomain) ||
                result == null ||
                !CustomerContext.CustomerAreaName.Equals(result.GetArea(), StringComparison.InvariantCultureIgnoreCase))
            {
                return result;
            }

            string areaUrlPath = CustomerContext.AreaPath;

            if (!string.IsNullOrEmpty(result.VirtualPath) &&
                result.VirtualPath.StartsWith(areaUrlPath, StringComparison.InvariantCultureIgnoreCase))
            {
                int internalUrlStartIndex = areaUrlPath.Length + 1;
                result.VirtualPath = result.VirtualPath.Substring(internalUrlStartIndex);
            }

            return result;
        }
    }
}