using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Maestro.Domain.DbEntities;

namespace Maestro.DataAccess.EF.Mappings
{
    /// <summary>
    /// Class VitalChartSettingMapping.
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Maestro.Domain.DbEntities.VitalChartSetting}" />
    public class VitalChartSettingMapping : EntityTypeConfiguration<VitalChartSetting>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VitalChartSettingMapping"/> class.
        /// </summary>
        public VitalChartSettingMapping()
        {
            this.ToTable("VitalChartsSettings");

            this.HasKey(v => v.Id);
            this.Property(v => v.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(v => v.ShowAverage)
                .IsRequired();

            this.Property(v => v.ShowMax)
                .IsRequired();

            this.Property(v => v.ShowMin)
                .IsRequired();

            this.Property(v => v.VitalName)
                .IsRequired();

        }
    }
}
