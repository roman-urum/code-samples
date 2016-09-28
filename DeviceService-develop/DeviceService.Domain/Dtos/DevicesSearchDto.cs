using System;
using DeviceService.Domain.Entities.Enums;

namespace DeviceService.Domain.Dtos
{
    /// <summary>
    /// DevicesSearchDto.
    /// </summary>
    public class DevicesSearchDto : BaseSearchDto
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid? PatientId { get; set; }

        /// <summary>
        /// Allows to filter devices before specified connection date.
        /// </summary>
        public DateTime? LastConnectedUtcBefore { get; set; }

        /// <summary>
        /// Allows to filter devices after specified connection date.
        /// </summary>
        public DateTime? LastConnectedUtcAfter { get; set; }

        /// <summary>
        /// Allows to filter devices by type.
        /// </summary>
        public DeviceType? Type { get; set; }

        /// <summary>
        /// Allows to filter devices by status.
        /// </summary>
        public Status? Status { get; set; }
    }
}