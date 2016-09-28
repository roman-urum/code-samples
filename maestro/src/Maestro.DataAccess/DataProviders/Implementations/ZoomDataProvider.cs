using System.Threading.Tasks;
using Maestro.DataAccess.Api.ApiClient;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos.Zoom;
using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// Provides ability to communicate with Zoom Api.
    /// </summary>
    public class ZoomDataProvider : IZoomDataProvider
    {
        private const string ZoomDefaultDataType = "JSON";

        private readonly IRestApiClient apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoomDataProvider"/> class.
        /// </summary>
        /// <param name="apiClientFactory">The API client factory.</param>
        public ZoomDataProvider(IRestApiClientFactory apiClientFactory)
        {
            this.apiClient = apiClientFactory.Create(ZoomSettings.ZoomAPIUrl);
        }

        /// <summary>
        /// Returns zoom account for specified email if exists.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public async Task<ZoomUserDto> GetUserByEmail(GetUserByEmailRequestDto requestDto)
        {
            InitRequestDefaultData(requestDto);

            return await this.apiClient.SendRequestAsync<ZoomUserDto>("user/getbyemail", requestDto, Method.POST);
        }

        /// <summary>
        /// Creates new account with provided data in zoom.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public async Task<ZoomUserDto> CreateUser(CreateUserRequestDto requestDto)
        {
            InitRequestDefaultData(requestDto);

            return await this.apiClient.SendRequestAsync<ZoomUserDto>("user/custcreate", requestDto, Method.POST);
        }

        /// <summary>
        /// Creates new meeting in zoom.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public async Task<MeetingDto> CreateMeeting(CreateMeetingRequestDto requestDto)
        {
            InitRequestDefaultData(requestDto);

            return await this.apiClient.SendRequestAsync<MeetingDto>("meeting/create", requestDto, Method.POST);
        }

        /// <summary>
        /// Returns required meeting by identifier.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public async Task<MeetingDto> GetMeetingById(GetMeetingByIdRequestDto requestDto)
        {
            InitRequestDefaultData(requestDto);

            return await this.apiClient.SendRequestAsync<MeetingDto>("meeting/get", requestDto, Method.POST);
        }

        #region Private methods

        /// <summary>
        /// Initializes default data for request.
        /// </summary>
        /// <param name="requestDto"></param>
        private static void InitRequestDefaultData(ZoomBaseRequestDto requestDto)
        {
            requestDto.ApiKey = ZoomSettings.ZoomApiKey;
            requestDto.ApiSecret = ZoomSettings.ZoomApiSecret;
            requestDto.DataType = ZoomDefaultDataType;
        }

        #endregion
    }
}
