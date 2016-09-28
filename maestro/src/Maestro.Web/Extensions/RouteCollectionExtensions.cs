using System.Web.Routing;
using Maestro.Web.Routing;

namespace Maestro.Web.Extensions
{
    /// <summary>
    /// Extension methods for RouteCollection.
    /// </summary>
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// Extension method to declare customer route.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        /// <param name="defaults">The defaults.</param>
        /// <param name="namespaces">The namespaces.</param>
        /// <param name="constraints">The constraints.</param>
        /// <param name="area">The area.</param>
        public static void MapMaestroRoute(
            this RouteCollection routes,
            string name,
            string url,
            object defaults = null,
            string[] namespaces = null,
            object constraints = null, 
            string area = null
        )
        {
            var datatokens = new RouteValueDictionary {{"Namespaces", namespaces}, {"Area", area}};

            routes.Add(
                name,
                new MaestroRoute(
                    url,
                    new RouteValueDictionary(defaults),
                    datatokens,
                    new RouteValueDictionary(constraints)
                )
            );
        }
    }
}