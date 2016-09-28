using System.Data.Entity;
using System.Diagnostics;
using System.Reflection;
using CustomerService.Domain.Entities;

namespace CustomerService.DataAccess.Context
{
    using Isg.EntityFramework;

    /// <summary>
    /// CustomerServiceDbContextю
    /// </summary>
    public class CustomerServiceDbContext : DbContextBase, ICustomerServiceDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerServiceDbContext"/> class.
        /// </summary>
        public CustomerServiceDbContext()
            : base("CustomerServiceDbContext")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        #region DbSets

        /// <summary>
        /// Gets or sets the customers.
        /// </summary>
        /// <value>
        /// The customers.
        /// </value>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Gets or sets the sites.
        /// </summary>
        /// <value>
        /// The sites.
        /// </value>
        public DbSet<Site> Sites { get; set; }

        #endregion

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Set relations.
#if DEBUG
            LogGeneratedSql();
#endif
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        private void LogGeneratedSql()
        {
            Database.Log = log => Debug.Write(log);
        }
    }
}