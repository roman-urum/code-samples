using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.ScaleAnswerSet;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerSet;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements
{
    /// <summary>
    /// OpenEndedAnswerSetResponseDto.
    /// </summary>
    [KnownType(typeof(OpenEndedAnswerSetResponseDto))]
    [KnownType(typeof(ScaleAnswerSetResponseDto))]
    [KnownType(typeof(SelectionAnswerSetResponseDto))]
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
        public List<string> Tags { get; set; }
    }
}