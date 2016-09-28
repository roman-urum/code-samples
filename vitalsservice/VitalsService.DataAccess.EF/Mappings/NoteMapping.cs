using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.Constants;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// NoteMapping.
    /// </summary>
    internal class NoteMapping : EntityTypeConfiguration<Note>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteMapping"/> class.
        /// </summary>
        public NoteMapping()
        {
            // Table name
            this.ToTable("Notes");

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

            this.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(DbConstraints.MaxLength.NoteText);

            this.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(DbConstraints.MaxLength.CreatedBy);

            // Relationships
            // Zero-Or-One-to-Many /Vital - Notes/
            this.HasOptional(n => n.Measurement)
                .WithMany(m => m.Notes)
                .HasForeignKey(n => n.MeasurementId)
                .WillCascadeOnDelete(true);

            // Zero-Or-One-to-Many /HealthSessionElement - Notes/
            this.HasOptional(n => n.HealthSessionElement)
                .WithMany(n => n.Notes)
                .HasForeignKey(n => n.HealthSessionElementId)
                .WillCascadeOnDelete(true);
        }
    }
}