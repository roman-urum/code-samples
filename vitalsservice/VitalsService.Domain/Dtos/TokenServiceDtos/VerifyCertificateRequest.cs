using System;
using VitalsService.ApiClient;

namespace VitalsService.Domain.Dtos.TokenServiceDtos
{
    /// <summary>
    /// VerifyCertificateRequest.
    /// </summary>
    public class VerifyCertificateRequest
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the thumbprint.
        /// </summary>
        /// <value>
        /// The thumbprint.
        /// </value>
        [RequestParameter(RequestParameterType.UrlSegment)]
        public string Thumbprint { get; set; }
    }
}