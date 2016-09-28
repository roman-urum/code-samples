using System;

namespace VitalsService.Domain.Dtos
{
    /// <summary>
    /// Additional search criteria to filter assessment media.
    /// </summary>
    public class AssessmentMediaSearchDto : BaseSearchDto
    {
        /// <summary>
        /// Exclude all assessment media created after specified value from response.
        /// </summary>
        public DateTime? CreatedBefore { get; set; }

        /// <summary>
        /// Exclude all assessment media created before specified value from response.
        /// </summary>
        public DateTime? CreatedAfter { get; set; }
    }
}
