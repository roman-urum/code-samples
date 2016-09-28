using System;
using System.Threading.Tasks;
using Maestro.Common.Extensions;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Domain.Dtos.Zoom;
using Maestro.Domain.Dtos.Zoom.Enums;
using Maestro.DomainLogic.Services.Interfaces;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Contains business logic to communicate with Zoom Service.
    /// </summary>
    public class ZoomService : IZoomService
    {
        private const string DeptFormat = "VCS-{0}";

        private readonly IZoomDataProvider zoomDataProvider;

        public ZoomService(IZoomDataProvider zoomDataProvider)
        {
            this.zoomDataProvider = zoomDataProvider;
        }

        /// <summary>
        /// Creates new meeting for user with specified email.
        /// </summary>
        /// <param name="customerId">Id of user's customer.</param>
        /// <param name="user">User which initiated meeting.</param>
        /// <param name="patient">A patient with whom a meeting is created.</param>
        /// <returns>Instance of created meeting.</returns>
        public async Task<MeetingDto> CreateMeeting(int customerId, User user, PatientDto patient)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var zoomUser = await this.GetOrCreateZoomUser(customerId, user);

            if (zoomUser.Error != null)
            {
                return new MeetingDto
                {
                    Error = zoomUser.Error
                };
            }

            var createMeetingRequest = new CreateMeetingRequestDto
            {
                HostId = zoomUser.Id,
                Topic = string.Join(" ", patient.FirstName, patient.LastName),
                Type = MeetingType.Instant
            };

            return await this.zoomDataProvider.CreateMeeting(createMeetingRequest);
        }

        /// <summary>
        /// Returns required meeting by identifier.
        /// </summary>
        /// <param name="id">Meeting identifier.</param>
        /// <param name="hostId">Identifier of the host(user) in zoom who created meeting.</param>
        /// <returns></returns>
        public async Task<MeetingDto> GetMeetingById(long id, string hostId)
        {
            var requestDto = new GetMeetingByIdRequestDto
            {
                Id = id,
                HostId = hostId
            };

            return await this.zoomDataProvider.GetMeetingById(requestDto);
        }

        #region Private methods

        /// <summary>
        /// Returns existing zoom account for specified user or creates new.
        /// </summary>
        /// <param name="customerId">Id of user's customer.</param>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<ZoomUserDto> GetOrCreateZoomUser(int customerId, User user)
        {
            var getUserRequest = new GetUserByEmailRequestDto
            {
                Email = user.Email,
                LoginType = LoginType.SNS_API
            };

            var zoomUser = await this.zoomDataProvider.GetUserByEmail(getUserRequest);

            if (zoomUser.Error != null)
            {
                switch (zoomUser.Error.Code)
                {
                    case 1010: // "User not belong to this account"
                    case 1001: // "User not exist"
                        zoomUser = await this.CreateZoomUser(customerId, user);
                        break;
                }
            }

            return zoomUser;
        }

        /// <summary>
        /// Creates new account for provided user entity in zoom.
        /// </summary>
        /// <param name="customerId">Id of user's customer.</param>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<ZoomUserDto> CreateZoomUser(int customerId, User user)
        {
            var requestDto = new CreateUserRequestDto
            {
                Email = user.Email,
                Type = UserType.Basic,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dept = DeptFormat.FormatWith(customerId)
            };

            return await this.zoomDataProvider.CreateUser(requestDto);
        }

        #endregion
    }
}
