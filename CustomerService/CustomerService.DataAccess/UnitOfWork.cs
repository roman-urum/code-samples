using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerService.DataAccess.Context;
using Microsoft.Practices.ServiceLocation;

namespace CustomerService.DataAccess
{
    /// <summary>
    /// The UnitOfWork.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private bool isDisposed = false;

        private readonly DbContext dbContext =
            (DbContext)ServiceLocator.Current.GetInstance<ICustomerServiceDbContext>();

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
