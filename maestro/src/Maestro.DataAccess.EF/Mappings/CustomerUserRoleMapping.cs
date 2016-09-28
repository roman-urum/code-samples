using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Maestro.Domain.DbEntities;

namespace Maestro.DataAccess.EF.Mappings
{
    /// <summary>
    /// CustomerUserRoleMapping.
    /// </summary>
    internal class CustomerUserRoleMapping : EntityTypeConfiguration<CustomerUserRole>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUserRoleMapping"/> class.
        /// </summary>
        public CustomerUserRoleMapping()
        {
            // Table name
            this.ToTable("CustomerUserRoles");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}