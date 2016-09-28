using Maestro.Domain.Dtos.VitalsService.Enums;
using System.Runtime.Serialization;

namespace Maestro.Domain.Dtos.VitalsService
{
    /// <summary>
    /// HealthSessionElementValueDto.
    /// </summary>
    [KnownType(typeof(SelectionAnswerDto))]
    [KnownType(typeof(MeasurementValueResponseDto))]
    [KnownType(typeof(ScaleAndFreeFormAnswerDto))]
    [KnownType(typeof(AssessmentValueResponseDto))]
    public class HealthSessionElementValueDto
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public HealthSessionElementValueType Type { get; set; }
    }
}