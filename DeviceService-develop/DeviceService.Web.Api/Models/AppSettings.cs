using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using DeviceService.Common;

namespace DeviceService.Web.Api.Models
{
    /// <summary>
    /// AppSettings.
    /// </summary>
    public class AppSettings : IAppSettings
    {
        /// <summary>
        /// Returns url of service to authenticate users.
        /// </summary>
        public string TokenServiceUrl
        {
            get
            {
                string value = ConfigurationManager.AppSettings["TokenServiceUrl"];

                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("TokenServiceUrl parameter should be specified.");
                }

                return value;
            }
        }

        /// <summary>
        /// Gets the patient service URL.
        /// </summary>
        /// <value>
        /// The patient service URL.
        /// </value>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">PatientServiceUrl parameter should be specified.</exception>
        public string PatientServiceUrl
        {
            get
            {
                string value = ConfigurationManager.AppSettings["PatientServiceUrl"];

                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("PatientServiceUrl parameter should be specified.");
                }

                return value;
            }
        }

        /// <summary>
        /// Gets the messaging hub URL.
        /// </summary>
        /// <value>
        /// The messaging hub URL.
        /// </value>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">MessagingHubUrl parameter should be specified.</exception>
        public string MessagingHubUrl
        {
            get
            {
                string value = ConfigurationManager.AppSettings["MessagingHubUrl"];

                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("MessagingHubUrl parameter should be specified.");
                }

                return value;
            }
        }

        /// <summary>
        /// Gets the deployment number.
        /// </summary>
        /// <value>
        /// The deployment number.
        /// </value>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">DeploymentNumber parameter should be specified.</exception>
        /// <exception cref="InvalidDataException">DeploymentNumber parameter provided in wrong format.</exception>
        public ushort DeploymentNumber
        {
            get
            {
                string value = ConfigurationManager.AppSettings["DeploymentNumber"];

                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("DeploymentNumber parameter should be specified.");
                }

                ushort result;

                if (!UInt16.TryParse(value, out result))
                {
                    throw new InvalidDataException("DeploymentNumber parameter provided in wrong format.");
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the profanity list path.
        /// </summary>
        /// <value>
        /// The profanity list path.
        /// </value>
        public List<string> ProfanityList
        {
            get
            {
                var filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data\\profanity_list.txt");

                return File.ReadAllLines(filename).ToList();
            }
        }

        /// <summary>
        /// Gets the name of the host.
        /// </summary>
        /// <value>
        /// The name of the host.
        /// </value>
        public string HostName
        {
            get
            {
                return HttpContext.Current.Request.Url.Host;
            }
        }
    }
}