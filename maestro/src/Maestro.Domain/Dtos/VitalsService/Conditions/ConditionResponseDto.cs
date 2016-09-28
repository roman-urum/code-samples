using System;
using System.Collections.Generic;

namespace Maestro.Domain.Dtos.VitalsService.Conditions
{
    /// <summary>
    /// ConditionResponseDto.
    /// </summary>
    public class ConditionResponseDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier;
        /// </summary>
        /// <value>
        /// The customer identifier;
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
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public virtual List<string> Tags { get; set; }
    }
}