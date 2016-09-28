using Maestro.Common.Helpers;

namespace Maestro.DataAccess.Api
{
    /// <summary>
    /// Contains settings for integration with Zoom API.
    /// </summary>
    internal static class ZoomSettings
    {
        /// <summary>
        /// Root url of Zoom API.
        /// </summary>
        public static string ZoomAPIUrl
        {
            get { return ConfigurationManagerHelper.ReadRequiredAttribute("ZoomAPIUrl"); }
        }

        /// <summary>
        /// API key received from zoom.us
        /// </summary>
        public static string ZoomApiKey
        {
            get { return ConfigurationManagerHelper.ReadRequiredAttribute("ZoomApiKey"); }
        }

        /// <summary>
        /// API secret received from zoom.us
        /// </summary>
        public static string ZoomApiSecret
        {
            get { return ConfigurationManagerHelper.ReadRequiredAttribute("ZoomApiSecret"); }
        }

        /// <summary>
        /// Zoom host id.
        /// </summary>
        public static string ZoomHostId
        {
            get { return ConfigurationManagerHelper.ReadRequiredAttribute("ZoomHostId"); }
        }
    }
}
