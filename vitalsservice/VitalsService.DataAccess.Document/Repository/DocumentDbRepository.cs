using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Linq;
using VitalsService.DataAccess.Document.Contexts;
using VitalsService.DataAccess.Document.Helpers;

namespace VitalsService.DataAccess.Document.Repository
{
    public class DocumentDbRepository<T> : IDocumentDbRepository<T>
    {
        private readonly IDocumentDbContext dbContext;
        private readonly string collectionId;

        public DocumentDbRepository(IDocumentDbContext dbContext, string collection)
        {
            this.dbContext = dbContext;
            this.collectionId = collection;
        }

        private DocumentCollection collection;
        private DocumentCollection Collection
        {
            get
            {
                if (collection == null)
                {
                    collection = dbContext.ReadOrCreateCollection(collectionId);
                }

                return collection;
            }
        }

        public async Task<HttpStatusCode> CreateItemAsync(T item)
        {
            var result = await dbContext.Client.CreateDocumentAsync(Collection.SelfLink, item);
            return result.StatusCode;
        }

        public async Task<T> GetItemAsync(Expression<Func<T, bool>> predicate)
        {
            var query = dbContext.Client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                    .Where(predicate)
                    .AsQueryable();

            var result = await QueryHelper.QueryAsync<T>(query);

            return result.FirstOrDefault();
        }

        public Microsoft.Azure.Documents.Document GetDocument(Guid id)
        {
            return GetDocument(id.ToString());
        }

        public async Task<HttpStatusCode> UpdateItemAsync(Guid id, T item)
        {
            return await UpdateItemAsync(id.ToString(), item);
        }

        public async Task<HttpStatusCode> DeleteItemAsync(Guid id)
        {
            return await DeleteItemAsync(id.ToString());
        }

        #region Private methods

        private async Task<HttpStatusCode> UpdateItemAsync(string id, T item)
        {
            Microsoft.Azure.Documents.Document doc = GetDocument(id);
            var result = await dbContext.Client.ReplaceDocumentAsync(doc.SelfLink, item);

            return result.StatusCode;
        }

        private async Task<HttpStatusCode> DeleteItemAsync(string id)
        {
            Microsoft.Azure.Documents.Document doc = GetDocument(id);
            var result = await dbContext.Client.DeleteDocumentAsync(doc.SelfLink);

            return result.StatusCode;
        }

        private Microsoft.Azure.Documents.Document GetDocument(string id)
        {
            return dbContext.Client.CreateDocumentQuery(Collection.DocumentsLink)
                                .Where(d => d.Id == id)
                                .AsEnumerable()
                                .FirstOrDefault();
        }

        #endregion
    }
}
