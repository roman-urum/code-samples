using System;
using System.Linq;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.DomainObjects;
using VitalsService.Domain.Enums;
using VitalsService.Helpers;
using VitalsService.Web.Api.Models.Alerts;
using VitalsService.Web.Api.Models.Mappings.Converters;

namespace VitalsService.Web.Api.Models.Mappings
{
    /// <summary>
    /// AlertMapping.
    /// </summary>
    public class AlertMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<AlertRequestDto, Alert>()
                .ForMember(d => d.Id, o => o.MapFrom(s => SequentialGuidGenerator.Generate()))
                .ForMember(d => d.AcknowledgedUtc, o => o.UseValue(null))
                .ForMember(d => d.AcknowledgedBy, o => o.UseValue(null))
                .ForMember(d => d.Acknowledged, o => o.UseValue(false))
                .ForMember(d => d.PatientId, o => o.MapFrom(s => s.PatientId ?? Guid.Empty));

            CreateMap<Alert, BaseAlertResponseDto>().ConvertUsing<AlertsConverter>();

            CreateMap<VitalAlert, VitalAlertBriefResponseDto>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Vital.Value))
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
                .ForMember(d => d.Name, o => o.MapFrom(s => Enum.Parse(typeof(VitalType), s.Vital.Name, true)))
                .ForMember(d => d.Unit, o => o.MapFrom(s => Enum.Parse(typeof(UnitType), s.Vital.Unit, true)))
                .ForMember(d => d.ViolatedThreshold, o => o.MapFrom(s => s.Threshold));

            CreateMap<VitalAlert, VitalAlertResponseDto>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Vital.Value))
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
                .ForMember(d => d.Name, o => o.MapFrom(s => Enum.Parse(typeof(VitalType), s.Vital.Name, true)))
                .ForMember(d => d.Unit, o => o.MapFrom(s => Enum.Parse(typeof(UnitType), s.Vital.Unit, true)))
                .ForMember(d => d.ViolatedThreshold, o => o.MapFrom(s => s.Threshold))
                .ForMember(d => d.Measurement, o => o.MapFrom(s => s.Vital.Measurement));

            CreateMap<PatientAlerts, PatientAlertsDto>();

            CreateMap<HealthSessionElementAlert, HealthSessionElementAlertResponseDto>()
                .ForMember(d => d.ElementId, o => o.MapFrom(s => s.HealthSessionElement.ElementId))
                .ForMember(d => d.QuestionText, o => o.MapFrom(s => s.HealthSessionElement.Text))
                .ForMember(d => d.AnsweredUtc, o => o.MapFrom(s => s.HealthSessionElement.AnsweredUtc))
                .ForMember(d => d.AnsweredTz, o => o.MapFrom(s => s.HealthSessionElement.AnsweredTz))
                .ForMember(d => d.AnswerText, o => o.ResolveUsing(
                    source =>
                    {
                        var selectionAnswerValues = source.HealthSessionElement.Values.OfType<SelectionAnswer>().ToList();

                        if (selectionAnswerValues.Any())
                        {
                            return selectionAnswerValues.Select(v => v.Text).Aggregate((s1, s2) => s1 + ", " + s2);
                        }
                        var scaleAnswerValues = source.HealthSessionElement.Values.OfType<ScaleAnswer>().ToList();

                        if (scaleAnswerValues.Any())
                        {
                            return scaleAnswerValues.Select(v => v.Value.ToString()).Aggregate((s1, s2) => s1 + ", " + s2);
                        }

                        return string.Empty;
                    })
                );

            CreateMap<Threshold, ViolatedThresholdDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => Enum.Parse(typeof(VitalType), s.Name, true)))
                .ForMember(d => d.Unit, o => o.MapFrom(s => Enum.Parse(typeof(UnitType), s.Unit, true)));

            CreateMap<DefaultThreshold, ViolatedThresholdDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => Enum.Parse(typeof(VitalType), s.Name, true)))
                .ForMember(d => d.Unit, o => o.MapFrom(s => Enum.Parse(typeof(UnitType), s.Unit, true)));

            CreateMap<PatientThreshold, ViolatedThresholdDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => Enum.Parse(typeof(VitalType), s.Name, true)))
                .ForMember(d => d.Unit, o => o.MapFrom(s => Enum.Parse(typeof(UnitType), s.Unit, true)));
        }
    }
}