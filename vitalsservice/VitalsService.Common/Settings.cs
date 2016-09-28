using System.Configuration;

namespace VitalsService
{
    public class Settings
    {
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

        public static string MessagingHubUrl
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

        public static string MeasurementESBTopicName
        {
            get
            {
                string value = ConfigurationManager.AppSettings["MeasurementESBTopicName"];

                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("MeasurementESBTopicName parameter should be specified.");
                }

                return value;
            }
        }
        public static string HealthSessionESBTopicName
        {
            get
            {
                string value = ConfigurationManager.AppSettings["HealthSessionESBTopicName"];

                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("HealthSessionESBTopicName parameter should be specified.");
                }

                return value;
            }
        }
    }
}
