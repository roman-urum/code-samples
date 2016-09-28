using System.Web.Http;
using System.Web.Http.ExceptionHandling;

using VitalsService.Web.Api.ExceptionHandling;
using VitalsService.Web.Api.Filters;

namespace VitalsService.Web.Api
{
    /// <summary>
    /// WebApiConfig.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{customerId}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { customerId = @"\d+" }
            );

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }
    }
}