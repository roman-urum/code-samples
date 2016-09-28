using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HealthLibrary.Common;
using HealthLibrary.Common.Helpers;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Protocol;
using HealthLibrary.Web.Api.Models.Elements;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;
using HealthLibrary.Web.Api.Models.Mappings.Converters;
using HealthLibrary.Web.Api.Models.Mappings.Resolvers;
using HealthLibrary.Web.Api.Models.Protocols;

namespace HealthLibrary.Web.Api.Models.Mappings
{
    /// <summary>
    /// ProtocolMapping.
    /// </summary>
    public class ProtocolMapping : IClassMapping
    {
        /// <summary>
        /// Creates the map.
        /// </summary>
        public void CreateMap()
        {
            Mapper.CreateMap<Protocol, ProtocolResponseDto>()
                .ForMember(d => d.Name, m => m.ResolveUsing<ProtocolNameStringResolver>())
                .ForMember(d => d.FirstProtocolElementId, m => m.MapFrom(s => s.ProtocolElements.Single(pe => pe.IsFirstProtocolElement).Id))
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags.OrderBy(t => t.Name, new NaturalSortComparer())));

            Mapper.CreateMap<ProtocolElement, ProtocolElementResponseDto>();

            Mapper.CreateMap<Element, ElementDto>().ConvertUsing<ElementConverter>();

            Mapper.CreateMap<Branch, BranchDto>();

            Mapper.CreateMap<Alert, AlertDto>();

            Mapper.CreateMap<Condition, ConditionDto>();

            Mapper.CreateMap<ProtocolString, LocalizedStringResponseDto>();

            Mapper.CreateMap<Protocol, SearchEntryDto>()
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags == null ? new List<string>() : s.Tags.OrderBy(t => t.Name, new NaturalSortComparer()).Select(t => t.Name)))
                .ForMember(d => d.Type, m => m.MapFrom(s => SearchCategoryType.Protocol))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.NameLocalizedStrings.First(ls => ls.Language == Settings.DefaultLanguage).Value));
        }
    }
}