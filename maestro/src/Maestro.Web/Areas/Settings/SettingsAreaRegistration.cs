using System.Web.Mvc;
using Maestro.Web.Extensions;

namespace Maestro.Web.Areas.Settings
{
    /// <summary>
    /// SettingsAreaRegistration.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.AreaRegistration" />
    public class SettingsAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapMaestroRoute(
                "Settings_Default",
                "Settings/{controller}/{action}/{id}",
                new { id = UrlParameter.Optional, action = "Index" },
                new[] { "Maestro.Web.Areas.Settings.Controllers" },
                null,
                AreaName
            );
        }

        public override string AreaName
        {
            get
            {
                return "Settings";
            }
        }
    }
}