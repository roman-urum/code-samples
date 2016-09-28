using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Maestro.Domain.DbEntities;

namespace Maestro.DataAccess.EF.Mappings
{
    /// <summary>
    /// CustomerUserMapping.
    /// </summary>
    internal class UserRoleMapping : EntityTypeConfiguration<UserRole>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleMapping"/> class.
        /// </summary>
        public UserRoleMapping()
        {
            // Table name
            this.ToTable("UserRoles");

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