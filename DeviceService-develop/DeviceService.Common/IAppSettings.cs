using System.Collections.Generic;

namespace DeviceService.Common
{
    /// <summary>
    /// IAppSettings.
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Returns url of service to authenticate users.
        /// </summary>
        string TokenServiceUrl { get; }

        /// <summary>
        /// Gets the patient service URL.
        /// </summary>
        /// <value>
        /// The patient service URL.
        /// </value>
        string PatientServiceUrl { get; }

        /// <summary>
        /// Gets the messaging hub URL.
        /// </summary>
        /// <value>
        /// The messaging hub URL.
        /// </value>
        string MessagingHubUrl { get; }

        /// <summary>
        /// Gets the deployment number.
        /// </summary>
        /// <value>
        /// The deployment number.
        /// </value>
        ushort DeploymentNumber { get; }

        /// <summary>
        /// Gets the profanity list.
        /// </summary>
        /// <value>
        /// The profanity list.
        /// </value>
        List<string> ProfanityList { get; }

        /// <summary>
        /// Gets the name of the host.
        /// </summary>
        /// <value>
        /// The name of the host.
        /// </value>
        string HostName { get; }
    }
}