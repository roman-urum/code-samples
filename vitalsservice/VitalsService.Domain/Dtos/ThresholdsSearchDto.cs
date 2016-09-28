using System;
using System.Collections.Generic;
using VitalsService.Domain.Enums;

namespace VitalsService.Domain.Dtos
{
    /// <summary>
    /// ThresholdsSearchDto.
    /// </summary>
    public class ThresholdsSearchDto: BaseSearchDto
    {
        /// <summary>
        /// Gets or sets the default type.
        /// </summary>
        /// <value>
        /// The default type.
        /// </value>
        public ThresholdSearchType? Mode { get; set; }

        /// <summary>
        /// Gets or sets the list of condition identifiers.
        /// </summary>
        /// <value>
        /// The list of condition identifiers.
        /// </value>
        public IList<Guid> ConditionIds { get; set; }
    }
}