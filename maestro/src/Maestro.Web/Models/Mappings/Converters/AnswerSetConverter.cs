using System;
using AutoMapper;
using Maestro.Domain.Dtos.HealthLibraryService.Elements;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;
using Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols;

namespace Maestro.Web.Models.Mappings.Converters
{
    public class AnswerSetConverter : ITypeConverter<OpenEndedAnswerSetResponseDto, AnswerSetElementResponseViewModel>
    {
        public AnswerSetElementResponseViewModel Convert(ResolutionContext context)
        {
            if (context == null || context.SourceValue == null || !(context.SourceValue is OpenEndedAnswerSetResponseDto))
            {
                throw new ArgumentException("context.SourceValue");
            }

            var answerSet = (OpenEndedAnswerSetResponseDto)context.SourceValue;

            var options = context.Options.Items;
            object isBrief;

            if (options.TryGetValue("isBrief", out isBrief) && !(bool)isBrief)
            {

                if (answerSet.Type == AnswerSetType.Scale)
                {
                    return Mapper.Map<ScaleAnswerSetElementResponseViewModel>(answerSet);
                }
                if (answerSet.Type == AnswerSetType.Selection)
                {
                    return Mapper.Map<SelectionAnswerSetElementResponseViewModel>(answerSet);
                }

            }

            return new AnswerSetElementResponseViewModel()
            {
                Id = answerSet.Id,
                CustomerId = answerSet.CustomerId,
                Type = answerSet.Type,
                Name = answerSet.Name
            };
        }
    }
}