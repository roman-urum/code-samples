using System.Web.Mvc;
using Maestro.Web.Extensions;

namespace Maestro.Web.Areas.Site
{
    /// <summary>
    /// SiteAreaRegistration.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.AreaRegistration" />
    public class SiteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Site";
            }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapMaestroRoute(
                "Site_EditPatient",
                "{siteId}/{controller}/{action}/{id}/{tab}",
                new { action = "Index", tab = UrlParameter.Optional },
                new[] { "Maestro.Web.Areas.Site.Controllers" },
                new { siteId = @"\b[a-fA-F0-9]{8}(?:-[a-fA-F0-9]{4}){3}-[a-fA-F0-9]{12}\b" },
                AreaName
            );

            context.Routes.MapMaestroRoute(
                "Site_default",
                "{siteId}/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Maestro.Web.Areas.Site.Controllers" },
                new { siteId = @"\b[a-fA-F0-9]{8}(?:-[a-fA-F0-9]{4}){3}-[a-fA-F0-9]{12}\b" },
                AreaName
            );
        }
    }
}