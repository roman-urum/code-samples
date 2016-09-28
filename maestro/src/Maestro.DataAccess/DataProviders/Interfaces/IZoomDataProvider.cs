using System.Threading.Tasks;
using Maestro.Domain.Dtos.Zoom;

namespace Maestro.DataAccess.Api.DataProviders.Interfaces
{
    /// <summary>
    /// Provides ability to communicate with Zoom Api.
    /// </summary>
    public interface IZoomDataProvider
    {
        /// <summary>
        /// Returns zoom account for specified email if exists.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        Task<ZoomUserDto> GetUserByEmail(GetUserByEmailRequestDto requestDto);

        /// <summary>
        /// Creates new account with provided data in zoom.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        Task<ZoomUserDto> CreateUser(CreateUserRequestDto requestDto);

        /// <summary>
        /// Creates new meeting in zoom.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        Task<MeetingDto> CreateMeeting(CreateMeetingRequestDto requestDto);

        /// <summary>
        /// Returns required meeting by identifier.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        Task<MeetingDto> GetMeetingById(GetMeetingByIdRequestDto requestDto);
    }
}