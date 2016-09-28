using System;
using System.Collections.Generic;
using HealthLibrary.Domain.Entities.Enums;

namespace HealthLibrary.Web.Api.Models.Elements
{
    /// <summary>
    /// OpenEndedAnswerSetResponseDto.
    /// </summary>
    public class OpenEndedAnswerSetResponseDto
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
        public AnswerSetType Type { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Answer set tags.
        /// </summary>
        public IList<string> Tags { get; set; }
    }
}