using System;
using Maestro.Common.ApiClient;
using Maestro.Domain.Dtos.Zoom.Enums;

namespace Maestro.Domain.Dtos.Zoom
{
    /// <summary>
    /// Model of data for request to create new meeting in zoom.
    /// </summary>
    public class CreateMeetingRequestDto : ZoomBaseRequestDto
    {
        /// <summary>
        /// Meeting host user ID. Can be any user under this account.
        /// Cannot be updated after creation.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "host_id")]
        public string HostId { get; set; }

        /// <summary>
        /// Meeting topic. Max 300 characters.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "topic")]
        public string Topic { get; set; }

        /// <summary>
        /// Meeting type. 
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "type",
            ValueConverter = typeof(EnumToIntRequestValueConverter))]
        public MeetingType Type { get; set; }

        /// <summary>
        /// Meeting start time in UTC.
        /// Should send to Zoom in ISO datetime format.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "start_time")]
        public DateTime? StartTimeUtc { get; set; }

        /// <summary>
        /// Meeting duration in minutes.
        /// For scheduled meetings only.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "duration")]
        public int? Duration { get; set; }

        /// <summary>
        /// Timezone to format StartTimeUtc.
        /// For scheduled meetings only.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "timezone")]
        public string Timezone { get; set; }

        /// <summary>
        /// Meeting password. Password may only contain the
        /// following characters: [a-z A-Z 0-9 @ - _ *].
        /// Max of 10 characters.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "password")]
        public string Password { get; set; }

        /// <summary>
        /// Join meeting before host start the meeting.
        /// False by default.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "option_jbh")]
        public bool? OptionJbh { get; set; }

        /// <summary>
        /// Meeting start type.
        /// Can be "video" or "screen_share". (deprecated)
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "option_start_type")]
        public OptionStartType? OptionStartType { get; set; }

        /// <summary>
        /// Start video when host join meeting.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "option_host_video")]
        public bool? OptionHostVideo { get; set; }

        /// <summary>
        /// Start video when participants join meeting
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "option_participant_video")]
        public bool? OptionParticipantVideo { get; set; }

        /// <summary>
        /// Meeting audio options.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "option_audio")]
        public OptionAudio? OptionAudio { get; set; }
    }
}
