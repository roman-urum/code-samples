using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Contexts;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess
{
    /// <summary>
    /// The UnitOfWork.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;

        private bool isDisposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        public UnitOfWork(ITokenServiceDbContext dbContext)
        {
            this.dbContext = (DbContext)dbContext;
        }

        /// <summary>
        /// Creates the generic repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T> CreateGenericRepository<T>() where T : Entity
        {
            return new Repository<T>(this.dbContext);
        }

        /// <summary>
        /// Saves current state.
        /// </summary>
        public void Save()
        {
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Saves current state asynchronous.
        /// </summary>
        public async Task SaveAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }

        public void DetachEntity(object entity)
        {
            ((IObjectContextAdapter)this.dbContext).ObjectContext.Detach(entity);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.isDisposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
