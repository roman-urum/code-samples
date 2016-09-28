using System.Linq;
using AutoMapper;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Mappings.Resolvers
{
    /// <summary>
    /// TextMediaElementStringResolver.
    /// </summary>
    public class TextMediaElementStringResolver : ValueResolver<TextMediaElement, LocalizedStringWithAudioFileMediaResponseDto>
    {
        /// <summary>
        /// Resolves the core.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        protected override LocalizedStringWithAudioFileMediaResponseDto ResolveCore(TextMediaElement source)
        {
            if (!string.IsNullOrEmpty(CareElementRequestContext.Current.Language))
            {
                var particularLocalization = 
                    source
                    .TextLocalizedStrings
                    .SingleOrDefault(s => s.Language == CareElementRequestContext.Current.Language);

                if (particularLocalization != null)
                {
                    return Mapper.Map<TextMediaElementString, LocalizedStringWithAudioFileMediaResponseDto>(particularLocalization);
                }
            }
            else if (!string.IsNullOrEmpty(CareElementRequestContext.Current.DefaultLanguage))
            {
                var defaultLocalization = 
                    source
                    .TextLocalizedStrings
                    .SingleOrDefault(s => s.Language == CareElementRequestContext.Current.DefaultLanguage);

                if (defaultLocalization != null)
                {
                    return Mapper.Map<TextMediaElementString, LocalizedStringWithAudioFileMediaResponseDto>(defaultLocalization);
                }
            }

            return null;
        }
    }
}