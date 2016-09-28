using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Program;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// ProgramElementMapping.
    /// </summary>
    internal class ProgramElementMapping : EntityTypeConfiguration<ProgramElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramElementMapping"/> class.
        /// </summary>
        public ProgramElementMapping()
        {
            // Table name
            this.ToTable("ProgramElements");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // One-to-Many /Program - ProgramElements/
            this.HasRequired(t => t.Program)
                .WithMany(a => a.ProgramElements)
                .HasForeignKey(t => t.ProgramId)
                .WillCascadeOnDelete(true);

            // One-to-Many /Protocol - ProgramElements/
            this.HasRequired(t => t.Protocol)
                .WithMany(a => a.ProgramElements)
                .HasForeignKey(t => t.ProtocolId)
                .WillCascadeOnDelete(true);
        }
    }
}