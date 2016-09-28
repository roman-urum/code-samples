using System;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models.HealthSessions;

namespace VitalsService.Web.Api.Models.Converters
{
    /// <summary>
    /// Converts element value model to appropriate entity.
    /// </summary>
    public class HealthSessionElementValueTypeConverter :
        ITypeConverter<HealthSessionElementValueDto, HealthSessionElementValue>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="context">Resolution context</param>
        /// <returns>
        /// Destination object
        /// </returns>
        /// <exception cref="NotSupportedException">Mapping is not supported for specified element type.</exception>
        public HealthSessionElementValue Convert(ResolutionContext context)
        {
            var source = context.SourceValue as HealthSessionElementValueDto;

            if (source == null)
            {
                return null;
            }

            switch (source.Type)
            {
                case HealthSessionElementValueType.SelectionAnswer:
                    return Mapper.Map<SelectionAnswer>(source);

                case HealthSessionElementValueType.ScaleAnswer:
                    return Mapper.Map<ScaleAnswer>(source);

                case HealthSessionElementValueType.OpenEndedAnswer:
                    return Mapper.Map<FreeFormAnswer>(source);

                case HealthSessionElementValueType.StethoscopeAnswer:
                    return Mapper.Map<AssessmentValue>(source);

                default:
                    throw new NotSupportedException("Mapping is not supported for specified element type.");
            }
        }
    }
}