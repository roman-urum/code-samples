using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Mappings
{
    /// <summary>
    /// Declaration of Policies table properties.
    /// </summary>
    public class PolicyMapping : EntityTypeConfiguration<Policy>
    {
        public PolicyMapping()
        {
            // Table name
            this.ToTable("Policies");

            // Properties
            this.Property(e => e.Service)
                .HasMaxLength(DbConstraints.MaxLength.ServiceName);

            this.Property(e => e.Name)
                .HasMaxLength(DbConstraints.MaxLength.PolicyName);

            this.Property(e => e.Controller)
                .HasMaxLength(DbConstraints.MaxLength.ControllerName);

            // Primary key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
