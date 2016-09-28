using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Web.Areas.Customer;
using Maestro.Web.Areas.Site.Models.Patients.Charts;
using Maestro.Web.Extensions;
using Newtonsoft.Json;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.Charts
    /// </summary>
    public partial class PatientsController
    {
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("VitalsChart")]
        public async Task<ActionResult> GetVitalsChart(GetVitalsChartViewModel getChartRequest)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 500;

                return Content(ModelState.GetErrorMessages());
            }

            var vitalsChart = await patientsControllerManager.GetVitalsChart(getChartRequest);

            return Content(JsonConvert.SerializeObject(vitalsChart), "application/json");
        }

        [HttpGet]
        [CustomerAuthorize]
        [ActionName("AssessmentChart")]
        public async Task<ActionResult> GetAssessmentChart(GetAssessmentChartViewModel getChartRequest)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 500;
                return Content(ModelState.GetErrorMessages());
            }

            var assessmentChart = await patientsControllerManager.GetAssessmentChart(getChartRequest);

            return Content(JsonConvert.SerializeObject(assessmentChart), "application/json");
        }

        [HttpGet]
        [CustomerAuthorize()]
        [ActionName("AssessmentChartQuestions")]
        public async Task<ActionResult> GetAssessmentChartQuestions(GetAssessmentChartQuestionsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 500;

                return Content(ModelState.GetErrorMessages());
            }

            var questions = await patientsControllerManager.GetAssessmentChartQuestions(request);

            return Content(JsonConvert.SerializeObject(questions), "application/json");
        }

        /// <summary>
        /// Saves settings for specified patient in db.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CustomerAuthorize]
        [ActionName("TrendsSettings")]
        public async Task<ActionResult> SaveTrendsSettings(Guid patientId, TrendsSettingsViewModel model)
        {
            await patientsControllerManager.SaveTrendsSettings(patientId, model);

            return new EmptyResult();
        }

        /// <summary>
        /// Retunrs settings for specified patient in db.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("TrendsSettings")]
        public async Task<ActionResult> GetTrendsSettings(Guid patientId)
        {
            var result = await patientsControllerManager.GetTrendsSettings(patientId);

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        /// <summary>
        /// Exports the trends to excel.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("ExportTrendsToExcel")]
        public async Task<ActionResult> ExportTrendsToExcel(Guid patientId)
        {
            var result = await patientsControllerManager.ExportTrendsToExcel(patientId);

            if (!result.Item2.Any())
            {
                return new EmptyResult();
            }

            return File(result.Item2, "application/vnd.ms-excel", result.Item1);
        }
    }
}