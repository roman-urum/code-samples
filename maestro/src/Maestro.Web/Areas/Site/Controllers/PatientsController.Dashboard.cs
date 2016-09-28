using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Web.Areas.Customer;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.Dashboard
    /// </summary>
    public partial class PatientsController
    {
        /// <summary>
        /// Gets the patient specific thresholds.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("PatientSpecificThresholds")]
        public async Task<ActionResult> GetPatientSpecificThresholds(Guid patientId)
        {
            var patientSpecificThresholds = await patientsControllerManager.GetPatientSpecificThresholds(patientId);

            return Json(patientSpecificThresholds, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the peripherals.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("Peripherals")]
        public async Task<ActionResult> GetPeripherals(Guid patientId)
        {
            var peripherals = await patientsControllerManager.GetPeripherals(patientId);

            return Json(peripherals, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the health sessions dashboard.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("HealthSessionsDashboard")]
        public async Task<ActionResult> GetHealthSessionsDashboard(Guid patientId)
        {
            var healthSessionsDashboard = await patientsControllerManager
                .GetHealthSessionsDashboard(patientId);
            
            return Content(
                JsonConvert.SerializeObject(
                    healthSessionsDashboard, 
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                ),
                "application/json"
            );
        }

        /// <summary>
        /// Gets the latest information dashboard.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("LatestInformationDashboard")]
        public async Task<ActionResult> GetLatestInformationDashboard(Guid patientId)
        {
            var latestInformationDashboard = await patientsControllerManager
                .GetLatestInformationDashboard(patientId);

            return Content(
                JsonConvert.SerializeObject(
                    latestInformationDashboard,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                ),
                "application/json"
            );
        }
    }
}