using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using HealthLibrary.Common;
using HealthLibrary.Common.Helpers;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Web.Api.Models.Elements;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;
using HealthLibrary.Web.Api.Models.Elements.SelectionAnswerChoices;
using HealthLibrary.Web.Api.Models.Elements.SelectionAnswerSets;
using HealthLibrary.Web.Api.Models.Mappings.Converters;
using HealthLibrary.Web.Api.Models.Mappings.Resolvers;

namespace HealthLibrary.Web.Api.Models.Mappings
{
    /// <summary>
    /// SelectionAnswerSetMapping.
    /// </summary>
    public class SelectionAnswerSetMapping : IClassMapping
    {
        /// <summary>
        /// Creates the map.
        /// </summary>
        public void CreateMap()
        {
            // Create request

            Mapper.CreateMap<CreateSelectionAnswerSetRequestDto, SelectionAnswerSet>()
                .BeforeMap((s, d) => d.SelectionAnswerChoices = new Collection<SelectionAnswerChoice>())
                .ForMember(d => d.SelectionAnswerChoices, m => m.Ignore())
                .ForMember(d => d.Type, m => m.Ignore())
                .ForMember(d => d.Tags, m => m.Ignore());

            Mapper.CreateMap<CreateSelectionAnswerChoiceRequestDto, SelectionAnswerChoice>()
                .BeforeMap((s, d) => d.LocalizedStrings = new Collection<SelectionAnswerChoiceString>())
                .ForMember(d => d.LocalizedStrings, m => m.Ignore());

            Mapper.CreateMap<CreateAnswerStringRequestDto, SelectionAnswerChoiceString>()
                .ForMember(d => d.AudioFileMedia, m => m.Ignore());

            // Update request models

            Mapper.CreateMap<UpdateSelectionAnswerSetRequestDto, SelectionAnswerSet>()
                .BeforeMap((s, d) => d.SelectionAnswerChoices = new Collection<SelectionAnswerChoice>())
                .ForMember(d => d.SelectionAnswerChoices, m => m.Ignore())
                .ForMember(d => d.Type, m => m.Ignore())
                .ForMember(d => d.Tags, m => m.Ignore());

            Mapper.CreateMap<UpdateSelectionAnswerChoiceRequestDto, SelectionAnswerChoice>()
                .BeforeMap((s, d) => d.LocalizedStrings = new Collection<SelectionAnswerChoiceString>())
                .ForMember(d => d.LocalizedStrings, m => m.Ignore());

            Mapper.CreateMap<UpdateAnswerStringRequestDto, SelectionAnswerChoiceString>()
                .ForMember(d => d.AudioFileMedia, m => m.Ignore());

            // Update request for localized strings

            Mapper.CreateMap<UpdateSelectionAnswerSetLocalizedRequestDto, SelectionAnswerSet>()
                .BeforeMap((s, d) => d.SelectionAnswerChoices = new Collection<SelectionAnswerChoice>())
                .ForMember(d => d.SelectionAnswerChoices, m => m.Ignore())
                .ForMember(d => d.Type, m => m.Ignore());

            // Get request models

            Mapper.CreateMap<SelectionAnswerChoiceString, LocalizedStringWithAudioFileMediaResponseDto>();

            Mapper.CreateMap<SelectionAnswerSet, SelectionAnswerSetResponseDto>()
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags.OrderBy(t => t.Name, new NaturalSortComparer())))
                .ForMember(d => d.SelectionAnswerChoices, m => m.MapFrom(s => s.SelectionAnswerChoices.OrderBy(sac => sac.Sort)));

            Mapper.CreateMap<SelectionAnswerChoice, SelectionAnswerChoiceResponseDto>()
                .ForMember(d => d.AnswerString, m => m.ResolveUsing<SelectionAnswerChoiceResponseResolver>());

            Mapper.CreateMap<SelectionAnswerSet, OpenEndedAnswerSetResponseDto>().ConvertUsing<AnswerSetConverter>();

            Mapper.CreateMap<SelectionAnswerSet, SearchEntryDto>()
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags == null ? new List<string>() : s.Tags.OrderBy(t => t.Name, new NaturalSortComparer()).Select(t => t.Name)))
                .ForMember(d => d.Type, m => m.MapFrom(s => SearchCategoryType.SelectionAnswerSet));
        }
    }
}