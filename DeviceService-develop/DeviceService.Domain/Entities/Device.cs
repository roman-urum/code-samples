using System;
using DeviceService.Domain.Entities.Enums;

namespace DeviceService.Domain.Entities
{
    /// <summary>
    /// Device.
    /// </summary>
    public class Device : Entity, ISoftDelitable
    {
        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        public string DeviceId { get; set; }

        /// <summary>
        /// String representation of device type.
        /// </summary>
        /// <value>
        /// The type of the device.
        /// </value>
        public string DeviceTypeString
        {
            get
            {
                return DeviceType.ToString();
            }
            private set
            {
                DeviceType result;

                DeviceType = Enum.TryParse(value, true, out result) ? result : DeviceType.Other;
            }
        }

        /// <summary>
        /// Type of patient device.
        /// </summary>
        public DeviceType DeviceType { get; set; }

        /// <summary>
        /// Gets or sets the type of the device identifier.
        /// </summary>
        /// <value>
        /// The type of the device identifier.
        /// </value>
        public DeviceIdType? DeviceIdType { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the activation code.
        /// </summary>
        /// <value>
        /// The activation code.
        /// </value>
        public string ActivationCode { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the device model.
        /// </summary>
        /// <value>
        /// The device model.
        /// </value>
        public string DeviceModel { get; set; }

        /// <summary>
        /// Gets or sets the device timezone.
        /// </summary>
        /// <value>
        /// The device timezone.
        /// </value>
        public string DeviceTz { get; set; }

        /// <summary>
        /// Indicating whether entity deleted or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Device certificate in base-64 string.
        /// </summary>
        public string Certificate { get; set; }

        /// <summary>
        /// Certificate thumbprint.
        /// </summary>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Date when device connected last time.
        /// </summary>
        public DateTime? LastConnectedUtc { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public DeviceSettings Settings { get; set; }
    }
}