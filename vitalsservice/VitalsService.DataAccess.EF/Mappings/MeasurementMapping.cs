using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// MeasurementMapping.
    /// </summary>
    internal class MeasurementMapping : EntityTypeConfiguration<Measurement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementMapping"/> class.
        /// </summary>
        public MeasurementMapping()
        {
            // Table name
            this.ToTable("Measurements");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.CustomerId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CUSTOMER_ID", 0) { IsUnique = false }));

            this.Property(e => e.PatientId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_PATIENT_ID", 0) { IsUnique = false }));

            this.Property(e => e.ClientId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CLIENT_ID", 0) { IsUnique = false }));

            this.Property(e => e.ObservedTz)
                .HasMaxLength(44);

            this.Property(e => e.ClientId)
                .HasMaxLength(50);

            // Relationships
            // One-to-One /Measurement - Device/
            this.HasRequired(m => m.Device)
                .WithRequiredPrincipal(d => d.Measurement)
                .WillCascadeOnDelete(true);
        }
    }
}