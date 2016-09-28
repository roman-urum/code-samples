using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace VitalsService.DataAccess.Document.Contexts
{
    /// <summary>
    /// Common interface to connect to document db.
    /// </summary>
    public interface IDocumentDbContext
    {
        DocumentClient Client { get; }

        /// <summary>
        /// Reads or Creates new document collection by identifier.
        /// </summary>
        /// <param name="collectionId"></param>
        /// <returns></returns>
        DocumentCollection ReadOrCreateCollection(string collectionId);
    }
}
