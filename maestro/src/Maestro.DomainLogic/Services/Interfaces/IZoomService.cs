using System.Threading.Tasks;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Domain.Dtos.Zoom;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Contains business logic to communicate with Zoom Service.
    /// </summary>
    public interface IZoomService
    {
        /// <summary>
        /// Creates new meeting for user with specified email.
        /// </summary>
        /// <param name="customerId">Id of user's customer.</param>
        /// <param name="user">User which initiated meeting.</param>
        /// <param name="patient">A patient with whom a meeting is created.</param>
        /// <returns>Instance of created meeting.</returns>
        Task<MeetingDto> CreateMeeting(int customerId, User user, PatientDto patient);

        /// <summary>
        /// Returns required meeting by identifier.
        /// </summary>
        /// <param name="id">Meeting identifier.</param>
        /// <param name="hostId">Identifier of the host(user) in zoom who created meeting.</param>
        /// <returns></returns>
        Task<MeetingDto> GetMeetingById(long id, string hostId);
    }
}
