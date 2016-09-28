using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// HealthSessionElementAlertMapping.
    /// </summary>
    internal class HealthSessionElementAlertMapping : EntityTypeConfiguration<HealthSessionElementAlert>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthSessionElementAlertMapping"/> class.
        /// </summary>
        public HealthSessionElementAlertMapping()
        {
            // Table name
            this.ToTable("HealthSessionElementAlerts");

            // Relationships
            this.HasRequired(e => e.HealthSessionElement)
                .WithOptional(e => e.HealthSessionElementAlert)
                .WillCascadeOnDelete(true);
        }
    }
}