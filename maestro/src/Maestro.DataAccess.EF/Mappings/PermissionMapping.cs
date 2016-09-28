using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Maestro.Domain.DbEntities;

namespace Maestro.DataAccess.EF.Mappings
{
    /// <summary>
    /// PermissionMapping.
    /// </summary>
    internal class PermissionMapping : EntityTypeConfiguration<CustomerUserRoleToPermissionMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionMapping"/> class.
        /// </summary>
        public PermissionMapping()
        {
            // Table name
            this.ToTable("CustomerUserRolesPermissionsMappings");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // One-to-Many /CustomerUserRole - CustomerUserRoleToPermissionMapping/
            this.HasRequired(p => p.CustomerUserRole)
                .WithMany(cr => cr.Permissions)
                .HasForeignKey(p => p.CustomerUserRoleId)
                .WillCascadeOnDelete(true);
        }
    }
}