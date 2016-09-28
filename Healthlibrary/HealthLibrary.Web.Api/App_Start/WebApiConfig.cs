using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using HealthLibrary.Web.Api.ExceptionHandling;
using HealthLibrary.Web.Api.Filters;
using HealthLibrary.Web.Api.Filters.Exceptions;
using WebApi.OutputCache.V2;

namespace HealthLibrary.Web.Api
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
            config.Filters.Add(new AuthorizeResultAttribute());
            config.Filters.Add(new ValidateModelAttribute());
            config.Filters.Add(new InvalidContentFilterAttribute());

            // Caching
#if !DEBUG
            config.Filters.Add(new CacheOutputAttribute()
            {
                ServerTimeSpan = 3600,
                ExcludeQueryStringFromCacheKey = false
            });
#endif

            // There must be exactly one exception handler. (There is a default one that may be replaced.)
            // To make this sample easier to run in a browser, replace the default exception handler with one that sends
            // back text/plain content for all errors.
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }
    }
}