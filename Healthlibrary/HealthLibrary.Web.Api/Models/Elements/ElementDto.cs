using System;
using HealthLibrary.Domain.Entities.Enums;

namespace HealthLibrary.Web.Api.Models.Elements
{
    /// <summary>
    /// ElementDto.
    /// </summary>
    public class ElementDto
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
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ElementType Type { get; set; }
    }
}