using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalsService.DataAccess.Document.Helpers
{
    using System.Configuration;
    using System.Data.Common;

    public class DocumentDbConnectionString
    {
        private const string ACCOUNTENDPOINT_KEY = "AccountEndpoint";
        private const string ACCOUNTKEY_KEY = "AccountKey";
        private const string DATABASE_KEY = "Database";

        public Uri AccountEndpoint { get; private set; }
        public string AccountKey { get; private set; }
        public string Database { get; private set; }

        public DocumentDbConnectionString()
        { }

        public DocumentDbConnectionString(string connectionStringName)
        {
            if (String.IsNullOrEmpty(connectionStringName)) throw new ArgumentException("Connection string is null or empty.");

            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            AccountEndpoint = new Uri(getValue(connectionString, ACCOUNTENDPOINT_KEY));
            AccountKey = getValue(connectionString, ACCOUNTKEY_KEY);
            Database = getValue(connectionString, DATABASE_KEY);
        }

        private string getValue(string connectionString, string key)
        {
            int keyPos = connectionString.IndexOf(key);
            if (keyPos < 0) throw new ArgumentException("Missing " + key);
            int keyEndPos = connectionString.IndexOf(';', keyPos);
            if (keyEndPos < 0 || keyEndPos <= keyPos) throw new ArgumentException("Missing connection string separator");
            string keyValue = connectionString.Substring(keyPos + key.Length + 1, keyEndPos - (keyPos + key.Length + 1));            

            return keyValue;
        }
    }
}
