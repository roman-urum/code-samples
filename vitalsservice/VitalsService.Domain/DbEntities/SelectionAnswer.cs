using System;
using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Constants;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// SelectionAnswer.
    /// </summary>
    /// <seealso cref="VitalsService.Domain.DbEntities.HealthSessionElementValue" />
    /// <seealso cref="VitalsService.Domain.DbEntities.IAnalyticsEntity" />
    public class SelectionAnswer : HealthSessionElementValue, IAnalyticsEntity
    {
        /// <summary>
        /// Text of the answer element.
        /// </summary>
        [MaxLength(DbConstraints.MaxLength.SelectionAnswerText)]
        public string Text { get; set; }

        /// <summary>
        /// Guid of answer element.
        /// </summary>
        public Guid Value { get; set; }

        /// <summary>
        /// Gets or sets the internal question id.
        /// </summary>
        [MaxLength(DbConstraints.MaxLength.InternalId)]
        public string InternalId { get; set; }

        /// <summary>
        /// Gets or sets the external questino id.
        /// </summary>
        [MaxLength(DbConstraints.MaxLength.ExternalId)]
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the External score.
        /// </summary>
        /// <value>
        /// Int.
        /// </value>
        public int? ExternalScore { get; set; }

        /// <summary>
        /// Gets or sets the External score.
        /// </summary>
        /// <value>
        /// Int.
        /// </value>
        public int? InternalScore { get; set; }
    }
}