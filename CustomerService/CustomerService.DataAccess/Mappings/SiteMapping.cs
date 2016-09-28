using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using CustomerService.Domain.Entities;

namespace CustomerService.DataAccess.Mappings
{
    /// <summary>
    /// SiteMapping.
    /// </summary>
    internal class SiteMapping : EntityTypeConfiguration<Site>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteMapping"/> class.
        /// </summary>
        public SiteMapping()
        {
            // Table name
            this.ToTable("Sites");

            // Primary key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.State)
                .HasMaxLength(100);

            this.Property(e => e.City)
                .HasMaxLength(50);

            this.Property(e => e.Address1)
                .HasMaxLength(250);

            this.Property(e => e.Address2)
                .HasMaxLength(250);

            this.Property(e => e.Address3)
                .HasMaxLength(250);

            this.Property(e => e.ZipCode)
                .HasMaxLength(10);

            this.Property(e => e.NationalProviderIdentificator)
                .HasColumnName("NPI")
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NPI", 0) { IsUnique = false }))
                .HasMaxLength(100);

            this.Property(e => e.CustomerSiteId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CustomerSiteId", 1) { IsUnique = false }))
                .HasMaxLength(100);

            this.Property(e => e.ContactPhone)
                .HasMaxLength(20);

            this.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            // One-to-Many /Customer - Sites/
            this.HasRequired(s => s.Customer)
                .WithMany(c => c.Sites)
                .HasForeignKey(s => s.CustomerId)
                .WillCascadeOnDelete(true);

            // One-to-Many /Organization - Sites/
            this.HasOptional(s => s.ParentOrganization)
                .WithMany(o => o.Sites)
                .HasForeignKey(s => s.ParentOrganizationId)
                .WillCascadeOnDelete(false);
        }
    }
}