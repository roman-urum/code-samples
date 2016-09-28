using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Program;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// ProgramDayElementMapping.
    /// </summary>
    internal class ProgramDayElementMapping : EntityTypeConfiguration<ProgramDayElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramElementMapping"/> class.
        /// </summary>
        public ProgramDayElementMapping()
        {
            // Table name
            this.ToTable("ProgramDayElements");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Many-to-Many /ProgramDayElements - ProgramElements/
            this.HasRequired(t => t.ProgramElement)
                .WithMany(a => a.ProgramDayElements)
                .WillCascadeOnDelete(true);

            // One-to-Many /Program - ProgramDayElements/
            this.HasRequired(t => t.Program)
                .WithMany(a => a.ProgramDayElements)
                .HasForeignKey(t => t.ProgramId)
                .WillCascadeOnDelete(false);

            // One-to-Many /Recurrence - ProgramDayElements/
            this.HasOptional(t => t.Recurrence)
                .WithMany(a => a.ProgramDayElements)
                .HasForeignKey(t => t.RecurrenceId)
                .WillCascadeOnDelete(false);
        }
    }
}