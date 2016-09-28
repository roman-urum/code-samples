using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// DefaultThresholdMapping.
    /// </summary>
    internal class DefaultThresholdMapping : EntityTypeConfiguration<DefaultThreshold>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultThresholdMapping" /> class.
        /// </summary>
        public DefaultThresholdMapping()
        {
            // Table name
            this.ToTable("DefaultThresholds");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.DefaultType)
                .IsRequired()
                .HasMaxLength(20);

            // Relationships
            // Zero-Or-One-to-Many /Conditions - DefaultThreshold/
            this.HasOptional(dt => dt.Condition)
                .WithMany(c => c.DefaultThresholds)
                .HasForeignKey(dt => dt.ConditionId)
                .WillCascadeOnDelete(true);
        }
    }
}