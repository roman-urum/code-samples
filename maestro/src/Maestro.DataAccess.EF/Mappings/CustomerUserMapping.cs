using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Maestro.Domain.DbEntities;

namespace Maestro.DataAccess.EF.Mappings
{
    /// <summary>
    /// CustomerUserMapping.
    /// </summary>
    internal class CustomerUserMapping : EntityTypeConfiguration<CustomerUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUserMapping"/> class.
        /// </summary>
        public CustomerUserMapping()
        {
            // Table name
            this.ToTable("CustomerUsers");

            // Primary Key
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

            this.Property(e => e.CustomerUserId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CustomerUserId", 1) { IsUnique = false }))
                .HasMaxLength(100);

            // One-to-Many /CustomerUserRole - CustomerUsers/
            this.HasRequired(cu => cu.CustomerUserRole)
                .WithMany(cur => cur.CustomerUsers)
                .HasForeignKey(cu => cu.CustomerUserRoleId)
                .WillCascadeOnDelete(true);
        }
    }
}