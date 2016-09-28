using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using CustomerService.Domain.Entities;

namespace CustomerService.DataAccess.Mappings
{
    /// <summary>
    /// CustomerMapping.
    /// </summary>
    internal class CustomerMapping : EntityTypeConfiguration<Customer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerMapping"/> class.
        /// </summary>
        public CustomerMapping()
        {
            // Table name
            this.ToTable("Customers");

            // Primary key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(e => e.Name)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NAME", 0) { IsUnique = true }))
                .HasMaxLength(250);

            this.Property(e => e.Subdomain)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_SUBDOMAIN", 1) { IsUnique = true }))
                .HasMaxLength(63);

            this.Property(e => e.LogoPath)
                .HasMaxLength(1022);
        }
    }
}