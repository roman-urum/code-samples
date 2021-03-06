﻿namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// Includes internal and external analytics fields.
    /// </summary>
    public interface IAnalyticsEntity : IBaseAnalyticsEntity
    {
        /// <summary>
        /// Gets or sets the External score.
        /// </summary>
        /// <value>
        /// Int.
        /// </value>
        int? ExternalScore { get; set; }

        /// <summary>
        /// Gets or sets the External score.
        /// </summary>
        /// <value>
        /// Int.
        /// </value>
        int? InternalScore { get; set; }
    }
}
