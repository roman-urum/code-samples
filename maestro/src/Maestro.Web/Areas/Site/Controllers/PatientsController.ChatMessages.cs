using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Web.Areas.Customer;
using Maestro.Web.Areas.Site.Models.Patients.ChatMessages;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.ChatMessages
    /// </summary>
    public partial class PatientsController
    {
        /// <summary>
        /// Sends chat message.
        /// </summary>
        /// <param name="messageRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomerAuthorize]
        [ActionName("ChatMessage")]
        public async Task<ActionResult> ChatMessage(ChatMessageViewModel messageRequest)
        {
            await this.patientsControllerManager.SendChatMessage(messageRequest);

            return Json(new {Success = true});
        }
    }
}