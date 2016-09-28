using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Maestro.Web.Areas.Customer;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.VideoCalls
    /// </summary>
    public partial class PatientsController
    {
        /// <summary>
        /// Creates new video call in zoom.
        /// </summary>
        /// <returns>Url to redirect.</returns>
        [System.Web.Mvc.HttpPost]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("VideoCall")]
        public async Task<ActionResult> CreateVideoCall(Guid patientId)
        {
            var result = await patientsControllerManager.CreateVideoCall(patientId);

            return Json(result);
        }

        /// <summary>
        /// Returns video call by identifier.
        /// </summary>
        /// <returns>Url to redirect.</returns>
        [System.Web.Mvc.HttpGet]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("VideoCall")]
        public async Task<ActionResult> GetVideoCall(long id, string hostId)
        {
            var result = await patientsControllerManager.GetVideoCall(id, hostId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Sends notification to specified patient that video call is started.
        /// </summary>
        /// <param name="meetingId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        [CustomerAuthorize]
        public async Task<ActionResult> SendVideoCallNotification(Guid patientId, [FromBody]long meetingId)
        {
            await patientsControllerManager.SendVideoCallNotification(meetingId, patientId);

            return Json(string.Empty);
        }
    }
}