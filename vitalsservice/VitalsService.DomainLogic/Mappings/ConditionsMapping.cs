using AutoMapper;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DomainLogic.Mappings
{
    /// <summary>
    /// PatientNotesMapping.
    /// </summary>
    public class ConditionsMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<Condition, Condition>()
                .ForMember(d => d.CustomerId, o => o.Ignore())
                .ForMember(d => d.DefaultThresholds, o => o.Ignore());
        }
    }
}