using System;
using Newtonsoft.Json;

namespace VitalsService.Domain.DocumentDb
{
    /// <summary>
    /// RawMeasurment.
    /// </summary>
    public class RawMeasurement
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the observed.
        /// </summary>
        /// <value>
        /// The observed.
        /// </value>
        public DateTime Observed { get; set; }

        /// <summary>
        /// Gets or sets the raw json.
        /// </summary>
        /// <value>
        /// The raw json.
        /// </value>
        public object RawJson { get; set; }
    }
}
