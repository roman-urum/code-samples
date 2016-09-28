using Maestro.Domain.Dtos.Zoom.Enums;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.Zoom
{
    /// <summary>
    /// Model of user account in zoom.
    /// </summary>
    public class ZoomUserDto : ZoomBaseResponseDto
    {
        public string Email { get; set; }

        [JsonProperty(PropertyName = "account_id")]
        public string AccountId { get; set; }

        public string Id { get; set; }

        public UserType Type { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }
    
        public string Token { get; set; }

        [JsonProperty(PropertyName = "pic_url")]
        public string PicUrl { get; set; }

        [JsonProperty(PropertyName = "disable_chat")]
        public bool DisableChat { get; set; }

        [JsonProperty(PropertyName = "enable_e2e_encryption")]
        public bool EnableE2EEncryption { get; set; }

        [JsonProperty(PropertyName = "enable_silent_mode")]
        public bool EnableSilentMode { get; set; }

        [JsonProperty(PropertyName = "disable_group_hd")]
        public bool DisableGroupHd { get; set; }

        [JsonProperty(PropertyName = "disable_recording")]
        public bool DisableRecording { get; set; }

        [JsonProperty(PropertyName = "enable_cmr")]
        public bool EnableCmr { get; set; }

        [JsonProperty(PropertyName = "enable_auto_recording")]
        public bool EnableAutoRecording { get; set; }

        [JsonProperty(PropertyName = "enable_cloud_auto_recording")]
        public bool EnableCloudAutoRecording { get; set; }

        public bool Verified { get; set; }
    
        public string PMI { get; set; }

        [JsonProperty(PropertyName = "meeting_capacity")]
        public int MeetingCapacity { get; set; }

        [JsonProperty(PropertyName = "enable_webinar")]
        public bool EnableWebinar { get; set; }

        [JsonProperty(PropertyName = "webinar_capacity")]
        public int WebinarCapacity { get; set; }

        [JsonProperty(PropertyName = "enable_large")]
        public bool EnableLarge { get; set; }

        [JsonProperty(PropertyName = "large_capacity")]
        public int LargeCapacity { get; set; }

        [JsonProperty(PropertyName = "disable_feedback")]
        public bool DisableFeedback { get; set; }

        [JsonProperty(PropertyName = "disable_jbh_reminder")]
        public bool DisableJbhReminder { get; set; }
    
        public string Dept { get; set; }
    
        public string Timezone { get; set; }
    }
}
