using System.Web.Mvc;
using Maestro.Web.Extensions;

namespace Maestro.Web.Areas.Customer
{
    /// <summary>
    /// CustomerAreaRegistration.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.AreaRegistration" />
    public class CustomerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Customer";
            }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapMaestroRoute(
                "Customer_SubAction",
                "Customer/{subdomain}/{controller}/{action}/{id}/{subaction}",
                new { subaction = UrlParameter.Optional, action = "Index" },
                new[] { "Maestro.Web.Areas.Customer.Controllers" },
                null,
                AreaName
            );

            context.Routes.MapMaestroRoute(
                "Customer_default",
                "Customer/{subdomain}/{controller}/{action}/{id}",
                new { id = UrlParameter.Optional, action = "Index" },
                new[] { "Maestro.Web.Areas.Customer.Controllers" },
                null,
                AreaName
            );
        }
    }
}