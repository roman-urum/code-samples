using AutoMapper;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DomainLogic.Mappings
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
            CreateMap<PatientThreshold, PatientThreshold>()
                .ForMember(d => d.Id, m => m.Ignore())
                .ForMember(d => d.AlertSeverity, m => m.Ignore())
                .ForMember(d => d.VitalAlerts, m => m.Ignore());

            CreateMap<DefaultThreshold, DefaultThreshold>()
                .ForMember(d => d.Id, m => m.Ignore())
                .ForMember(d => d.AlertSeverity, m => m.Ignore())
                .ForMember(d => d.VitalAlerts, m => m.Ignore());
        }
    }
}