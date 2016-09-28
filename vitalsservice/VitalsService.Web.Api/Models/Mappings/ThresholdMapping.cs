using System;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models.Thresholds;

namespace VitalsService.Web.Api.Models.Mappings
{
    /// <summary>
    /// ThresholdMapping.
    /// </summary>
    public class ThresholdMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<ThresholdRequestDto, PatientThreshold>()
                .ForMember(d => d.Type, m => m.MapFrom(s => s.Type.ToString()))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name.ToString()))
                .ForMember(d => d.Unit, m => m.MapFrom(s => s.Unit.ToString()))
                .ForMember(d => d.AlertSeverityId, m => m.MapFrom(s => s.AlertSeverityId))
                .ForMember(d => d.AlertSeverity, m => m.Ignore());

            CreateMap<PatientThreshold, PatientThresholdDto>()
                .ForMember(d => d.Type, m => m.MapFrom(s => Enum.Parse(typeof(ThresholdType), s.Type, true)))
                .ForMember(d => d.Name, m => m.MapFrom(s => Enum.Parse(typeof(VitalType), s.Name, true)))
                .ForMember(d => d.Unit, m => m.MapFrom(s => Enum.Parse(typeof(UnitType), s.Unit, true)));

            CreateMap<DefaultThresholdRequestDto, DefaultThreshold>()
                .ForMember(d => d.Type, m => m.MapFrom(s => s.Type.ToString()))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name.ToString()))
                .ForMember(d => d.Unit, m => m.MapFrom(s => s.Unit.ToString()))
                .ForMember(d => d.AlertSeverityId, m => m.MapFrom(s => s.AlertSeverityId))
                .ForMember(d => d.AlertSeverity, m => m.Ignore());

            CreateMap<DefaultThreshold, DefaultThresholdDto>()
                .ForMember(d => d.Type, m => m.MapFrom(s => Enum.Parse(typeof(ThresholdType), s.Type, true)))
                .ForMember(d => d.Name, m => m.MapFrom(s => Enum.Parse(typeof(VitalType), s.Name, true)))
                .ForMember(d => d.Unit, m => m.MapFrom(s => Enum.Parse(typeof(UnitType), s.Unit, true)));

            CreateMap<Threshold, BaseThresholdDto>()
                .Include<DefaultThreshold, DefaultThresholdDto>()
                .Include<PatientThreshold, PatientThresholdDto>();
        }
    }
}