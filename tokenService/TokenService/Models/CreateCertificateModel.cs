using System;
using System.ComponentModel.DataAnnotations;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Models
{
    /// <summary>
    /// Model to send certificate data.
    /// </summary>
    public class CreateCertificateModel
    {
        private const int MinCustomerId = 1;

        /// <summary>
        /// Id of patient who uses device with certificate.
        /// </summary>
        [Required]
        public Guid PatientId { get; set; }

        /// <summary>
        /// Id of patient customer.
        /// </summary>
        [Required]
        [Range(MinCustomerId, int.MaxValue)]
        public int CustomerId { get; set; }

        /// <summary>
        /// Certificate thumbprint.
        /// </summary>
        [Required]
        public string Thumbprint { get; set; }

        /// <summary>
        /// Base64 with certificate raw data.
        /// </summary>
        [Required]
        public string Certificate { get; set; }
    }
}