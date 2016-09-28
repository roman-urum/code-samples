using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// VitalAlertMapping.
    /// </summary>
    internal class VitalAlertMapping : EntityTypeConfiguration<VitalAlert>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VitalAlertMapping"/> class.
        /// </summary>
        public VitalAlertMapping()
        {
            // Table name
            this.ToTable("VitalAlerts");

            this.HasRequired(va => va.Threshold)
                .WithMany(t => t.VitalAlerts)
                .HasForeignKey(va => va.ThresholdId)
                .WillCascadeOnDelete(true);

            // Relationships
            this.HasRequired(e => e.Vital)
                .WithOptional(e => e.VitalAlert)
                .WillCascadeOnDelete(true);
        }
    }
}