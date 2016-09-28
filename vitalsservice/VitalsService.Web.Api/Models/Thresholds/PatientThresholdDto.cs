using System;

namespace VitalsService.Web.Api.Models.Thresholds
{
    /// <summary>
    /// PatientThresholdDto.
    /// </summary>
    public class PatientThresholdDto : BaseThresholdDto
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
    }
}