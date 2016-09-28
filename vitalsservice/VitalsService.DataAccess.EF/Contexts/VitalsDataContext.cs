using System.Data.Entity;
using System.Diagnostics;
using System.Reflection;
using Isg.EntityFramework;
using VitalsService.DataAccess.EF.Helpers;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Contexts
{
    /// <summary>
    /// VitalsDataContext.
    /// </summary>
    public class VitalsDataContext : DbContextBase, IVitalsDataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VitalsDataContext"/> class.
        /// </summary>
        public VitalsDataContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VitalsDataContext"/> class.
        /// </summary>
        /// <param name="customerContext">The customer context.</param>
        public VitalsDataContext(ICustomerContext customerContext)
            : base(ConnectionStringsHelper.GetConnectionString(customerContext.CustomerId))
        {
        }

        #region DbSets

        public DbSet<MeasurementNote> MeasurementNotes { get; set; }

        public DbSet<Vital> Vitals { get; set; }

        public DbSet<Measurement> Measurements { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<HealthSession> HealthSessions { get; set; }

        public DbSet<HealthSessionElement> HealthSessionElements { get; set; }

        public DbSet<HealthSessionElementValue> HealthSessionElementValues { get; set; }

        public DbSet<Threshold> Thresholds { get; set; }

        public DbSet<Alert> Alerts { get; set; }

        public DbSet<AlertSeverity> AlertSeverities { get; set; }

        public DbSet<VitalAlert> VitalAlerts { get; set; }

        public DbSet<HealthSessionElementAlert> HealthSessionElementAlerts { get; set; }

        public DbSet<AssessmentMedia> AssessmentMedias { get; set; }

        public DbSet<Condition> Conditions { get; set; }

        public DbSet<Tag> Tags { get; set; }

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
