using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Owin;

namespace MyWebApiIISHost
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteTable.Routes.MapOwinPath("/rss", app =>
            {
                app.Run(context =>
                {
                    context.Response.ContentType = "application/rss+xml";
                    var response = new StringBuilder();
                    response.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>")
                    .Append("<rss version=\"2.0\">")
                    .Append("<chanel>")
                    .Append("<title>My Rss Feed</title>")
                    .Append("<item>")
                    .Append("<title>Hello World</title>")
                    .Append("<description>")
                    .Append("Hail K &amp; R")
                    .Append("</description>")
                    .Append("<link>http://hello.world</link>")
                    .Append("</item>")
                    .Append("</chanel>")
                    .Append("</rss>")
                    ;

                    return context.Response.WriteAsync(response.ToString());
                });
            });

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}
