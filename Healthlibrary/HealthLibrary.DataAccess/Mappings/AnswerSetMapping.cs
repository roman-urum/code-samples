using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// AnswerSetMapping.
    /// </summary>
    internal class AnswerSetMapping : EntityTypeConfiguration<AnswerSet>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerSetMapping"/> class.
        /// </summary>
        public AnswerSetMapping()
        {
            // Table name
            this.ToTable("AnswerSets");

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

            // Many-to-Many /AnswerSets - Tags/
            this.HasMany(t => t.Tags)
                .WithMany(a => a.AnswerSets)
                .Map(m =>
                {
                    m.ToTable("AnswerSetsToTags");
                    m.MapLeftKey("AnswerSetId");
                    m.MapRightKey("TagId");
                });
        }
    }
}