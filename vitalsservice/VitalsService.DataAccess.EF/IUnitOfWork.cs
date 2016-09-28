using System.Threading.Tasks;
using VitalsService.DataAccess.EF.Repositories;

namespace VitalsService.DataAccess.EF
{
    /// <summary>
    /// Provides methods to access vitals service repositories.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Creates the generic repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> CreateRepository<T>() where T : class, new();

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