using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Mappings
{
    /// <summary>
    /// Declaration of Principals table properties.
    /// </summary>
    internal class PrincipalMapping : EntityTypeConfiguration<Principal>
    {
        public PrincipalMapping()
        {
            // Table name
            this.ToTable("Principals");

            // Primary key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(DbConstraints.MaxLength.Username)
                .HasColumnAnnotation("UsernameIndex",
                    new IndexAnnotation(new IndexAttribute("IX_Username") {IsUnique = true}));

            this.Property(e => e.FirstName)
                .HasMaxLength(DbConstraints.MaxLength.FirstName);

            this.Property(e => e.LastName)
                .HasMaxLength(DbConstraints.MaxLength.LastName);

            this.Property(p => p.Description)
                .HasMaxLength(DbConstraints.MaxLength.PrincipalDescription);

            // Many-to-Many /Principals - Groups/
            this.HasMany(p => p.Groups)
                .WithMany(g => g.Principals);

            // Many-to-Many /Principals - Policies/
            this.HasMany(p => p.Policies)
                .WithMany(p => p.Principals);
        }
    }
}
