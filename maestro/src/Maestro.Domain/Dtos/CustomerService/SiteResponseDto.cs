using System;
using System.Collections.Generic;

namespace Maestro.Domain.Dtos.CustomerService
{
    /// <summary>
    /// SiteResponseDto.
    /// </summary>
    public class SiteResponseDto : SiteRequestDto
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
        /// Gets or sets the categories of care.
        /// </summary>
        /// <value>
        /// The categories of care.
        /// </value>
        public List<CategoryOfCareDto> CategoriesOfCare { get; set; }
    }
}