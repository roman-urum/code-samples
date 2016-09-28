using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.Constants;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// DeviceMapping.
    /// </summary>
    internal class DeviceMapping : EntityTypeConfiguration<Device>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceMapping"/> class.
        /// </summary>
        public DeviceMapping()
        {
            // Table name
            this.ToTable("Devices");

            // Primary Key
            this.HasKey(e => e.MeasurementId);

            // Properties
            this.Property(e => e.UniqueIdentifier)
                .HasMaxLength(DbConstraints.MaxLength.DeviceUniqueIdentifier);

            this.Property(e => e.Model)
                .HasMaxLength(DbConstraints.MaxLength.DeviceModel);

            this.Property(e => e.Version)
                .HasMaxLength(30);
        }
    }
}