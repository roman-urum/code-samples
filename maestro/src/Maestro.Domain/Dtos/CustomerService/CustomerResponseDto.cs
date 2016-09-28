using System.Collections.Generic;

namespace Maestro.Domain.Dtos.CustomerService
{
    /// <summary>
    /// CustomerDto.
    /// </summary>
    public class CustomerResponseDto
    {
        /// <summary>
        /// Customer id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Password expiration in days
        /// </summary>
        public int PasswordExpirationDays { get; set; }

        /// <summary>
        /// Session timout in minutes
        /// </summary>
        public int IddleSessionTimeout { get; set; }

        /// <summary>
        /// Id of person who created a customer.
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Subdomain
        /// </summary>
        public string Subdomain { get; set; }

        /// <summary>
        /// Gets or sets LogoPath
        /// </summary>
        public string LogoPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is published.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is published; otherwise, <c>false</c>.
        /// </value>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the sites.
        /// </summary>
        /// <value>
        /// The sites.
        /// </value>
        public List<SiteResponseDto> Sites { get; set; }

        /// <summary>
        /// Gets or sets the organizations.
        /// </summary>
        /// <value>
        /// The organizations.
        /// </value>
        public List<OrganizationResponseDto> Organizations { get; set; }

        /// <summary>
        /// Gets or sets the categories of care.
        /// </summary>
        /// <value>
        /// The categories of care.
        /// </value>
        public List<CategoryOfCareDto> CategoriesOfCare { get; set; }
    }
}
