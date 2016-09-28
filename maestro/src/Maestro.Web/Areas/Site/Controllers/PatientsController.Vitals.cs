using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Web.Areas.Customer;
using Maestro.Web.Areas.Site.Models.Patients;
using System.Net;
using Maestro.Web.Areas.Site.Models.Patients.Vitals;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.Vitals
    /// </summary>
    public partial class PatientsController
    {
        /// <summary>
        /// Gets the vitals.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("Vitals")]
        public async Task<ActionResult> GetVitals(SearchVitalViewModel searchModel)
        {
            var vitalsResult = await patientsControllerManager.SearchVitals(searchModel);

            return Json(vitalsResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomerAuthorize]
        [ActionName("InvalidateMeasurement")]
        public async Task<ActionResult> InvalidateMeasurement(InvalidateMeasurementViewModel request)
        {
            await patientsControllerManager.InvalidateMeasurement(request);

            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }
    }
}