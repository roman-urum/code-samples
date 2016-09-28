using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.Constants;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// MeasurementNoteMapping.
    /// </summary>
    internal class MeasurementNoteMapping : EntityTypeConfiguration<MeasurementNote>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementNoteMapping" /> class.
        /// </summary>
        public MeasurementNoteMapping()
        {
            // Table name
            this.ToTable("MeasurementNotes");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.Title)
                .HasMaxLength(DbConstraints.MaxLength.NoteTitle);

            this.Property(e => e.Text)
                .HasMaxLength(DbConstraints.MaxLength.NoteText);

            // One-to-Many /Measurement - Notes/
            this.HasRequired(n => n.Measurement)
                .WithMany(m => m.MeasurementNotes)
                .HasForeignKey(n => n.MeasurementId)
                .WillCascadeOnDelete(true);
        }
    }
}