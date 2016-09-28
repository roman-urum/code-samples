using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Protocol;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// ConditionMapping.
    /// </summary>
    internal class ConditionMapping : EntityTypeConfiguration<Condition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionMapping"/> class.
        /// </summary>
        public ConditionMapping()
        {
            // Table name
            this.ToTable("Conditions");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.Value)
                .HasMaxLength(250);

            // One-to-Many /Branch - Conditions/
            this.HasOptional(t => t.Branch)
                .WithMany(a => a.Conditions)
                .HasForeignKey(t => t.BranchId)
                .WillCascadeOnDelete(true);

            // One-to-Many /Alert - Conditions/
            this.HasOptional(t => t.Alert)
                .WithMany(a => a.Conditions)
                .HasForeignKey(t => t.AlertId)
                .WillCascadeOnDelete(false);
        }
    }
}