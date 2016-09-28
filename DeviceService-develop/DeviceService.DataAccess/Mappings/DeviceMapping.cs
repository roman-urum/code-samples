using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DeviceService.Domain.Entities;

namespace DeviceService.DataAccess.Mappings
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
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.CustomerId)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CUSTOMER_ID", 0) { IsUnique = false }));

            this.Property(e => e.DeviceId)
                .HasMaxLength(100);

            this.Property(e => e.ActivationCode)
                .HasMaxLength(10);

            this.Property(e => e.DeviceModel)
                .HasMaxLength(100);

            this.Property(e => e.DeviceTz)
                .HasMaxLength(44);

            this.Property(d => d.DeviceTypeString)
                .HasColumnName("DeviceType")
                .HasMaxLength(20)
                .IsRequired();

            this.Ignore(d => d.DeviceType);
        }
    }
}