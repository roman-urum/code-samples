using System;

namespace Maestro.Domain.Dtos.CustomerService
{
    /// <summary>
    /// OrganizationResponseDto.
    /// </summary>
    public class OrganizationResponseDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is archived.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is archived; otherwise, <c>false</c>.
        /// </value>
        public bool IsArchived { get; set; }
        
        /// <summary>
        /// Gets or sets the parent organization identifier.
        /// </summary>
        /// <value>
        /// The parent organization identifier.
        /// </value>
        public Guid? ParentOrganizationId { get; set; }
    }
}