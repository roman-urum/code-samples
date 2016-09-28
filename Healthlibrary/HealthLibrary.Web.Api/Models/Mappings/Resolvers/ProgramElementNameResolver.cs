using AutoMapper;
using HealthLibrary.Domain.Entities.Program;
using HealthLibrary.Web.Api.Extensions;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Mappings.Resolvers
{
    /// <summary>
    /// Initializes collection of ProgramElementResponseDto using ProgramElement entity.
    /// Includes additional info about protocol name.
    /// </summary>
    public class ProgramElementNameResolver : ValueResolver<ProgramElement, LocalizedStringResponseDto>
    {
        protected override LocalizedStringResponseDto ResolveCore(ProgramElement source)
        {
            var localization = source.Protocol.NameLocalizedStrings.GetForRequestedLanguage();

            return localization == null
                ? null
                : Mapper.Map<LocalizedStringResponseDto>(localization);
        }
    }
}