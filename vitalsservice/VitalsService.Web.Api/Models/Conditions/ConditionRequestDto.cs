using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VitalsService.Web.Api.Models.Conditions
{
    /// <summary>
    /// ConditionDto.
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
        public virtual IList<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier;
        /// </summary>
        /// <value>
        /// The customer identifier;
        /// </value>
        public int CustomerId { get; set; }
    }
}