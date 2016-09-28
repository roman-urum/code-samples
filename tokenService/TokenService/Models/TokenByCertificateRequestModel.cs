using System;
using System.ComponentModel.DataAnnotations;
using CareInnovations.HealthHarmony.Maestro.TokenService.DataAnnotations;
using CareInnovations.HealthHarmony.Maestro.TokenService.Resources;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Models
{
    /// <summary>
    /// TokenByCertificateRequestModel.
    /// </summary>
    public class TokenByCertificateRequestModel
    {
        /// <summary>
        /// Gets or sets the nonce.
        /// </summary>
        /// <value>
        /// The nonce.
        /// </value>
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [Base64String(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "Base64StringAttribute_ValidationError",
            ErrorMessage = null
        )]
        [StringLength(
            4000,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string NonceBase64 { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public Guid? PatientId { get; set; }

        /// <summary>
        /// Gets or sets the signature.
        /// </summary>
        /// <value>
        /// The signature.
        /// </value>
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [Base64String(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "Base64StringAttribute_ValidationError",
            ErrorMessage = null
        )]
        [StringLength(
            4000,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string SignatureBase64 { get; set; }
    }
}