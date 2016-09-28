using System;
using System.Collections.Generic;
using Maestro.Common.ApiClient;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Domain.Dtos.VitalsService.Thresholds
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
        [RequestParameter(RequestParameterType.QueryString)]
        public ThresholdDefaultType? DefaultType { get; set; }

        /// <summary>
        /// Gets or sets the alert severity identifier
        /// </summary>
        /// <value>
        /// The alert severity identifier
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public Guid? AlertSeverityId { get; set; }

        /// <summary>
        /// Gets or sets the list of condition identifiers.
        /// </summary>
        /// <value>
        /// The list of condition identifiers.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public IList<Guid> ConditionIds { get; set; }
    }
}