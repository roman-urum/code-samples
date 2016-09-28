using System.Data.Entity;
using System.Diagnostics;
using System.Reflection;
using Isg.EntityFramework;
using Maestro.Domain.DbEntities;

namespace Maestro.DataAccess.EF.Context
{
    /// <summary>
    /// MaestroDbContext.
    /// </summary>
    /// <seealso cref="Isg.EntityFramework.DbContextBase" />
    public class MaestroDbContext : DbContextBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaestroDbContext"/> class.
        /// </summary>
        public MaestroDbContext() :
            base("MaestroDbContext")
        {
        }

        #region DbSets

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        /// <value>
        /// The user roles.
        /// </value>
        public DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets the customer user sites.
        /// </summary>
        /// <value>
        /// The customer user sites.
        /// </value>
        public DbSet<CustomerUserSite> CustomerUserSites { get; set; }

        /// <summary>
        /// Gets or sets the customer user roles.
        /// </summary>
        /// <value>
        /// The customer user roles.
        /// </value>
        public DbSet<CustomerUserRole> CustomerUserRoles { get; set; }

        /// <summary>
        /// Gets or sets the customer users.
        /// </summary>
        /// <value>
        /// The customer users.
        /// </value>
        public DbSet<CustomerUser> CustomerUsers { get; set; }

        /// <summary>
        /// Gets or sets the customer user role to permission mappings.
        /// </summary>
        /// <value>
        /// The customer user role to permission mappings.
        /// </value>
        public DbSet<CustomerUserRoleToPermissionMapping> CustomerUserRoleToPermissionMappings { get; set; }

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
