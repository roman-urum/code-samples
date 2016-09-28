using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Mappings
{
    /// <summary>
    /// Declaration of Credentials table properties.
    /// </summary>
    internal class CredentialMapping : EntityTypeConfiguration<Credential>
    {
        public CredentialMapping()
        {
            // Table name
            this.ToTable("Credentials");

            // Primary key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(p => p.Value)
                .HasMaxLength(DbConstraints.MaxLength.CredentialValue);

            // One-to-Many /Principals - Credentials/
            this.HasRequired(c => c.Principal)
                .WithMany(p => p.Credentials)
                .HasForeignKey(c => c.PrincipalId)
                .WillCascadeOnDelete(true);
        }
    }
}