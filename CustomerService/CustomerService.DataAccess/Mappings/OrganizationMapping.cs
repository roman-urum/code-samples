using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using CustomerService.Domain.Entities;

namespace CustomerService.DataAccess.Mappings
{
    /// <summary>
    /// OrganizationMapping.
    /// </summary>
    internal class OrganizationMapping : EntityTypeConfiguration<Organization>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationMapping"/> class.
        /// </summary>
        public OrganizationMapping()
        {
            // Table name
            this.ToTable("Organizations");

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

            // One-to-Many /Customer - Organizations/
            this.HasRequired(o => o.Customer)
                .WithMany(c => c.Organizations)
                .HasForeignKey(o => o.CustomerId)
                .WillCascadeOnDelete(true);

            // One-to-Many /Customer - Organizations/
            this.HasOptional(o => o.ParentOrganization)
                .WithMany(o => o.ChildOrganizations)
                .HasForeignKey(o => o.ParentOrganizationId)
                .WillCascadeOnDelete(false);
        }
    }
}