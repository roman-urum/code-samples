using System;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities
{
    /// <summary>
    /// DeviceCertificate.
    /// </summary>
    /// <seealso cref="CareInnovations.HealthHarmony.Maestro.TokenService.Domain.AggregateRoot" />
    public class DeviceCertificate : AggregateRoot
    {
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
        /// Gets or sets the certificate.
        /// </summary>
        /// <value>
        /// The certificate.
        /// </value>
        public string Certificate { get; set; }

        /// <summary>
        /// Gets or sets the thumbprint.
        /// </summary>
        /// <value>
        /// The thumbprint.
        /// </value>
        public string Thumbprint { get; set; }
    }
}