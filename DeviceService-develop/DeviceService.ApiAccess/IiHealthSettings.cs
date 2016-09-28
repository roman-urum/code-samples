namespace DeviceService.ApiAccess
{
    public interface IiHealthSettings
    {
        /// <summary>
        /// Root url of Zoom API.
        /// </summary>
        string iHealthAccountDomain { get; }

        /// <summary>
        /// Returns root url of iHealth API.
        /// </summary>
        string iHealthApiUrl { get; }

        /// <summary>
        /// Client id to authenticate requests in iHealth API.
        /// </summary>
        string iHealthClientId { get; }

        /// <summary>
        /// Client secret to authenticate requests in iHealth API.
        /// </summary>
        string iHealthClientSecret { get; }

        string iHealthSv { get; }

        string iHealthSc { get; }
    }
}
