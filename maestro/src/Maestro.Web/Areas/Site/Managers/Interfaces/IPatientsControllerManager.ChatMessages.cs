using System.Threading.Tasks;
using Maestro.Web.Areas.Site.Models.Patients.ChatMessages;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IPatientsControllerManager.ChatMessages
    /// </summary>
    public partial interface IPatientsControllerManager
    {
        /// <summary>
        /// Sends push notification to patient. 
        /// </summary>
        /// <param name="messageRequest">The notification request.</param>
        /// <returns></returns>
        Task SendChatMessage(ChatMessageViewModel messageRequest);
    }
}