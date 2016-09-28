using System.Web.Http.Filters;
using VitalsService.Web.Api.Filters;
using WebApi.OutputCache.V2;

namespace VitalsService.Web.Api
{
    /// <summary>
    /// FilterConfig.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(HttpFilterCollection filters)
        {
            // Global filters
            filters.Add(new AuthorizeResultAttribute());

            // Model validation
            filters.Add(new ValidateModelAttribute());

            // Caching
#if !DEBUG
            filters.Add(new CacheOutputAttribute()
            {
                ServerTimeSpan = 3600,
                ExcludeQueryStringFromCacheKey = false
            });
#endif
        }
    }
}