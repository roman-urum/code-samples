using System;

namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// Model for assessment values of health session elements.
    /// </summary>
    public class AssessmentValueRequestDto : HealthSessionElementValueDto
    {
        /// <summary>
        /// Id of related assessment media.
        /// </summary>
        public Guid Value { get; set; }
    }
}