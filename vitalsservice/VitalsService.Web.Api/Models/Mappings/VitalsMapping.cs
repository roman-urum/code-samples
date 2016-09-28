using System;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Enums;
using VitalsService.Domain.EsbEntities;
using VitalsService.Extensions;
using VitalsService.Web.Api.Models.Mappings.Resolvers;

namespace VitalsService.Web.Api.Models.Mappings
{
    /// <summary>
    /// Mapping of interfaces to implementations for vitals API.
    /// </summary>
    public class VitalsMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<VitalDto, Vital>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name.ToString()))
                .ForMember(d => d.Unit, o => o.MapFrom(s => s.Unit.ToString()));

            CreateMap<MeasurementNoteDto, MeasurementNote>();

            CreateMap<DeviceDto, Device>();

            CreateMap<MeasurementRequestDto, Measurement>()
                .ForMember(d => d.ProcessingType, o => o.MapFrom(s => s.ProcessingType ?? ProcessingType.Debugging));

            CreateMap<UpdateMeasurementRequestDto, Measurement>();

            CreateMap<Measurement, MeasurementResponseDto>()
                .ForMember(d => d.HealthSessionId, m => m.ResolveUsing<MeasurementSessionIdResolver>());

            CreateMap<Measurement, MeasurementBriefResponseDto>();

            CreateMap<Vital, VitalDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => Enum.Parse(typeof(VitalType), s.Name, true)))
                .ForMember(d => d.Unit, o => o.MapFrom(s => Enum.Parse(typeof(UnitType), s.Unit, true)));

            CreateMap<Vital, VitalResponseDto>()
                .ForMember(d => d.IsAutomated, o => o.MapFrom(s => s.Measurement.IsAutomated));

            CreateMap<Vital, VitalBriefResponseDto>()
                .ForMember(d => d.IsAutomated, o => o.MapFrom(s => s.Measurement.IsAutomated));

            CreateMap<MeasurementNote, MeasurementNoteDto>();

            CreateMap<Device, DeviceDto>();

            CreateMap<Vital, VitalMessage>();

            CreateMap<Measurement, MeasurementMessage>()
                .ForMember(d => d.ProcessingType, o => o.MapFrom(s => s.ProcessingType.Description()))
                .ForMember(d => d.HealthSessionId, o => o.ResolveUsing<MeasurementSessionIdResolver>());

            CreateMap<MeasurementNote, NoteMessage>();

            CreateMap<Device, DeviceMessage>();
        }
    }
}