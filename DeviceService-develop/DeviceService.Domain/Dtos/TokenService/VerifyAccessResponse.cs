using Newtonsoft.Json;

namespace DeviceService.Domain.Dtos.TokenService
{
    /// <summary>
    /// VerifyAccessResponse.
    /// </summary>
    [JsonObject]
    public class VerifyAccessResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="VerifyAccessResponse"/> is allowed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if allowed; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "allowed")]
        public bool Allowed { get; set; }

        /// <summary>
        /// Gets or sets the TTL.
        /// </summary>
        /// <value>
        /// The TTL.
        /// </value>
        [JsonProperty(PropertyName = "ttl")]
        public double Ttl { get; set; }
    }
}