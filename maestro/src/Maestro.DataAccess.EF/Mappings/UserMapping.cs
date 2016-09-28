using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Maestro.Domain.Constants;
using Maestro.Domain.DbEntities;

namespace Maestro.DataAccess.EF.Mappings
{
    /// <summary>
    /// UserMapping.
    /// </summary>
    internal class UserMapping : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMapping"/> class.
        /// </summary>
        public UserMapping()
        {
            // Table name
            this.ToTable("Users");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.FirstName)
                .HasMaxLength(50);

            this.Property(e => e.LastName)
                .HasMaxLength(50);

            this.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Email", 0) { IsUnique = true }));

            this.Property(e => e.TokenServiceUserId)
                .HasMaxLength(100);

            this.Property(e => e.Phone)
                .HasMaxLength(DbConstraints.MaxLenght.Phone);

            // Relationships
            // One-to-Many /UserRole - Users/
            this.HasRequired(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .WillCascadeOnDelete(true);
        }
    }
}