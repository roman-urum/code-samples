using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Program;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// ProgramMapping.
    /// </summary>
    internal class ProgramMapping : EntityTypeConfiguration<Program>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramMapping"/> class.
        /// </summary>
        public ProgramMapping()
        {
            // Table name
            this.ToTable("Programs");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.CustomerId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CUSTOMER_ID", 0) { IsUnique = false }));

            this.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Many-to-Many /Programs - Tags/
            this.HasMany(t => t.Tags)
                .WithMany(a => a.Programs)
                .Map(m =>
                {
                    m.ToTable("ProgramsToTags");
                    m.MapLeftKey("ProgramId");
                    m.MapRightKey("TagId");
                });
        }
    }
}