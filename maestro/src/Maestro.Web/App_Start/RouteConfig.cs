using System.Web.Mvc;
using System.Web.Routing;
using Maestro.Web.Extensions;
using Maestro.Web.Routing;

namespace Maestro.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMaestroRoute(
                name: "CustomerDefault",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Customers", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Maestro.Web.Controllers" }
            );

            RoutesHelper.RegisterPublicController("Error");
            RoutesHelper.RegisterPublicController("Account");
        }
    }
}