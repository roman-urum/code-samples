using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.CustomerService
{
    /// <summary>
    /// CreateCustomerRequestDto.
    /// </summary>
    [JsonObject]
    public class CreateCustomerRequestDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the subdomain.
        /// </summary>
        /// <value>
        /// The subdomain.
        /// </value>
        public string Subdomain { get; set; }

        /// <summary>
        /// Gets or sets the logo path.
        /// </summary>
        /// <value>
        /// The logo path.
        /// </value>
        public string LogoPath { get; set; }

        /// <summary>
        /// Gets or sets the password expiration days.
        /// </summary>
        /// <value>
        /// The password expiration days.
        /// </value>
        public int PasswordExpirationDays { get; set; }

        /// <summary>
        /// Gets or sets the iddle session timeout.
        /// </summary>
        /// <value>
        /// The iddle session timeout.
        /// </value>
        public int IddleSessionTimeout { get; set; }
    }
}
