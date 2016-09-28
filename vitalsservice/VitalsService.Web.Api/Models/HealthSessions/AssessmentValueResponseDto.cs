using VitalsService.Web.Api.Models.AssessmentMedias;

namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// Model for response with assessment value.
    /// </summary>
    public class AssessmentValueResponseDto : HealthSessionElementValueDto
    {
        /// <summary>
        /// Model of assigned assessment media.
        /// </summary>
        public AssessmentMediaResponseDto AssessmentMedia { get; set; }
    }
}