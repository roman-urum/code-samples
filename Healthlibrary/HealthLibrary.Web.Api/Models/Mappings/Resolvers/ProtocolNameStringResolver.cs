using System.Linq;
using AutoMapper;
using HealthLibrary.Domain.Entities.Protocol;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Mappings.Resolvers
{
    /// <summary>
    /// TextMediaElementStringResolver.
    /// </summary>
    public class ProtocolNameStringResolver : ValueResolver<Protocol, LocalizedStringResponseDto>
    {
        /// <summary>
        /// Resolves the core.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        protected override LocalizedStringResponseDto ResolveCore(Protocol source)
        {
            if (!string.IsNullOrEmpty(CareElementRequestContext.Current.Language))
            {
                var particularLocalization = 
                    source
                    .NameLocalizedStrings
                    .SingleOrDefault(s => s.Language == CareElementRequestContext.Current.Language);

                if (particularLocalization != null)
                {
                    return Mapper.Map<ProtocolString, LocalizedStringResponseDto>(particularLocalization);
                }
            }

            if (!string.IsNullOrEmpty(CareElementRequestContext.Current.DefaultLanguage))
            {
                var defaultLocalization = 
                    source
                    .NameLocalizedStrings
                    .SingleOrDefault(s => s.Language == CareElementRequestContext.Current.DefaultLanguage);

                if (defaultLocalization != null)
                {
                    return Mapper.Map<ProtocolString, LocalizedStringResponseDto>(defaultLocalization);
                }
            }

            return null;
        }
    }
}