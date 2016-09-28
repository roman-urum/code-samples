using Maestro.Common.ApiClient;
using Maestro.Domain.Dtos.Zoom.Enums;

namespace Maestro.Domain.Dtos.Zoom
{
    /// <summary>
    /// Request model to retrieve zoom account by email.
    /// </summary>
    public class GetUserByEmailRequestDto : ZoomBaseRequestDto
    {
        /// <summary>
        /// Email of user account in zoom.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "email")]
        public string Email { get; set; }

        /// <summary>
        /// Type of login.
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "login_type",
            ValueConverter = typeof (EnumToIntRequestValueConverter))]
        public LoginType LoginType { get; set; }
    }
}
