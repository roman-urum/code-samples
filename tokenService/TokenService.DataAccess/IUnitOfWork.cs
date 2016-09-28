using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess
{
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

        void DetachEntity(object entity);
    }
}
