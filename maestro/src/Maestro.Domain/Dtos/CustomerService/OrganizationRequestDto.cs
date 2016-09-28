using System;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.CustomerService
{
    /// <summary>
    /// OrganizationRequestDto.
    /// </summary>
    [JsonObject]
    public class OrganizationRequestDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent organization identifier.
        /// </summary>
        /// <value>
        /// The parent organization identifier.
        /// </value>
        public Guid? ParentOrganizationId { get; set; }
    }
}