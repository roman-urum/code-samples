using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Program;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// RecurrenceMapping.
    /// </summary>
    internal class RecurrenceMapping : EntityTypeConfiguration<Recurrence>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecurrenceMapping"/> class.
        /// </summary>
        public RecurrenceMapping()
        {
            // Table name
            this.ToTable("Recurrences");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // One-to-Many /Program - Recurrences/
            this.HasRequired(t => t.Program)
                .WithMany(a => a.Recurrences)
                .HasForeignKey(t => t.ProgramId)
                .WillCascadeOnDelete(true);
        }
    }
}