using System;
using AutoMapper;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Web.Api.Models.Elements;
using HealthLibrary.Web.Api.Models.Elements.AssessmentElements;
using HealthLibrary.Web.Api.Models.Elements.MeasurementElements;
using HealthLibrary.Web.Api.Models.Elements.QuestionElements;
using HealthLibrary.Web.Api.Models.Elements.TextMediaElements;

namespace HealthLibrary.Web.Api.Models.Mappings.Converters
{
    /// <summary>
    /// ElementConverter.
    /// </summary>
    public class ElementConverter : ITypeConverter<Element, ElementDto>
    {
        /// <summary>
        /// Converts the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public ElementDto Convert(ResolutionContext context)
        {
            if (context == null || context.SourceValue == null || !(context.SourceValue is Element))
            {
                throw new ArgumentException("context.SourceValue");
            }

            var element = (Element)context.SourceValue;

            var options = context.Options.Items;
            object isBrief;

            if (options.TryGetValue("isBrief", out isBrief) && !(bool)isBrief)
            {
                if (element is QuestionElement)
                {
                    return Mapper.Map<QuestionElementResponseDto>(element, o => o.Items.Add("isBrief", isBrief));
                }

                if (element is TextMediaElement)
                {
                    return Mapper.Map<TextMediaElementResponseDto>(element, o => o.Items.Add("isBrief", isBrief));
                }

                if (element is MeasurementElement)
                {
                    return Mapper.Map<MeasurementResponseDto>(element, o => o.Items.Add("isBrief", isBrief));
                }

                if (element is AssessmentElement)
                {
                    return Mapper.Map<AssessmentResponseDto>(element, o => o.Items.Add("isBrief", isBrief));
                }
            }

            return new ElementDto()
            {
                Id = element.Id,
                CustomerId = element.CustomerId,
                Type = element.Type
            };
        }
    }
}