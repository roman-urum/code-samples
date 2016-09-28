using System.Collections.Generic;
using System.Configuration;

namespace HealthLibrary.Common
{
    public class Settings
    {
        private const int DefaultCICustomerId = 1;

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
        /// Returns if of care innovations customer.
        /// </summary>
        public static int CICustomerId
        {
            get
            {
                var ciCustomerIdValue = ConfigurationManager.AppSettings["CICustomerId"];
                int ciCustomerId;

                if (string.IsNullOrEmpty(ciCustomerIdValue) || !int.TryParse(ciCustomerIdValue, out ciCustomerId))
                {
                    return DefaultCICustomerId;
                }

                return ciCustomerId;
            }
        }

        /// <summary>
        /// Gets the default language of system.
        /// </summary>
        public static string DefaultLanguage
        {
            get { return ConfigurationManager.AppSettings["DefaultLanguage"]; }
        }

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
                    { ".wmv", new List<string>() { "video/x-ms-wmv" } },
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
    }
}