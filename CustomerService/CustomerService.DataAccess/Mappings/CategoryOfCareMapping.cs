using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using CustomerService.Domain.Entities;

namespace CustomerService.DataAccess.Mappings
{
    /// <summary>
    /// CategoryOfCareMapping.
    /// </summary>
    internal class CategoryOfCareMapping : EntityTypeConfiguration<CategoryOfCare>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryOfCareMapping"/> class.
        /// </summary>
        public CategoryOfCareMapping()
        {
            // Table name
            this.ToTable("CategoriesOfCare");

            // Primary key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.Name)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NAME", 0) { IsUnique = true }))
                .HasMaxLength(100);

            // One-to-Many /Customer - CategoriesOfCare/
            this.HasRequired(c => c.Customer)
                .WithMany(c => c.CategoriesOfCare)
                .HasForeignKey(c => c.CustomerId)
                .WillCascadeOnDelete(false);

            // Many-to-Many /Sites - CategoriesOfCare/
            this.HasMany(c => c.Sites)
                .WithMany(s => s.CategoriesOfCare)
                .Map(m =>
                {
                    m.ToTable("SitesToCategoriesOfCare");
                    m.MapLeftKey("SiteId");
                    m.MapRightKey("CategoryOfCareId");
                });
        }
    }
}