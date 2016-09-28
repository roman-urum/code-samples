using System;
using Newtonsoft.Json;

namespace DeviceService.Web.Api.Models.Dtos.RequestsResponses
{
    /// <summary>
    /// ActivationResponseDto.
    /// </summary>
    public class ActivationResponseDto
    {
        /// <summary>
        /// [unique device identifier]
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        /// <summary>
        /// [customer identifier]
        /// </summary>
        /// <value>
        /// The cucstomer identifier.
        /// </value>
        [JsonProperty(PropertyName = "customerId")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Id of patient who uses device.
        /// </summary>
        public Guid PatientId { get; set; }

        /// <summary>
        /// [base64-encoded client certificate]
        /// </summary>
        /// <value>
        /// The certificate.
        /// </value>
        [JsonProperty(PropertyName = "certificate")]
        public string Certificate { get; set; }
    }
}