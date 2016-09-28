using System;
using System.Configuration;
using System.Linq;
using VitalsService.DataAccess.Document.Contexts;
using VitalsService.DataAccess.Document.Repository;
using VitalsService.Extensions;

namespace VitalsService.DataAccess.Document
{
    using VitalsService.DataAccess.Document.Helpers;

    /// <summary>
    /// Factory to create instances of required repositories.
    /// </summary>
    internal class DocumentRepositoryFactory : IDocumentRepositoryFactory
    {
        private const string ConnectionStringTemplate = "DocumentDB_Vitals_Customer_{0}";
        private const string DefaultConnectionString = "DocumentDB_Vitals_Shared";

        private readonly IDocumentDbContext dbContext;

        public DocumentRepositoryFactory(ICustomerContext customerContext)
        {
            var connection = GetConnection(customerContext.CustomerId);

            dbContext = new DocumentDbContext(connection);
        }

        /// <summary>
        /// Creates and returs repository for specified collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public IDocumentDbRepository<T> Create<T>(VitalsServiceCollection collection)
        {
            return new DocumentDbRepository<T>(this.dbContext, collection.GetCollectionName());
        }

        /// <summary>
        /// Returns connection name by customer Id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private static DocumentDbConnectionString GetConnection(int customerId)
        {
            var customerConnectionStringName = ConnectionStringTemplate.FormatWith(customerId);

            if (ConfigurationManager.ConnectionStrings[customerConnectionStringName] != null)
            {
                return new DocumentDbConnectionString(customerConnectionStringName);
            }
            
            if (ConfigurationManager.ConnectionStrings[DefaultConnectionString] != null)
            {
                return new DocumentDbConnectionString(DefaultConnectionString);
            }

            throw new NullReferenceException("Missing DocuemntDB connection string");
            
        }
    }
}
