using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Maestro.Domain.DbEntities;

namespace Maestro.DataAccess.EF.Mappings
{
    /// <summary>
    /// Class QuestionChartSettingMapping.
    /// </summary>
    public class QuestionChartSettingMapping:EntityTypeConfiguration<QuestionChartSetting>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionChartSettingMapping"/> class.
        /// </summary>
        public QuestionChartSettingMapping()
        {
            this.ToTable("QuestionChartsSettings");

            this.HasKey(q => q.Id);
            this.Property(q => q.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(q => q.QuestionId)
                .IsRequired();
        }
    }
}
