using System;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace VitalsService.DataAccess.Document.Repository
{
    public interface IDocumentDbRepository<T>
    {
        Task<HttpStatusCode> CreateItemAsync(T item);

        Task<T> GetItemAsync(Expression<Func<T, bool>> predicate);

        Microsoft.Azure.Documents.Document GetDocument(Guid id);

        Task<HttpStatusCode> UpdateItemAsync(Guid id, T item);

        Task<HttpStatusCode> DeleteItemAsync(Guid id);
    }
}
