using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Maestro.Domain.DbEntities;

namespace Maestro.DataAccess.EF.Mappings
{
    internal class ChartSettingMapping: EntityTypeConfiguration<ChartSetting>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartSettingMapping"/> class.
        /// </summary>
        public ChartSettingMapping()
        {
            // Table name
            this.ToTable("ChartsSettings");

            this.HasKey(c => c.Id);
            this.Property(c => c.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(c => c.Order)
                .IsRequired();

            this.Property(c => c.Type)
                .IsRequired();

            this.HasRequired(c => c.TrendSetting)
                .WithMany(c => c.ChartsSettings)
                .HasForeignKey(c => c.TrendSettingId)
                .WillCascadeOnDelete(true);

        }
    }
}
