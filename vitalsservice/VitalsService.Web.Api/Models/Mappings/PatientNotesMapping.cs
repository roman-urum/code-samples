using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Web.Api.Models.PatientNotes;

namespace VitalsService.Web.Api.Models.Mappings
{
    /// <summary>
    /// PatientNotesMapping.
    /// </summary>
    public class PatientNotesMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<SuggestedNotableRequestDto, SuggestedNotable>();

            CreateMap<SuggestedNotable, SuggestedNotableDto>();

            CreateMap<NoteRequestDto, Note>()
                .ForMember(d => d.Notables, m => m.MapFrom(s => s.Notables != null ? s.Notables.Select(n => new NoteNotable() { Name = n }) : new List<NoteNotable>()));

            CreateMap<Note, NoteBriefResponseDto>()
                .ForMember(d => d.Notables, m => m.MapFrom(s => s.Notables != null ? s.Notables.Select(n => n.Name) : new List<string>()));

            CreateMap<NoteNotable, string>()
                .ConvertUsing(source => source.Name);

            CreateMap<Note, NoteDetailedResponseDto>();
        }
    }
}