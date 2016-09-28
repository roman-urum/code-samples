using System.Configuration;

namespace CustomerService.Common
{
    /// <summary>
    /// Provides access to default application settings.
    /// </summary>

    public class Settings
    {
        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

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

        public static string UploadImagesFolder
        {
            get
            {
                string value = ConfigurationManager.AppSettings["UploadImagesFolder"];
                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("UploadImagesFolder parameter should be specified.");
                }

                return value;
            }
        }
    }
}
