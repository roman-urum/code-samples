using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.TokenService.RequestsResponses
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
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="VerifyAccessResponse"/> is allowed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if allowed; otherwise, <c>false</c>.
        /// </value>
        public bool Allowed { get; set; }

        /// <summary>
        /// Gets or sets the TTL.
        /// </summary>
        /// <value>
        /// The TTL.
        /// </value>
        public double Ttl { get; set; }
    }
}