using System;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using Maestro.Web.Areas.Customer;
using Maestro.Web.Areas.Site.Models.Patients.PatientsConditions;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.Vitals
    /// </summary>
    public partial class PatientsController
    {
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("PatientConditions")]
        public async Task<ActionResult> GetPatientConditions(Guid patientId)
        {
            var patientsConditions = await patientsControllerManager.GetPatientConditions(patientId);

            return Json(patientsConditions, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomerAuthorize]
        [ActionName("PatientConditions")]
        public async Task<ActionResult> CreatePatientConditions(PatiensConditionsRequestViewModel request)
        {
            await patientsControllerManager.CreatePatientConditions(request);

            return new EmptyResult();
        }
    }
}