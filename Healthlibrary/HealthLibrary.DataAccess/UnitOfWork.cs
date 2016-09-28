using System;
using System.Configuration;
using System.Data.Entity;
using System.Threading.Tasks;
using HealthLibrary.Common;
using HealthLibrary.DataAccess.Contexts;
using HealthLibrary.Domain;

namespace HealthLibrary.DataAccess
{
    /// <summary>
    /// The UnitOfWork.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private bool isDisposed = false;
        private readonly DbContext dbContext;
        private readonly ICareElementContext customerContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="customerContext">The customer context.</param>
        public UnitOfWork(ICareElementContext customerContext)
        {
            this.customerContext = customerContext;
            this.dbContext = 
                new HealthLibraryServiceDbContext(
                    GetConnectionString(customerContext.CustomerId));
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

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        private string GetConnectionString(int customerId)
        {
            var connectionStrings =
                ConfigurationManager.ConnectionStrings["HealthLibraryService_" + customerId] ??
                ConfigurationManager.ConnectionStrings["HealthLibraryService_Shared"];

            return connectionStrings.ConnectionString;
        }
    }
}
