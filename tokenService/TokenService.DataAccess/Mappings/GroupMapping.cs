using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Mappings
{
    /// <summary>
    /// Declaration of Groups table properties.
    /// </summary>
    internal class GroupMapping : EntityTypeConfiguration<Group>
    {
        public GroupMapping()
        {
            // Table name
            this.ToTable("Groups");

            // Primary key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.Name)
                .HasMaxLength(DbConstraints.MaxLength.GroupName);

            this.Property(e => e.Description)
                .HasMaxLength(DbConstraints.MaxLength.GroupDescription);

            // Many-to-Many /Principals - Groups/
            this.HasMany(g => g.Policies)
                .WithMany(p => p.Groups);
        }
    }
}