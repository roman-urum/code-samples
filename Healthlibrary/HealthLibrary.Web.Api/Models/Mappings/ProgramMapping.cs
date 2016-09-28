using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HealthLibrary.Common;
using HealthLibrary.Common.Helpers;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Program;
using HealthLibrary.Web.Api.Models.Mappings.Resolvers;
using HealthLibrary.Web.Api.Models.Programs;

namespace HealthLibrary.Web.Api.Models.Mappings
{
    /// <summary>
    /// ProgramMapping.
    /// </summary>
    public class ProgramMapping : IClassMapping
    {
        /// <summary>
        /// Creates the map.
        /// </summary>
        public void CreateMap()
        {
            Mapper.CreateMap<RecurrenceDto, Recurrence>()
                .ForMember(d => d.Program, o => o.Ignore())
                .ForMember(d => d.ProgramId, o => o.Ignore())
                .ForMember(d => d.ProgramDayElements, o => o.Ignore());

            Mapper.CreateMap<Recurrence, RecurrenceDto>();

            Mapper.CreateMap<ProgramDayElementDto, ProgramDayElement>()
                .ForMember(d => d.Recurrence, o => o.Ignore())
                .ForMember(d => d.RecurrenceId, o => o.Ignore())
                .ForMember(d => d.Program, o => o.Ignore())
                .ForMember(d => d.ProgramId, o => o.Ignore());

            Mapper.CreateMap<ProgramDayElement, ProgramDayElementDto>();

            Mapper.CreateMap<ProgramElementDto, ProgramElement>()
                .ForMember(d => d.ProgramDayElements, o => o.UseValue(new List<ProgramDayElement>()))
                .ForMember(d => d.Program, o => o.Ignore())
                .ForMember(d => d.ProgramId, o => o.Ignore());

            Mapper.CreateMap<ProgramElement, ProgramElementDto>();

            Mapper.CreateMap<ProgramRequestDto, Program>()
                .ForMember(d => d.ProgramDayElements, o => o.UseValue(new List<ProgramElement>()))
                .ForMember(d => d.ProgramElements, o => o.Ignore())
                .ForMember(d => d.Tags, o => o.Ignore());

            Mapper.CreateMap<Program, ProgramRequestDto>();

            Mapper.CreateMap<Program, ProgramBriefResponseDto>()
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags.OrderBy(t => t.Name, new NaturalSortComparer())));

            Mapper.CreateMap<Program, ProgramResponseDto>()
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags.OrderBy(t => t.Name, new NaturalSortComparer())));

            Mapper.CreateMap<ProgramElement, ProgramElementResponseDto>()
                .ForMember(d => d.ProtocolName, m => m.ResolveUsing<ProgramElementNameResolver>());

            Mapper.CreateMap<Program, SearchEntryDto>()
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags == null ? new List<string>() : s.Tags.OrderBy(t => t.Name, new NaturalSortComparer()).Select(t => t.Name)))
                .ForMember(d => d.Type, m => m.MapFrom(s => SearchCategoryType.Program));

            Mapper.CreateMap<Program, SearchProgramResponseDto>()
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags == null ? new List<string>() : s.Tags.OrderBy(t => t.Name, new NaturalSortComparer()).Select(t => t.Name)))
                .ForMember(d => d.Type, m => m.MapFrom(s => SearchCategoryType.Program))
                .ForMember(d => d.DurationDays, m => m.MapFrom(s => s.ProgramElements.Any() ? s.ProgramElements.Max(p => p.ProgramDayElements.Any() ? p.ProgramDayElements.Max(pd => pd.Day) : 0) : 0));
        }
    }
}