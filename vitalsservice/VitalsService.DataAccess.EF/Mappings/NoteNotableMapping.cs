using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.Constants;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// NoteNotableMapping.
    /// </summary>
    internal class NoteNotableMapping : EntityTypeConfiguration<NoteNotable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteNotableMapping"/> class.
        /// </summary>
        public NoteNotableMapping()
        {
            // Table name
            this.ToTable("NoteNotables");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(DbConstraints.MaxLength.NotableName);

            // Relationships
            // One-to-Many /Note - NoteNotables/
            this.HasRequired(nn => nn.Note)
                .WithMany(n => n.Notables)
                .HasForeignKey(nn => nn.NoteId)
                .WillCascadeOnDelete(true);
        }
    }
}