using VitalsService.DataAccess.Document.Repository;

namespace VitalsService.DataAccess.Document
{
    /// <summary>
    /// Factory to create instances of required repositories.
    /// </summary>
    public interface IDocumentRepositoryFactory
    {
        /// <summary>
        /// Creates and returs repository for specified collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        IDocumentDbRepository<T> Create<T>(VitalsServiceCollection collection);
    }
}
