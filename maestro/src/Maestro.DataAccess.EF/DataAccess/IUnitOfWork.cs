using System.Threading.Tasks;

namespace Maestro.DataAccess.EF.DataAccess
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Creates the generic repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> CreateGenericRepository<T>() where T : class, new();

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
