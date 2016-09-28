using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Maestro.Domain.DbEntities;

namespace Maestro.DataAccess.EF.Mappings
{
    /// <summary>
    /// Class DisplayThresholdSettingMapping.
    /// </summary>
    public class DisplayThresholdSettingMapping:EntityTypeConfiguration<DisplayThresholdSetting>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayThresholdSettingMapping"/> class.
        /// </summary>
        public DisplayThresholdSettingMapping()
        {
            this.ToTable("DisplayThresholdsSettings");

            this.HasKey(d => d.Id);
            this.Property(d => d.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(d => d.ThresholdId)
                .IsRequired();

            this.HasRequired(d => d.VitalChartSetting)
                .WithMany(v => v.DisplayThresholds)
                .HasForeignKey(d => d.VitalChartSettingId)
                .WillCascadeOnDelete(true);
        }
    }
}
