using System;
using Maestro.Domain.Enums;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.DevicesService
{
    /// <summary>
    /// DeviceDto.
    /// </summary>
    [JsonObject]
    public class DeviceDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

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
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the type of the device.
        /// </summary>
        /// <value>
        /// The type of the device.
        /// </value>
        public string DeviceType { get; set; }

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

        /// <summary>
        /// Gets or sets the device tz.
        /// </summary>
        /// <value>
        /// The device tz.
        /// </value>
        public string DeviceTz { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public DeviceStatus Status { get; set; }

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
        public string BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the last connected UTC.
        /// </summary>
        /// <value>
        /// The last connected UTC.
        /// </value>
        public DateTime? LastConnectedUtc { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public DeviceSettingsDto Settings { get; set; }
    }
}