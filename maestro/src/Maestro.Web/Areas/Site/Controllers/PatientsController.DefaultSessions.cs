using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Maestro.Domain.Dtos.PatientsService.DefaultSessions;
using Maestro.Web.Areas.Customer;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.Calendar
    /// </summary>
    public partial class PatientsController
    {
        /// <summary>
        /// Creates new default session for patient.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionDto"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("DefaultSessions")]
        public async Task<ActionResult> CreateDefaultSession(
            [FromUri] Guid patientId,
            DefaultSessionDto defaultSessionDto)
        {
            var result = await this.patientsControllerManager.CreateDefaultSession(patientId, defaultSessionDto);

            return Json(result);
        }

        /// <summary>
        /// Returns default health session for patient.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("DefaultSessions")]
        public async Task<ActionResult> GetDefaultSession([FromUri] Guid patientId)
        {
            var result = await this.patientsControllerManager.GetDefaultSession(patientId);

            if (result == null)
            {
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Updates existing session.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionId"></param>
        /// <param name="defaultSessionDto"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPut]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("DefaultSessions")]
        public async Task<ActionResult> UpdateDefaultSession(
            [FromUri] Guid patientId,
            [FromUri] Guid defaultSessionId,
            DefaultSessionDto defaultSessionDto)
        {
            await this.patientsControllerManager.UpdateDefaultSession(patientId, defaultSessionId, defaultSessionDto);

            return Json(string.Empty);
        }

        /// <summary>
        /// Deletes default session.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionId"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpDelete]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("DefaultSessions")]
        public async Task<ActionResult> DeleteDefaultSession(
            [FromUri] Guid patientId,
            [FromUri] Guid defaultSessionId)
        {
            await this.patientsControllerManager.DeleteDefaultSession(patientId, defaultSessionId);

            return Json(string.Empty);
        }
    }
}