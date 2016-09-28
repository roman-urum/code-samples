using System;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using VitalsService.DataAccess.Document.Helpers;

namespace VitalsService.DataAccess.Document.Contexts
{
    /// <summary>
    /// Provides access to document db though client.
    /// </summary>
    public class DocumentDbContext : IDocumentDbContext
    {
        private readonly DocumentDbConnectionString connection;

        public DocumentDbContext(DocumentDbConnectionString connection)
        {
            this.connection = connection;
        }

        private DocumentClient client;

        public DocumentClient Client
        {
            get
            {
                if (client == null)
                {
                    Uri endpointUri = connection.AccountEndpoint;
                    client = new DocumentClient(endpointUri, connection.AccountKey);
                }

                return client;
            }
        }

        private Database database;

        private Database Database
        {
            get
            {
                if (database == null)
                {
                    database = ReadOrCreateDatabase();
                }

                return database;
            }
        }

        /// <summary>
        /// Reads or Creates new document collection by identifier.
        /// </summary>
        /// <param name="collectionId"></param>
        /// <returns></returns>
        public DocumentCollection ReadOrCreateCollection(string collectionId)
        {
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("collectionId");
            }

            var col = this.Client.CreateDocumentCollectionQuery(this.Database.SelfLink)
                .Where(c => c.Id == collectionId)
                .AsEnumerable()
                .FirstOrDefault();

            if (col == null)
            {
                col =
                    Client.CreateDocumentCollectionAsync(this.Database.SelfLink,
                        new DocumentCollection {Id = collectionId}).Result;
            }

            return col;
        }

        /// <summary>
        /// Reads existed or creates new database.
        /// </summary>
        /// <returns></returns>
        private Database ReadOrCreateDatabase()
        {
            var db = Client.CreateDatabaseQuery()
                            .Where(d => d.Id == connection.Database)
                            .AsEnumerable()
                            .FirstOrDefault();

            if (db == null)
            {
                db = Client.CreateDatabaseAsync(new Database { Id = connection.Database }).Result;
            }

            return db;
        }
    }
}