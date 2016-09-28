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
using HealthLibrary.Web.Api.Models.Elements.ScaleAnswerSets;
using HealthLibrary.Web.Api.Models.Mappings.Converters;
using HealthLibrary.Web.Api.Models.Mappings.Resolvers;

namespace HealthLibrary.Web.Api.Models.Mappings
{
    /// <summary>
    /// ScaleAnswerSetMapping.
    /// </summary>
    public class ScaleAnswerSetMapping : IClassMapping
    {
        /// <summary>
        /// Creates the map.
        /// </summary>
        public void CreateMap()
        {
            Mapper.CreateMap<CreateScaleAnswerSetRequestDto, ScaleAnswerSet>()
                .BeforeMap((s, d) => d.HighLabelScaleAnswerSetStrings = new Collection<HighLabelScaleAnswerSetString>())
                .BeforeMap((s, d) => d.MidLabelScaleAnswerSetStrings = new Collection<MidLabelScaleAnswerSetString>())
                .BeforeMap((s, d) => d.LowLabelScaleAnswerSetStrings = new Collection<LowLabelScaleAnswerSetString>())
                .ForMember(d => d.HighLabelScaleAnswerSetStrings, m => m.Ignore())
                .ForMember(d => d.MidLabelScaleAnswerSetStrings, m => m.Ignore())
                .ForMember(d => d.LowLabelScaleAnswerSetStrings, m => m.Ignore())
                .ForMember(d => d.Type, m => m.Ignore())
                .ForMember(d => d.Tags, m => m.Ignore());

            Mapper.CreateMap<UpdateScaleAnswerSetRequestDto, ScaleAnswerSet>()
                .BeforeMap((s, d) => d.HighLabelScaleAnswerSetStrings = new Collection<HighLabelScaleAnswerSetString>())
                .BeforeMap((s, d) => d.MidLabelScaleAnswerSetStrings = new Collection<MidLabelScaleAnswerSetString>())
                .BeforeMap((s, d) => d.LowLabelScaleAnswerSetStrings = new Collection<LowLabelScaleAnswerSetString>())
                .ForMember(d => d.HighLabelScaleAnswerSetStrings, m => m.Ignore())
                .ForMember(d => d.MidLabelScaleAnswerSetStrings, m => m.Ignore())
                .ForMember(d => d.LowLabelScaleAnswerSetStrings, m => m.Ignore())
                .ForMember(d => d.Type, m => m.Ignore())
                .ForMember(d => d.Tags, m => m.Ignore());

            Mapper.CreateMap<ScaleAnswerSet, OpenEndedAnswerSetResponseDto>().ConvertUsing<AnswerSetConverter>();

            Mapper.CreateMap<ScaleAnswerSet, CreateScaleAnswerSetRequestDto>();

            Mapper.CreateMap<ScaleAnswerSet, ScaleAnswerSetResponseDto>()
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags.OrderBy(t => t.Name, new NaturalSortComparer())))
                .ForMember(d => d.LowLabel, m => m.ResolveUsing<ScaleAnswerSetLowLabelStringResolver>())
                .ForMember(d => d.MidLabel, m => m.ResolveUsing<ScaleAnswerSetMidLabelStringResolver>())
                .ForMember(d => d.HighLabel, m => m.ResolveUsing<ScaleAnswerSetHighLabelStringResolver>());

            Mapper.CreateMap<HighLabelScaleAnswerSetString, LocalizedStringWithAudioFileMediaResponseDto>();
            Mapper.CreateMap<MidLabelScaleAnswerSetString, LocalizedStringWithAudioFileMediaResponseDto>();
            Mapper.CreateMap<LowLabelScaleAnswerSetString, LocalizedStringWithAudioFileMediaResponseDto>();

            Mapper.CreateMap<CreateLocalizedStringRequestDto, LowLabelScaleAnswerSetString>()
                .ForMember(d => d.AudioFileMedia, m => m.Ignore());
            Mapper.CreateMap<CreateLocalizedStringRequestDto, MidLabelScaleAnswerSetString>()
                .ForMember(d => d.AudioFileMedia, m => m.Ignore());
            Mapper.CreateMap<CreateLocalizedStringRequestDto, HighLabelScaleAnswerSetString>()
                .ForMember(d => d.AudioFileMedia, m => m.Ignore());

            Mapper.CreateMap<UpdateLocalizedStringRequestDto, LowLabelScaleAnswerSetString>()
                .ForMember(d => d.AudioFileMedia, m => m.Ignore());
            Mapper.CreateMap<UpdateLocalizedStringRequestDto, MidLabelScaleAnswerSetString>()
                .ForMember(d => d.AudioFileMedia, m => m.Ignore());
            Mapper.CreateMap<UpdateLocalizedStringRequestDto, HighLabelScaleAnswerSetString>()
                .ForMember(d => d.AudioFileMedia, m => m.Ignore());

            Mapper.CreateMap<CreateLocalizedStringRequestDto, LocalizedString>()
                .ForMember(d => d.AudioFileMedia, m => m.Ignore());
            Mapper.CreateMap<CreateLocalizedStringRequestDto, LocalizedString>()
                .ForMember(d => d.AudioFileMedia, m => m.Ignore());

            Mapper.CreateMap<ScaleAnswerSet, SearchEntryDto>()
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags == null ? new List<string>() : s.Tags.OrderBy(t => t.Name, new NaturalSortComparer()).Select(t => t.Name)))
                .ForMember(d => d.Type, m => m.MapFrom(s => SearchCategoryType.ScaleAnswerSet));
        }
    }
}