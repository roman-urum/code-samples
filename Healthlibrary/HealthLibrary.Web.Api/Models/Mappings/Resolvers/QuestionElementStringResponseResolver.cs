using System.Linq;
using AutoMapper;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Web.Api.Extensions;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Mappings.Resolvers
{
    /// <summary>
    /// Resolver to map specific language to selection answer choice response.
    /// </summary>
    public class QuestionElementStringResponseResolver : ValueResolver<QuestionElement, LocalizedStringWithAudioFileMediaResponseDto>
    {
        /// <summary>
        /// Resolves the core.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        protected override LocalizedStringWithAudioFileMediaResponseDto ResolveCore(QuestionElement source)
        {
            var localization = source.LocalizedStrings.GetForRequestedLanguage();

            return localization == null
                ? null
                : Mapper.Map<QuestionElementString, LocalizedStringWithAudioFileMediaResponseDto>(localization);
        }
    }
}