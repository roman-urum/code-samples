using System;
using AutoMapper;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Web.Api.Models.Elements;
using HealthLibrary.Web.Api.Models.Elements.ScaleAnswerSets;
using HealthLibrary.Web.Api.Models.Elements.SelectionAnswerSets;

namespace HealthLibrary.Web.Api.Models.Mappings.Converters
{
    /// <summary>
    /// AnswerSetConverter.
    /// </summary>
    public class AnswerSetConverter : ITypeConverter<AnswerSet, OpenEndedAnswerSetResponseDto>
    {
        /// <summary>
        /// Converts the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public OpenEndedAnswerSetResponseDto Convert(ResolutionContext context)
        {
            if (context == null || context.SourceValue == null || !(context.SourceValue is AnswerSet))
            {
                throw new ArgumentException("context.SourceValue");
            }

            var answerSet = (AnswerSet)context.SourceValue;

            var options = context.Options.Items;
            object isBrief;

            if (options.TryGetValue("isBrief", out isBrief) && !(bool)isBrief)
            {
                if (answerSet is ScaleAnswerSet)
                {
                    return Mapper.Map<ScaleAnswerSetResponseDto>(answerSet);
                }

                if (answerSet is SelectionAnswerSet)
                {
                    return Mapper.Map<SelectionAnswerSetResponseDto>(answerSet);
                }
            }

            return new OpenEndedAnswerSetResponseDto()
            {
                Id = answerSet.Id,
                CustomerId = answerSet.CustomerId,
                Type = answerSet.Type,
                Name = answerSet.Name
            };
        }
    }
}