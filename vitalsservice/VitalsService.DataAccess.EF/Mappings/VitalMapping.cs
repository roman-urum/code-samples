using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.Constants;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// VitalMapping.
    /// </summary>
    internal class VitalMapping : EntityTypeConfiguration<Vital>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VitalMapping" /> class.
        /// </summary>
        public VitalMapping()
        {
            // Table name
            this.ToTable("Vitals");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.Name)
                .HasMaxLength(DbConstraints.MaxLength.VitalName);

            this.Property(e => e.Unit)
                .HasMaxLength(DbConstraints.MaxLength.Unit);

            // One-to-Many /Measurement - Vitals/
            this.HasRequired(v => v.Measurement)
                .WithMany(m => m.Vitals)
                .HasForeignKey(v => v.MeasurementId)
                .WillCascadeOnDelete(true);
        }
    }
}