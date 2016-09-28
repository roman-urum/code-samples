using System.Data.Entity.ModelConfiguration;
using DeviceService.Domain.Entities;

namespace DeviceService.DataAccess.Mappings
{
    internal class DeviceSettingsMapping : EntityTypeConfiguration<DeviceSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceSettingsMapping"/> class.
        /// </summary>
        public DeviceSettingsMapping()
        {
            this.Property(d => d.BloodGlucosePeripheralString)
                .HasColumnName("Settings_BloodGlucosePeripheral")
                .HasMaxLength(DbConstraints.MaxLength.BloodGlucosePeripheral);

            this.Ignore(d => d.BloodGlucosePeripheral);

            this.Property(d => d.iHealthAccount)
                .HasMaxLength(DbConstraints.MaxLength.iHealthAccount);
        }
    }
}
