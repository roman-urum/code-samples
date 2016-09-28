using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models.Converters;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// Base model class for values of health session elements.
    /// </summary>
    [JsonConverter(typeof(HealthSessionElementValueJsonConverter))]
    [KnownType(typeof(SelectionAnswerDto))]
    [KnownType(typeof(ScaleAnswerDto))]
    [KnownType(typeof(FreeFormAnswerDto))]
    [KnownType(typeof(MeasurementValueRequestDto))]
    [KnownType(typeof(MeasurementValueResponseDto))]
    [KnownType(typeof(AssessmentValueRequestDto))]
    [KnownType(typeof(AssessmentValueResponseDto))]
    public class HealthSessionElementValueDto
    {
        /// <summary>
        /// Type of answer.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public HealthSessionElementValueType Type { get; set; }
    }
}