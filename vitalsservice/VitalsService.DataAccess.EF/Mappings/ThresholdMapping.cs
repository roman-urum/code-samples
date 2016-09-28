using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.Constants;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// ThresholdMapping.
    /// </summary>
    internal class ThresholdMapping : EntityTypeConfiguration<Threshold>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThresholdMapping" /> class.
        /// </summary>
        public ThresholdMapping()
        {
            // Table name
            this.ToTable("Thresholds");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.CustomerId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CUSTOMER_ID", 0) { IsUnique = false }));

            this.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(DbConstraints.MaxLength.VitalName);

            this.Property(e => e.Unit)
                .IsRequired()
                .HasMaxLength(DbConstraints.MaxLength.Unit);

            // One-to-Many /AlertSeverity - Thresholds/
            this.HasOptional(t => t.AlertSeverity)
                .WithMany(a => a.Thresholds)
                .HasForeignKey(t => t.AlertSeverityId)
                .WillCascadeOnDelete(true);
        }
    }
}