using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// MeasurementElementMapping.
    /// </summary>
    internal class MeasurementElementMapping : EntityTypeConfiguration<MeasurementElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementElementMapping"/> class.
        /// </summary>
        public MeasurementElementMapping()
        {
            // Table name
            this.ToTable("MeasurementElements");

            // Properties
            this.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}