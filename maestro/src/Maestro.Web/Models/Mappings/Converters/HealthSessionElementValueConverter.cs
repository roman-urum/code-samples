using AutoMapper;
using Maestro.Domain.Dtos.VitalsService;
using Maestro.Web.Areas.Site.Models;

namespace Maestro.Web.Models.Mappings.Converters
{
    /// <summary>
    /// HealthSessionElementValueConverter.
    /// </summary>
    /// <seealso cref="AutoMapper.ITypeConverter{HealthSessionElementValueDto, HealthSessionElementValueViewModel}" />
    public class HealthSessionElementValueConverter : ITypeConverter<HealthSessionElementValueDto, HealthSessionElementValueViewModel>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="context">Resolution context</param>
        /// <returns>
        /// Destination object
        /// </returns>
        public HealthSessionElementValueViewModel Convert(ResolutionContext context)
        {
            var measurementHealthSessionElementValue = context.SourceValue as MeasurementValueResponseDto;

            if (measurementHealthSessionElementValue != null)
            {
                return Mapper.Map<MeasurementValueResponseDto, MeasurementHealthSessionElementValueViewModel>(measurementHealthSessionElementValue);
            }

            var selectionAnswerHealthSessionElementValue = context.SourceValue as SelectionAnswerDto;

            if (selectionAnswerHealthSessionElementValue != null)
            {
                return Mapper.Map<SelectionAnswerDto, SelectionAnswerHealthSessionElementValueViewModel>(selectionAnswerHealthSessionElementValue);
            }

            var scaleAnswerHealthSessionElementValue = context.SourceValue as ScaleAndFreeFormAnswerDto;

            if (scaleAnswerHealthSessionElementValue != null)
            {
                return Mapper.Map<ScaleAndFreeFormAnswerDto, ScaleAndFreeFormAnswerHealthSessionElementValueViewModel>(scaleAnswerHealthSessionElementValue);
            }

            var assessmentHealthSessionElementValue = context.SourceValue as AssessmentValueResponseDto;

            if (assessmentHealthSessionElementValue != null)
            {
                return Mapper.Map<AssessmentValueResponseDto, AssessmentHealthSessionElementValueViewModel>(assessmentHealthSessionElementValue);
            }

            return null;
        }
    }
}