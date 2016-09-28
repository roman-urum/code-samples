using System;

using VitalsService.Domain.Enums;

namespace VitalsService.Web.Api.Models.Thresholds
{
    /// <summary>
    /// DefaultThresholdDto.
    /// </summary>
    public class DefaultThresholdDto : BaseThresholdDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the default type.
        /// </summary>
        /// <value>
        /// The default type.
        /// </value>
        public ThresholdDefaultType DefaultType { get; set; }

        /// <summary>
        /// Gets or sets the condition identifier.
        /// </summary>
        /// <value>
        /// The condition identifier.
        /// </value>
        public Guid? ConditionId { get; set; }
    }
}