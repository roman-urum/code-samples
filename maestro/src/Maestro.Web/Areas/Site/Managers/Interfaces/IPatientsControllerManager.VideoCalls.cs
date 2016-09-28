using System;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.Zoom;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IPatientsControllerManager.Notes
    /// </summary>
    public partial interface IPatientsControllerManager
    {
        /// <summary>
        /// Creates new video call with patient.
        /// Returns meeting details.
        /// </summary>
        /// <returns></returns>
        Task<MeetingDto> CreateVideoCall(Guid patientId);

        /// <summary>
        /// Sends notification about started video call to the patient.
        /// </summary>
        /// <returns></returns>
        Task SendVideoCallNotification(long meetingId, Guid patientId);

        /// <summary>
        /// Returns required meeting by identifier.
        /// </summary>
        /// <param name="id">Meeting identifier.</param>
        /// <param name="hostId">Identifier of the host(user) in zoom who created meeting.</param>
        /// <returns></returns>
        Task<MeetingDto> GetVideoCall(long id, string hostId);
    }
}