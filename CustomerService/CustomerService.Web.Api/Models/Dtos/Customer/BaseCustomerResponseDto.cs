namespace CustomerService.Web.Api.Models.Dtos.Customer
{
    /// <summary>
    /// BaseCustomerResponseDto.
    /// </summary>
    public abstract class BaseCustomerResponseDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
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
        /// Gets or sets the password expiration days (days).
        /// </summary>
        /// <value>
        /// The password expiration days.
        /// </value>
        public int PasswordExpirationDays { get; set; }

        /// <summary>
        /// Gets or sets the iddle session timeout (minutes).
        /// </summary>
        /// <value>
        /// The iddle session timeout.
        /// </value>
        public int IddleSessionTimeout { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is archived.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is archived; otherwise, <c>false</c>.
        /// </value>
        public bool IsArchived { get; set; }
    }
}