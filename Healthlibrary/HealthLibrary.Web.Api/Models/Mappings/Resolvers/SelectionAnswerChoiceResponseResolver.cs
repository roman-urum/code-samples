using System.Linq;
using AutoMapper;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Mappings.Resolvers
{
    /// <summary>
    /// Resolver to map specific language to selection answer choice response.
    /// </summary>
    public class SelectionAnswerChoiceResponseResolver :
        ValueResolver<SelectionAnswerChoice, LocalizedStringWithAudioFileMediaResponseDto>
    {
        /// <summary>
        /// Resolves the core.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        protected override LocalizedStringWithAudioFileMediaResponseDto ResolveCore(SelectionAnswerChoice source)
        {
            if (!string.IsNullOrEmpty(CareElementRequestContext.Current.Language))
            {
                var particularLocalization = 
                    source
                    .LocalizedStrings
                    .SingleOrDefault(s => s.Language == CareElementRequestContext.Current.Language);

                if (particularLocalization != null)
                {
                    return Mapper.Map<SelectionAnswerChoiceString, LocalizedStringWithAudioFileMediaResponseDto>(particularLocalization);
                }
            }

            if (!string.IsNullOrEmpty(CareElementRequestContext.Current.DefaultLanguage))
            {
                var defaultLocalization = 
                    source
                    .LocalizedStrings
                    .SingleOrDefault(s => s.Language == CareElementRequestContext.Current.DefaultLanguage);

                if (defaultLocalization != null)
                {
                    return Mapper.Map<SelectionAnswerChoiceString, LocalizedStringWithAudioFileMediaResponseDto>(defaultLocalization);
                }
            }

            return null;
        }
    }
}