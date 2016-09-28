using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Constants;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// ScaleAnswer.
    /// </summary>
    /// <seealso cref="VitalsService.Domain.DbEntities.HealthSessionElementValue" />
    /// <seealso cref="VitalsService.Domain.DbEntities.IAnalyticsEntity" />
    public class ScaleAnswer : HealthSessionElementValue, IAnalyticsEntity
    {
        /// <summary>
        /// Value of scale answer (from answers range)
        /// </summary>
        public int Value { get; set; }

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
