using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// TagMapping.
    /// </summary>
    internal class TagMapping : EntityTypeConfiguration<Tag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagMapping"/> class.
        /// </summary>
        public TagMapping()
        {
            // Table name
            this.ToTable("Tags");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.CustomerId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CUSTOMER_ID_NAME", 0) { IsUnique = true }));

            this.Property(e => e.Name)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CUSTOMER_ID_NAME", 1) { IsUnique = true }))
                .HasMaxLength(30);

            // Many-to-Many /Elements - Tags/
            this.HasMany(t => t.Elements)
                .WithMany(a => a.Tags)
                .Map(m =>
                {
                    m.ToTable("ElementsToTags");
                    m.MapLeftKey("TagId");
                    m.MapRightKey("ElementId");
                });
        }
    }
}