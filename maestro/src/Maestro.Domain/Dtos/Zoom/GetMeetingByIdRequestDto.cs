using Maestro.Common.ApiClient;

namespace Maestro.Domain.Dtos.Zoom
{
    /// <summary>
    /// Model for request to get zoom meeting by Id.
    /// </summary>
    public class GetMeetingByIdRequestDto : ZoomBaseRequestDto
    {
        /// <summary>
        /// Meeting identifier.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "id")]
        public long Id { get; set; }

        /// <summary>
        /// Identifier of the host(user) in zoom who created meeting.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "host_id")]
        public string HostId { get; set; }
    }
}
