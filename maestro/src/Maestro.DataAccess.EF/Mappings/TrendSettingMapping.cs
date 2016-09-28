using System.Data.Entity.ModelConfiguration;
using Maestro.Domain.DbEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maestro.DataAccess.EF.Mappings
{
    /// <summary>
    /// Class TrendSettingMapping.
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Maestro.Domain.DbEntities.TrendSetting}" />
    public class TrendSettingMapping : EntityTypeConfiguration<TrendSetting>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrendSettingMapping"/> class.
        /// </summary>
        public TrendSettingMapping()
        {
            // Table name
            this.ToTable("TrendsSettings");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StartDate)
                .IsOptional();

            this.Property(t => t.EndDate)
                .IsOptional();

            this.Property(t => t.LastDays)
                .IsOptional();

            this.Property(t => t.PatientId)
                .IsRequired();



        }
    }
}
