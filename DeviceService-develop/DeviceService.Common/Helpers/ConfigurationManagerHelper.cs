using System.Configuration;
using DeviceService.Common.Extensions;

namespace DeviceService.Common.Helpers
{
    /// <summary>
    /// Contains help methods to work with ConfigurationManager.
    /// </summary>
    public static class ConfigurationManagerHelper
    {
        private const string ParameterNotFoundErrorTemplate = "{0} parameter should be specified.";

        /// <summary>
        /// Reads attribute from configuration as string.
        /// </summary>
        /// <param name="attrName"></param>
        /// <exception cref="ConfigurationErrorsException">If attributes is missed in configuration.</exception>
        /// <returns></returns>
        public static string ReadRequiredAttribute(string attrName)
        {
            string value = ConfigurationManager.AppSettings[attrName];

            if (string.IsNullOrEmpty(value))
            {
                throw new ConfigurationErrorsException(ParameterNotFoundErrorTemplate.FormatWith(attrName));
            }

            return value;
        }
    }
}
