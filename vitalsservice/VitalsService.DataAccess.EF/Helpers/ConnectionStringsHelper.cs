using System.Configuration;
using VitalsService.Extensions;

namespace VitalsService.DataAccess.EF.Helpers
{
    /// <summary>
    /// Helper to manage connection strings.
    /// </summary>
    internal static class ConnectionStringsHelper
    {
        internal const string DefaultConnectionStringName = "Vitals_Shared";
        private const string ConnectionStringTemplate = "Vitals_Customer_{0}";

        /// <summary>
        /// Returns connection string for specific customer by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static string GetConnectionString(int customerId)
        {
            var connectionStrings = 
                ConfigurationManager.ConnectionStrings[ConnectionStringTemplate.FormatWith(customerId)] ??
                ConfigurationManager.ConnectionStrings[DefaultConnectionStringName];

            return connectionStrings.ConnectionString;
        }
    }
}
