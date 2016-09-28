using System;
using System.Collections.Generic;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models.HealthSessions;

namespace VitalsService.Web.Api.Models.Mappings.Resolvers
{
    /// <summary>
    /// Selects appropriate model to map HealthSessionElementValue entity.
    /// </summary>
    public class HealthSessionElementValueModelResolver : ValueResolver<HealthSessionElement, IEnumerable<HealthSessionElementValueDto>>
    {
        protected override IEnumerable<HealthSessionElementValueDto> ResolveCore(HealthSessionElement source)
        {
            var result = new List<HealthSessionElementValueDto>();

            foreach (var elementValue in source.Values)
            {
                switch (elementValue.Type)
                {
                    case HealthSessionElementValueType.ScaleAnswer:
                        result.Add(Mapper.Map<ScaleAnswerDto>(elementValue));
                        break;

                    case HealthSessionElementValueType.SelectionAnswer:
                        result.Add(Mapper.Map<SelectionAnswerDto>(elementValue));
                        break;

                    case HealthSessionElementValueType.OpenEndedAnswer:
                        result.Add(Mapper.Map<FreeFormAnswerDto>(elementValue));
                        break;

                    case HealthSessionElementValueType.MeasurementAnswer:
                        result.Add(Mapper.Map<MeasurementValueResponseDto>(elementValue));
                        break;

                    case HealthSessionElementValueType.StethoscopeAnswer:
                        result.Add(Mapper.Map<AssessmentValueResponseDto>(elementValue));
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }

            return result;
        }
    }
}