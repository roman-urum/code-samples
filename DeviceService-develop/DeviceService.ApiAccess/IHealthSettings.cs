using DeviceService.Common.Helpers;

namespace DeviceService.ApiAccess
{
    /// <summary>
    /// Provides access to iHealth configuration settings.
    /// </summary>
    public class iHealthSettings : IiHealthSettings
    {
        /// <summary>
        /// Root url of Zoom API.
        /// </summary>
        public string iHealthAccountDomain
        {
            get { return ConfigurationManagerHelper.ReadRequiredAttribute("iHealthAccountDomain"); }
        }

        /// <summary>
        /// Returns root url of iHealth API.
        /// </summary>
        public string iHealthApiUrl
        {
            get { return ConfigurationManagerHelper.ReadRequiredAttribute("iHealthApiUrl"); }
        }

        /// <summary>
        /// Client id to authenticate requests in iHealth API.
        /// </summary>
        public string iHealthClientId
        {
            get { return ConfigurationManagerHelper.ReadRequiredAttribute("iHealthClientId"); }
        }

        /// <summary>
        /// Client secret to authenticate requests in iHealth API.
        /// </summary>
        public string iHealthClientSecret
        {
            get { return ConfigurationManagerHelper.ReadRequiredAttribute("iHealthClientSecret"); }
        }

        public string iHealthSv
        {
            get { return ConfigurationManagerHelper.ReadRequiredAttribute("iHealthSv"); }
        }

        public string iHealthSc
        {
            get { return ConfigurationManagerHelper.ReadRequiredAttribute("iHealthSc"); }
        }
    }
}