using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Web.Api.Models.Converters;
using VitalsService.Domain.EsbEntities;
using VitalsService.Web.Api.Models.HealthSessions;
using VitalsService.Web.Api.Models.Mappings.Resolvers;

namespace VitalsService.Web.Api.Models.Mappings
{
    /// <summary>
    /// Models mapping rules for health sessions.
    /// </summary>
    public class HealthSessionMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            // Model to Entity
            CreateMap<HealthSessionRequestDto, HealthSession>()
                .BeforeMap((s, d) => d.Elements = new Collection<HealthSessionElement>())
                .ForMember(d => d.Elements, m => m.Ignore())
                .ForMember(d => d.CompletedUtc, m => m.MapFrom(s => s.CompletedUtc ?? DateTime.UtcNow))
                .ForMember(d => d.ScheduledUtc, m => m.MapFrom(s => s.ScheduledUtc ?? DateTime.UtcNow))
                .ForMember(d => d.StartedUtc, m => m.MapFrom(s => s.StartedUtc ?? DateTime.UtcNow));

            CreateMap<HealthSessionElementRequestDto, HealthSessionElement>()
                .BeforeMap((s, d) => d.Values = new List<HealthSessionElementValue>())
                .ForMember(d => d.Values, m => m.Ignore())
                .ForMember(d => d.HealthSessionElementAlert, m => m.Ignore());

            CreateMap<HealthSessionElementValueDto, HealthSessionElementValue>()
                .ConvertUsing<HealthSessionElementValueTypeConverter>();

            CreateMap<SelectionAnswerDto, SelectionAnswer>();
            CreateMap<ScaleAnswerDto, ScaleAnswer>();
            CreateMap<FreeFormAnswerDto, FreeFormAnswer>();

            // Entity to Model
            CreateMap<HealthSession, HealthSessionResponseDto>();
            CreateMap<HealthSessionElement, HealthSessionElementResponseDto>()
                .ForMember(d => d.Values, m => m.ResolveUsing<HealthSessionElementValueModelResolver>());

            CreateMap<SelectionAnswer, SelectionAnswerDto>();
            CreateMap<ScaleAnswer, ScaleAnswerDto>();
            CreateMap<FreeFormAnswer, FreeFormAnswerDto>();
            CreateMap<MeasurementValue, MeasurementValueResponseDto>()
                .ForMember(d => d.Value, m => m.MapFrom(s => s.Measurement));

            CreateMap<HealthSession, HealthSessionMessage>();
            CreateMap<HealthSessionElement, HealthSessionElementMessage>();
            CreateMap<HealthSessionElementValue, HealthSessionElementValueMessage>();
            CreateMap<HealthSessionRequestDto, HealthSessionEsbDto>().
                ForMember(d => d.CustomerId, o => o.Ignore()).
                ForMember(d => d.PatientId, o => o.Ignore()).
                ForMember(d => d.Id, o => o.Ignore());

            CreateMap<HealthSession, HealthSessionEsbDto>().
                ForMember(d => d.Elements, o => o.Ignore());

            CreateMap<AssessmentValue, AssessmentValueResponseDto>();
            CreateMap<AssessmentValueRequestDto, AssessmentValue>()
                .ForMember(d => d.AssessmentMediaId, m => m.MapFrom(s => s.Value));
        }
    }
}