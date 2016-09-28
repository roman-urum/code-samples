using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// HealthSessionElementMapping.
    /// </summary>
    internal class HealthSessionElementMapping : EntityTypeConfiguration<HealthSessionElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthSessionElementMapping"/> class.
        /// </summary>
        public HealthSessionElementMapping()
        {
            // Table name
            this.ToTable("HealthSessionElements");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.AnsweredTz)
                .HasMaxLength(44);

            // One-to-Many /Measurement - Vitals/
            this.HasRequired(v => v.HealthSession)
                .WithMany(m => m.Elements)
                .HasForeignKey(v => v.HealthSessionId)
                .WillCascadeOnDelete(true);
        }
    }
}