using System.Collections.Generic;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.ScaleAnswerSet
{
    /// <summary>
    /// BaseScaleAnswerSetDto.
    /// </summary>
    public class BaseScaleAnswerSetDto
    {
        /// <summary>
        /// Gets or sets the low value.
        /// </summary>
        /// <value>
        /// The low value.
        /// </value>
        public int LowValue { get; set; }
        /// <summary>
        /// Gets or sets the high value.
        /// </summary>
        /// <value>
        /// The high value.
        /// </value>
        public int HighValue { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Answer set tags.
        /// </summary>
        public List<string> Tags { get; set; }
    }
}