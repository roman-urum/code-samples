using System.Threading.Tasks;
using HealthLibrary.Domain;

namespace HealthLibrary.DataAccess
{
    /// <summary>
    /// IUnitOfWork.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Creates the generic repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> CreateGenericRepository<T>() where T : Entity;

        /// <summary>
        /// Saves current state.
        /// </summary>
        void Save();

        /// <summary>
        /// Saves current state asynchronous.
        /// </summary>
        Task SaveAsync();
    }
}