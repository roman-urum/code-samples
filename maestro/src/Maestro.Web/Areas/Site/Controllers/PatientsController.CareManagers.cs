using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

using Maestro.Domain.Dtos.PatientsService;
using Maestro.Web.Areas.Customer;
using Maestro.Web.Areas.Site.Models.Patients.CareManagers;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.CareManagers
    /// </summary>
    public partial class PatientsController
    {
        /// <summary>
        /// Gets the available care managers.
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("CareManagers")]
        public async Task<ActionResult> GetAvailableCareManagers([FromUri]SearchCareManagersViewModel searchRequest)
        {
            var patientCareManagers = await patientsControllerManager.GetAvailableCareManagers(searchRequest);

            return Json(patientCareManagers, JsonRequestBehavior.AllowGet);
        }
    }
}