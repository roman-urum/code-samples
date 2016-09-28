using System;
using System.Runtime.Serialization;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.AssessmentElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.MeasurementElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.QuestionElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.TextMediaElements;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements
{
    /// <summary>
    /// ElementDto.
    /// </summary>
    [KnownType(typeof(ElementDto))]
    [KnownType(typeof(MeasurementResponseDto))]
    [KnownType(typeof(AssessmentResponseDto))]
    [KnownType(typeof(QuestionElementResponseDto))]
    [KnownType(typeof(TextMediaElementResponseDto))]
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