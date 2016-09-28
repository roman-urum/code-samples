using System.Linq;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Web.Api.Models.Conditions;

namespace VitalsService.Web.Api.Models.Mappings
{
    /// <summary>
    /// ConditionsMapping.
    /// </summary>
    public class ConditionsMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<Condition, ConditionResponseDto>()
                .ForMember(d => d.Tags, o => o.MapFrom(s => s.Tags.Select(t => t.Name)));

            CreateMap<ConditionRequestDto, Condition>()
                .ForMember(d => d.CustomerId, o => o.Ignore())
                .ForMember(d => d.Tags, o => o.Ignore())
                .ForMember(d => d.DefaultThresholds, o => o.Ignore());
        }
    }
}