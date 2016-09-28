using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Maestro.Common.Extensions;
using Maestro.Domain.Dtos.VitalsService;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Web.Areas.Site.Models;
using Maestro.Web.Areas.Site.Models.Patients;

namespace Maestro.Web.Models.Mappings.Converters
{
    /// <summary>
    /// QuestionReadingConverter.
    /// </summary>
    public class QuestionReadingConverter : ITypeConverter<HealthSessionElementDto, QuestionReadingViewModel>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="context">Resolution context</param>
        /// <returns>
        /// Destination object
        /// </returns>
        public QuestionReadingViewModel Convert(ResolutionContext context)
        {
            var healthSessionElement = context.SourceValue as HealthSessionElementDto;

            if (healthSessionElement == null)
            {
                return null;
            }

            if (healthSessionElement.Type != HealthSessionElementType.Question)
            {
                return null;
            }

            var values = new List<string>();

            if (healthSessionElement.Values.Any(v => v.Type == HealthSessionElementValueType.SelectionAnswer))
            {
                values = healthSessionElement.Values.Select(v => ((SelectionAnswerDto)v).Text).ToList();
            }

            if (healthSessionElement.Values.Any(v => v.Type == HealthSessionElementValueType.ScaleAnswer))
            {
                values = healthSessionElement.Values.Select(v => ((ScaleAndFreeFormAnswerDto)v).Value).ToList();
            }

            if (healthSessionElement.Values.Any(v => v.Type == HealthSessionElementValueType.OpenEndedAnswer))
            {
                values = healthSessionElement.Values.Select(v => ((ScaleAndFreeFormAnswerDto)v).Value).ToList();
            }

            return new QuestionReadingViewModel()
            {
                Id = healthSessionElement.Id,
                ElementId = healthSessionElement.ElementId,
                Date = healthSessionElement.AnsweredUtc.HasValue ?
                    healthSessionElement.AnsweredUtc.Value.ConvertTimeFromUtc(healthSessionElement.AnsweredTz) :
                    (DateTimeOffset?)null,
                Alert = healthSessionElement.HealthSessionElementAlert != null ?
                    Mapper.Map<AlertViewModel>(healthSessionElement.HealthSessionElementAlert) :
                    null,
                QuestionText = healthSessionElement.Text,
                AnswerText = values.DefaultIfEmpty(string.Empty).Aggregate((s1, s2) => string.Format("{0}, {1}", s1, s2))
            };
        }
    }
}