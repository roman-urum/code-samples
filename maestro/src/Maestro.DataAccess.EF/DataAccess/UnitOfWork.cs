using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Maestro.Domain;

namespace Maestro.DataAccess.EF.DataAccess
{
    /// <summary>
    /// The UnitOfWork.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private bool isDisposed = false;

        private readonly DbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(DbContext context)
        {
            this.dbContext = context;
        }

        /// <summary>
        /// Creates the generic repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T> CreateGenericRepository<T>() where T : class, new()
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
            await dbContext.SaveChangesAsync();
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
