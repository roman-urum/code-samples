using System;
using System.Collections.Generic;
using System.Configuration;
using Maestro.Common.Helpers;

namespace Maestro.Common
{
    /// <summary>
    /// Provides access to default application settings.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Returns default session timout for SuperAdmin.
        /// </summary>
        public static TimeSpan DefaultSessionTimeout
        {
            get
            {
                string value = ConfigurationManager.AppSettings["DefaultSessionTimeout"];

                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("DefaultSessionTimeout parameter should be specified.");
                }

                int valueInMinutes;

                if (!int.TryParse(value, out valueInMinutes))
                {
                    throw new ConfigurationErrorsException("DefaultSessionTimeout parameter has an invalid format.");
                }

                return new TimeSpan(0, valueInMinutes, 0);
            }
        }

        /// <summary>
        /// Returns root url of messaging hub API.
        /// </summary>
        public static string MessagingHubUrl
        {
            get
            {
                return ConfigurationManagerHelper.ReadRequiredAttribute("MessagingHubUrl");
            }
        }

        /// <summary>
        /// Returns url of service to authenticate users.
        /// </summary>
        public static string TokenServiceUrl
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
        /// Returns url of service to authenticate users.
        /// </summary>
        public static string CustomerServiceUrl
        {
            get
            {
                string value = ConfigurationManager.AppSettings["CustomerServiceUrl"];

                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("TokenServiceUrl parameter should be specified.");
                }

                return value;
            }
        }

        public static string DefaultCustomerLogoPath
        {
            get
            {
                string value = ConfigurationManager.AppSettings["DefaultCustomerLogoPath"];

                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("DefaultCustomerLogoPath setting should be specified");
                }

                return value;
            }
        }

        public static int DefaultCustomerPasswordExpirationDays
        {
            get
            {
                int value;
                if (!int.TryParse(ConfigurationManager.AppSettings["DefaultCustomerPasswordExpirationDays"], out value))
                {
                    throw new ConfigurationErrorsException("DefaultCustomerPasswordExpirationDays setting should be specified");
                }
                return value;
            }
        }

        public static int DefaultCustomerIddleSessionTimeout
        {
            get
            {
                int value;
                if (!int.TryParse(ConfigurationManager.AppSettings["DefaultCustomerIddleSessionTimeout"], out value))
                {
                    throw new ConfigurationErrorsException("DefaultCustomerIddleSessionTimeout setting should be specified");
                }
                return value;
            }
        }

        public static string SiteUrl
        {
            get
            {
                string value = ConfigurationManager.AppSettings["SiteUrl"];

                if (!string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("SiteUrl setting should be specified");
                }

                return value;
            }
        }

        public static int ActivationLinkExpirationHours
        {
            get
            {
                int value = Int32.Parse(ConfigurationManager.AppSettings["ActivationLinkExpirationHours"]);

                return value;
            }
        }

        public static string PatientServiceUrl
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

        public static string DeviceServiceUrl
        {
            get
            {
                string value = ConfigurationManager.AppSettings["DeviceServiceUrl"];

                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("DeviceServiceUrl parameter should be specified.");
                }

                return value;
            }
        }

        public static string HealthLibraryServiceUrl
        {
            get
            {
                string value = ConfigurationManager.AppSettings["HealthLibraryServiceUrl"];

                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("HealthLibraryServiceUrl parameter should be specified.");
                }

                return value;
            }
        }

        public static string VitalsServiceUrl
        {
            get
            {
                string value = ConfigurationManager.AppSettings["VitalsServiceUrl"];

                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("VitalsServiceUrl parameter should be specified.");
                }

                return value;
            }
        }

        public static int CICustomerId
        {
            get
            {
                string value = ConfigurationManager.AppSettings["CICustomerId"];
                int result;

                if (string.IsNullOrEmpty(value) || !int.TryParse(value, out result))
                {
                    throw new ConfigurationErrorsException("CICustomerId parameter should be specified.");
                }

                return result;
            }
        }

        public static string CITempFolderName
        {
            get
            {
                string value = ConfigurationManager.AppSettings["CITempFolderName"];

                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("CITempFolderName parameter should be specified.");
                }

                return value;
            }
        }

        public static int TempFilesMaxAgeHours
        {
            get
            {
                int value;

                if (int.TryParse(ConfigurationManager.AppSettings["TempFiles.MaxAgeHours"], out value))
                {
                    throw new ConfigurationErrorsException("TempFiles.MaxAgeHours parameter should be specified.");
                }

                return value;
            }
        }

        public const int AdminDefaultCustomerId = 1;

        /// <summary>
        /// Gets the supported extensions and MIME types.
        /// </summary>
        /// <value>
        /// The supported extensions and MIME types.
        /// </value>
        public static Dictionary<string, List<string>> SupportedExtensionsAndMimeTypes
        {
            get
            {
                return new Dictionary<string, List<string>>()
                {
                    { ".mp4", new List<string>() { "video/mp4" } },
                    { ".m4a", new List<string>() { "audio/mp4" } },
                    { ".mp3", new List<string>() { "audio/mp3", "audio/mpeg", "audio/mpa", "audio/mpa-robust" } },
                    { ".wav", new List<string>() { "audio/vnd.wave", "audio/wav", "audio/wave", "audio/x-wav" } },
                    { ".webm", new List<string>() { "video/webm", "audio/webm" } },
                    { ".jpg", new List<string>() { "image/jpeg" } },
                    { ".jpeg", new List<string>() { "image/jpeg" } },
                    { ".png", new List<string>() { "image/png" } },
                    { ".pdf", new List<string>() { "application/pdf", "application/x-pdf", "application/x-bzpdf", "application/x-gzpdf" } }
                };
            }
        }

        /// <summary>
        /// Gets the supported extensions and MIME types.
        /// </summary>
        /// <value>
        /// The supported extensions and MIME types.
        /// </value>
        public static readonly Dictionary<string, List<string>> SupportedMediaExtensionsAndMimeTypes = new Dictionary
            <string, List<string>>()
        {
            {".mp4", new List<string>() {"video/mp4"}},
            {".m4a", new List<string>() {"audio/mp4"}},
            {".webm", new List<string>() {"video/webm", "audio/webm"}},
            {".jpg", new List<string>() {"image/jpeg"}},
            {".jpeg", new List<string>() {"image/jpeg"}},
            {".png", new List<string>() {"image/png"}},
            {
                ".pdf",
                new List<string>()
                {
                    "application/pdf",
                    "application/x-pdf",
                    "application/x-bzpdf",
                    "application/x-gzpdf"
                }
            }
        };
    }
}
