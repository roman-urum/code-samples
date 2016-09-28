﻿using System.Linq;
using AutoMapper;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Mappings.Resolvers
{
    /// <summary>
    /// ScaleAnswerSetMidLabelStringResolver.
    /// </summary>
    public class ScaleAnswerSetMidLabelStringResolver :
        ValueResolver<ScaleAnswerSet, LocalizedStringWithAudioFileMediaResponseDto>
    {
        /// <summary>
        /// Resolves the core.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        protected override LocalizedStringWithAudioFileMediaResponseDto ResolveCore(ScaleAnswerSet source)
        {
            if (!string.IsNullOrEmpty(CareElementRequestContext.Current.Language))
            {
                var particularLocalization = 
                    source
                    .MidLabelScaleAnswerSetStrings
                    .SingleOrDefault(s => s.Language == CareElementRequestContext.Current.Language);

                if (particularLocalization != null)
                {
                    return Mapper.Map<MidLabelScaleAnswerSetString, LocalizedStringWithAudioFileMediaResponseDto>(particularLocalization);
                }
            }

            if (!string.IsNullOrEmpty(CareElementRequestContext.Current.DefaultLanguage))
            {
                var defaultLocalization = 
                    source
                    .MidLabelScaleAnswerSetStrings
                    .SingleOrDefault(s => s.Language == CareElementRequestContext.Current.DefaultLanguage);

                if (defaultLocalization != null)
                {
                    return Mapper.Map<MidLabelScaleAnswerSetString, LocalizedStringWithAudioFileMediaResponseDto>(defaultLocalization);
                }
            }

            return null;
        }
    }
}