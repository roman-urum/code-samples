using System;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.PatientsService
{
    /// <summary>
    /// UpdatePatientRequestDto.
    /// </summary>
    [JsonObject]
    public class UpdatePatientRequestDto : CreatePatientRequestDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the effective date.
        /// </summary>
        /// <value>
        /// The effective date.
        /// </value>
        public string EffectiveDate { get; set; }
    }
}