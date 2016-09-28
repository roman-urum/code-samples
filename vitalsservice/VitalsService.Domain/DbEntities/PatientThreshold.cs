using System;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// PatientThreshold.
    /// </summary>
    public class PatientThreshold : Threshold
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }
    }
}