using System.Data.Entity;
using System.Diagnostics;
using System.Reflection;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Program;
using HealthLibrary.Domain.Entities.Protocol;
using Isg.EntityFramework;

namespace HealthLibrary.DataAccess.Contexts
{
    /// <summary>
    /// PatientServiceDbContext.
    /// </summary>
    public class HealthLibraryServiceDbContext : DbContextBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthLibraryServiceDbContext"/> class.
        /// </summary>
        public HealthLibraryServiceDbContext():base("HealthLibraryService_Shared")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthLibraryServiceDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public HealthLibraryServiceDbContext(string connectionString)
            : base(connectionString)
        {
        }

        #region DbSets

        public DbSet<Element> Elements { get; set; }

        public DbSet<MeasurementElement> MeasurementElements { get; set; }

        public DbSet<AssessmentElement> AssessmentElements { get; set; }

        public DbSet<Protocol> Protocols { get; set; }

        public DbSet<Program> Programs { get; set; }

        public DbSet<AnswerSet> AnswerSets { get; set; }

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
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

#if DEBUG
            LogGeneratedSql();
#endif
            base.OnModelCreating(modelBuilder);
        }

        private void LogGeneratedSql()
        {
            Database.Log = log => Debug.Write(log);
        }
    }
}