using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// TagsMapping.
    /// </summary>
    public class TagsMapping: EntityTypeConfiguration<Tag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagsMapping"/> class.
        /// </summary>
        public TagsMapping()
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
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CUSTOMER_ID", 0) { IsUnique = false }));

            this.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);            

            // Many-to-Many /Conditions - Tags/
            this.HasMany(t => t.Conditions)
                .WithMany(c => c.Tags)
                .Map(m =>
                {
                    m.ToTable("ConditionsToTags");
                    m.MapLeftKey("ConditionId");
                    m.MapRightKey("TagId");
                });
        }
    }
}