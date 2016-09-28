using System;
using Maestro.Domain.Dtos.Zoom.Enums;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.Zoom
{
    /// <summary>
    /// Model of response with meeting info from zoom.
    /// </summary>
    public class MeetingDto : ZoomBaseResponseDto
    {
        public string Uuid { get; set; }

        public long Id { get; set; }

        [JsonProperty("start_url")]
        public string StartUrl { get; set; }

        [JsonProperty("join_url")]
        public string JoinUrl { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Meeting host user ID. Can be any user under this account.
        /// Cannot be updated after creation.
        /// </summary>
        [JsonProperty("host_id")]
        public string HostId { get; set; }

        /// <summary>
        /// Meeting topic. Max 300 characters.
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// Meeting type. 
        /// </summary>
        public MeetingType Type { get; set; }

        /// <summary>
        /// Meeting start time in UTC.
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime? StartTimeUtc { get; set; }

        /// <summary>
        /// Meeting duration in minutes.
        /// For scheduled meetings only.
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Timezone to format StartTimeUtc.
        /// For scheduled meetings only.
        /// </summary>
        public string Timezone { get; set; }

        /// <summary>
        /// Meeting password.
        /// </summary>
        public string Password { get; set; }

        [JsonProperty("h323_password")]
        public string H323Password { get; set; }

        /// <summary>
        /// Join meeting before host start the meeting.
        /// False by default.
        /// </summary>
        [JsonProperty("option_jbh")]
        public bool? OptionJbh { get; set; }

        /// <summary>
        /// Meeting start type.
        /// Can be "video" or "screen_share". (deprecated)
        /// </summary>
        [JsonProperty("option_start_type")]
        public OptionStartType? OptionStartType { get; set; }

        /// <summary>
        /// Start video when host join meeting.
        /// </summary>
        [JsonProperty("option_host_video")]
        public bool? OptionHostVideo { get; set; }

        /// <summary>
        /// Start video when participants join meeting
        /// </summary>
        [JsonProperty("option_participant_video")]
        public bool? OptionParticipantVideo { get; set; }

        /// <summary>
        /// Meeting audio options.
        /// </summary>
        [JsonProperty("option_audio")]
        public OptionAudio? OptionAudio { get; set; }

        /// <summary>
        /// "status" variable: 0 means meeting not started. 1 means meeting starting. 2 means meeting finished. 
        /// </summary>
        public MeetingStatus Status { get; set; }
    }
}
