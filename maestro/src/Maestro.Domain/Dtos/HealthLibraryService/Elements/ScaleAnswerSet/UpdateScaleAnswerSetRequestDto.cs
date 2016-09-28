using System;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.ScaleAnswerSet
{
    /// <summary>
    /// ScaleAnswerSetUpdateDto.
    /// </summary>
    [JsonObject]
    public class UpdateScaleAnswerSetRequestDto : BaseScaleAnswerSetDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        public UpdateScaleAnswerSetLabelsRequestDto Labels { get; set; }
    }
}