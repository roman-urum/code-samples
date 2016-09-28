using System.ComponentModel.DataAnnotations;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// Model for element value presented as measurement.
    /// </summary>
    public class MeasurementValueRequestDto : HealthSessionElementValueDto
    {
        /// <summary>
        /// Measurement data.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public MeasurementRequestDto Value { get; set; }
    }
}