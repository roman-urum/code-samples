using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Domain.Dtos.VitalsService.Thresholds;
using Maestro.Web.Areas.Customer;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.Thresholds
    /// </summary>
    public partial class PatientsController
    {
        /// <summary>
        /// Gets the thresholds.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("Thresholds")]
        public async Task<ActionResult> GetThresholds(Guid patientId)
        {
            var patientThresholds = await patientsControllerManager.GetThresholds(patientId);

            return Json(patientThresholds, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates the threshold.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [CustomerAuthorize]
        [ActionName("Threshold")]
        public async Task<ActionResult> CreateThreshold(CreateThresholdRequestDto request)
        {
            var result = await patientsControllerManager.CreateThreshold(request);

            return Json(result);
        }

        /// <summary>
        /// Updates the threshold.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [CustomerAuthorize]
        [ActionName("Threshold")]
        public async Task<ActionResult> UpdateThreshold(UpdateThresholdRequestDto request)
        {
            await patientsControllerManager.UpdateThreshold(request);

            return Json(string.Empty);
        }

        /// <summary>
        /// Deletes the threshold.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [CustomerAuthorize]
        [ActionName("Threshold")]
        public async Task<ActionResult> DeleteThreshold(Guid patientId, Guid thresholdId)
        {
            await patientsControllerManager.DeleteThreshold(patientId, thresholdId);

            return Json(string.Empty);
        }
    }
}