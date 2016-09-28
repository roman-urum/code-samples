using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using CustomerService.Web.Api.ExceptionHandling;
using CustomerService.Web.Api.Filters;
using WebApi.OutputCache.V2;

namespace CustomerService.Web.Api
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

            // Caching
#if !DEBUG
            config.Filters.Add(new CacheOutputAttribute()
            {
                ServerTimeSpan = 3600,
                ExcludeQueryStringFromCacheKey = false
            });
            config.Filters.Add(new AutoInvalidateCacheOutputAttribute());
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