using System;
using DeviceService.Domain.Entities.Enums;

namespace DeviceService.Domain.Entities
{
    /// <summary>
    /// Activation.
    /// </summary>
    public class Activation
    {
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
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        public string DeviceId { get; set; }

        /// <summary>
        /// Certificate thumbprint.
        /// </summary>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Certificate in base-64 string.
        /// </summary>
        public string Certificate { get; set; }

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
        /// Gets or sets the device model.
        /// </summary>
        /// <value>
        /// The device model.
        /// </value>
        public string DeviceModel { get; set; }
    }
}
