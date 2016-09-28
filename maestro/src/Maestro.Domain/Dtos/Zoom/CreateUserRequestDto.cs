using Maestro.Common.ApiClient;
using Maestro.Domain.Dtos.Zoom.Enums;

namespace Maestro.Domain.Dtos.Zoom
{
    /// <summary>
    /// Model of data for request to create new user.
    /// </summary>
    public class CreateUserRequestDto : ZoomBaseRequestDto
    {
        [RequestParameter(RequestParameterType.RequestBody, "email")]
        public string Email { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "type",
            ValueConverter = typeof(EnumToIntRequestValueConverter))]
        public UserType Type { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "first_name")]
        public string FirstName { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "last_name")]
        public string LastName { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "disable_chat")]
        public bool DisableChat { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "enable_e2e_encryption")]
        public bool EnableE2EEncryption { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "enable_silent_mode")]
        public bool EnableSilentMode { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "disable_group_hd")]
        public bool DisableGroupHd { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "disable_recording")]
        public bool DisableRecording { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "meeting_capacity")]
        public int MeetingCapacity { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "enable_webinar")]
        public bool EnableWebinar { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "enable_large")]
        public bool EnableLarge { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "disable_feedback")]
        public bool DisableFeedback { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "disable_jbh_reminder")]
        public bool DisableJbhReminder { get; set; }

        [RequestParameter(RequestParameterType.RequestBody, "dept")]
        public string Dept { get; set; }
    }
}
