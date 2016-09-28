using System;
using AutoMapper;
using Maestro.Domain.Dtos.HealthLibraryService.Elements;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;
using Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols;

namespace Maestro.Web.Models.Mappings.Converters
{
    /// <summary>
    /// ElementConverter.
    /// </summary>
    public class ElementConverter : ITypeConverter<ElementDto, ElementResponseViewModel>
    {
        /// <summary>
        /// Converts the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">context.SourceValue</exception>
        public ElementResponseViewModel Convert(ResolutionContext context)
        {
            if (context == null || context.SourceValue == null ||
                !(context.SourceValue is ElementDto))
            {
                throw new ArgumentException("context.SourceValue");
            }

            var element = (ElementDto)context.SourceValue;

            var options = context.Options.Items;
            object isBrief;

            if (options.TryGetValue("isBrief", out isBrief) && !(bool)isBrief)
            {
                if (element.Type == ElementType.Question)
                {
                    return Mapper.Map<QuestionProtocolElementResponseViewModel>(element,
                        o => o.Items.Add("isBrief", isBrief));
                }

                if (element.Type == ElementType.Measurement)
                {
                    return Mapper.Map<MeasurementProtocolElementResponseViewModel>(element,
                        o => o.Items.Add("isBrief", isBrief));
                }

                if (element.Type == ElementType.Assessment)
                {
                    return Mapper.Map<AssessmentProtocolElementResponseViewModel>(element,
                        o => o.Items.Add("isBrief", isBrief));
                }

                if (element.Type == ElementType.TextMedia)
                {
                    var result = Mapper.Map<TextMediaProtocolElementResponseViewModel>(element,
                        o => o.Items.Add("isBrief", isBrief));

                    //if (result.Media != null)
                    //{
                    //    result.Media.MediaType = result.MediaType;
                    //    result.Media.PreviewPath = MediaTypeUrlContentHelper
                    //        .GenerateContentPath(result.MediaType, result.Media.ContentPath);
                    //}

                    return result;
                }
            }

            return new ElementResponseViewModel()
            {
                Id = element.Id,
                CustomerId = element.CustomerId,
                Type = element.Type,
            };
        }
    }
}