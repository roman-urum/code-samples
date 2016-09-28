using System;
using System.Web;
using System.Web.Mvc;

namespace Maestro.Web.Filters
{
    public class PreventCachingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var responseCache = filterContext.HttpContext.Response.Cache;

            responseCache.SetExpires(DateTime.UtcNow.AddDays(-1));
            responseCache.SetValidUntilExpires(false);
            responseCache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            responseCache.SetCacheability(HttpCacheability.NoCache);
            responseCache.SetNoStore();
        }
    }
}