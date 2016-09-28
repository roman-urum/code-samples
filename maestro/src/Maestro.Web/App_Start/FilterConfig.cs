using System.Web.Mvc;
using Maestro.Domain.Constants;
using Maestro.Web.Filters;

namespace Maestro.Web
{
    public class FilterConfig
    {
        private const string SettingsAreaName = "Settings";

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Global filters
            filters.Add(new PreventCachingAttribute());

            // Area filters
            filters.Add(new AreaRelatedAttribute(SettingsAreaName,
                new MaestroAuthorizeAttribute(Roles.SuperAdmin)));
            
            filters.Add(new AreaRelatedAttribute(SiteContext.SiteAreaName,
                new SiteAuthorizeAttribute()));

            filters.Add(new AreaRelatedAttribute(SiteContext.SiteAreaName,
                new MaestroAuthorizeAttribute()));
        }
    }
}