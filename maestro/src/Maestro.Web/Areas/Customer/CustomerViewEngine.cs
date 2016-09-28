using System.Web.Mvc;

namespace Maestro.Web.Areas.Customer
{
    /// <summary>
    /// CustomerViewEngine.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.RazorViewEngine" />
    public class CustomerViewEngine : RazorViewEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerViewEngine"/> class.
        /// </summary>
        public CustomerViewEngine()
        {
            var viewLocations = new[]
            {
                "~/Areas/Customer/Views/{1}/{0}.cshtml",
                "~/Areas/Customer/Shared/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            this.PartialViewLocationFormats = viewLocations;
            this.ViewLocationFormats = viewLocations;
        }
    }
}