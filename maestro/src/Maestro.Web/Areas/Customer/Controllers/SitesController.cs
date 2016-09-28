using System.Web.Mvc;
using Maestro.Web.Areas.Customer.Managers.Interfaces;
using Maestro.Web.Controllers;
using Maestro.Web.Filters;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// SitesController.
    /// </summary>
    /// <seealso cref="Maestro.Web.Controllers.BaseController" />
    [MaestroAuthorize]
    public class SitesController : BaseController
    {
        private readonly ISitesControllerManager sitesControllerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitesController"/> class.
        /// </summary>
        /// <param name="sitesControllerManager">The sites controller manager.</param>
        public SitesController(ISitesControllerManager sitesControllerManager)
        {
            this.sitesControllerManager = sitesControllerManager;
        }

        /// <summary>
        /// Displays message that user haven't sites assigned.
        /// </summary>
        /// <returns></returns>
        public ActionResult NoAccess()
        {
            return View();
        }

        /// <summary>
        /// Returns partial view with list of available sites.
        /// </summary>
        /// <returns></returns>
        public ActionResult Selector()
        {
            var model = sitesControllerManager.InitSitesSelectorModel();

            return PartialView("_Selector", model);
        }
    }
}