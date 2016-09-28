using System;
using System.Collections.Generic;
using VitalsService.Domain.Enums;

namespace VitalsService.Domain.Dtos
{
    /// <summary>
    /// DefaultThresholdsSearchDto.
    /// </summary>
    public class DefaultThresholdsSearchDto : BaseSearchDto
    {
        /// <summary>
        /// Gets or sets the default type.
        /// </summary>
        /// <value>
        /// The default type.
        /// </value>
        public ThresholdDefaultType? DefaultType { get; set; }

        /// <summary>
        /// Gets or sets the alert severity identifier
        /// </summary>
        /// <value>
        /// The alert severity identifier
        /// </value>
        public Guid? AlertSeverityId { get; set; }

        /// <summary>
        /// Gets or sets the list of condition identifiers.
        /// </summary>
        /// <value>
        /// The list of condition identifiers.
        /// </value>
        public IList<Guid> ConditionIds { get; set; }
    }
}