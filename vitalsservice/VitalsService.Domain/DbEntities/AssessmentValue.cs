using System;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// Entity of assessment values in healsh session element.
    /// </summary>
    public class AssessmentValue : HealthSessionElementValue
    {
        /// <summary>
        /// Guid of assessment media entity.
        /// </summary>
        public Guid AssessmentMediaId { get; set; }

        /// <summary>
        /// Reference to AssessmentMedia.
        /// </summary>
        public virtual AssessmentMedia AssessmentMedia { get; set; }
    }
}