using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Protocol;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// AlertMapping.
    /// </summary>
    internal class AlertMapping : EntityTypeConfiguration<Alert>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlertMapping"/> class.
        /// </summary>
        public AlertMapping()
        {
            // Table name
            this.ToTable("Alerts");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.AlertSeverityId)
                .IsOptional();

            // One-to-Many /ProtocolElement - Alerts/
            this.HasRequired(t => t.ProtocolElement)
                .WithMany(a => a.Alerts)
                .HasForeignKey(t => t.ProtocolElementId)
                .WillCascadeOnDelete(true);
        }
    }
}