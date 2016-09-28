using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// Db configuration for measurement value.
    /// </summary>
    internal class MeasurementValueMapping : EntityTypeConfiguration<MeasurementValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementValueMapping"/> class.
        /// </summary>
        public MeasurementValueMapping()
        {
            // Table name
            this.ToTable("MeasurementValues");

            // Primary key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.HasRequired(e => e.Measurement)
                .WithMany(m => m.MeasurementValues)
                .WillCascadeOnDelete(true);
        }
    }
}