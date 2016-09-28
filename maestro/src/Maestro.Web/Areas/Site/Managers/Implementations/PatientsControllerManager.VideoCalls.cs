using System;
using System.Threading.Tasks;
using Maestro.Domain.Constants;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.Zoom;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// PatientsControllerManager.VideoCalls
    /// </summary>
    public partial class PatientsControllerManager
    {
        /// <summary>
        /// Creates new video call with patient.
        /// Returns meeting details.
        /// </summary>
        /// <returns></returns>
        public async Task<MeetingDto> CreateVideoCall(Guid patientId)
        {
            var userAuthData = this.authDataStorage.GetUserAuthData();
            var user = await this.usersService.GetUser(userAuthData.UserId);
            var patient = await this.patientsService.GetPatientAsync(
                CustomerContext.Current.Customer.Id, patientId, true, userAuthData.Token);
            var customerId = user.Role.Name.Equals(Roles.SuperAdmin)
                ? Common.Settings.CICustomerId
                : ((CustomerUser) user).CustomerId;

            return await zoomService.CreateMeeting(customerId, user, patient);
        }

        /// <summary>
        /// Returns required meeting by identifier.
        /// </summary>
        /// <param name="id">Meeting identifier.</param>
        /// <param name="hostId">Identifier of the host(user) in zoom who created meeting.</param>
        /// <returns></returns>
        public async Task<MeetingDto> GetVideoCall(long id, string hostId)
        {
            return await this.zoomService.GetMeetingById(id, hostId);
        }

        /// <summary>
        /// Sends notification about started video call to the patient.
        /// </summary>
        /// <returns></returns>
        public async Task SendVideoCallNotification(long meetingId, Guid patientId)
        {
            var userAuthData = this.authDataStorage.GetUserAuthData();
            var user = await this.usersService.GetUser(userAuthData.UserId);
            var patient = await this.patientsService.GetPatientAsync(
                CustomerContext.Current.Customer.Id, patientId, false, userAuthData.Token);

            await this.messagingHubService.SendVideoCallNotification(
                CustomerContext.Current.Customer.Id, meetingId, patient, user);
        }
    }
}