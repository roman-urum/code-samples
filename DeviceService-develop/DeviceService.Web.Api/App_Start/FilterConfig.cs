using System.Web.Http.Filters;
using DeviceService.Web.Api.Filters;
using WebApi.OutputCache.V2;

namespace DeviceService.Web.Api
{
    /// <summary>
    /// FilterConfig.
    /// </summary>
    public class FilterConfig
    {
        private const int OutputCacheServerTimeSpan = 3600;

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
                ServerTimeSpan = OutputCacheServerTimeSpan,
                ExcludeQueryStringFromCacheKey = false
            });
#endif
        }
    }
}