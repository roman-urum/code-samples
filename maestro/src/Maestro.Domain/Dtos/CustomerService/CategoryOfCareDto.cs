using System;

namespace Maestro.Domain.Dtos.CustomerService
{
    /// <summary>
    /// CategoryOfCareDto.
    /// </summary>
    public class CategoryOfCareDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}