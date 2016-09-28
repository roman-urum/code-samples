using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Protocol;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// BranchMapping.
    /// </summary>
    internal class BranchMapping : EntityTypeConfiguration<Branch>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BranchMapping"/> class.
        /// </summary>
        public BranchMapping()
        {
            // Table name
            this.ToTable("Branches");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.ThresholdAlertSeverityId)
                .IsOptional();

            // One-to-Many /ProtocolElement - Branches/
            this.HasRequired(t => t.ProtocolElement)
                .WithMany(a => a.Branches)
                .HasForeignKey(t => t.ProtocolElementId)
                .WillCascadeOnDelete(true);

            // One-to-Many /ProtocolElement - Branches/
            this.HasOptional(t => t.NextProtocolElement)
                .WithMany()
                .HasForeignKey(t => t.NextProtocolElementId)
                .WillCascadeOnDelete(false);
        }
    }
}