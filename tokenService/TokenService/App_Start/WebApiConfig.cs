using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using CareInnovations.HealthHarmony.Maestro.TokenService.ExceptionHandling;
using CareInnovations.HealthHarmony.Maestro.TokenService.Filters;

namespace CareInnovations.HealthHarmony.Maestro.TokenService
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
            // Model validation
            config.Filters.Add(new ValidateModelAttribute());

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}