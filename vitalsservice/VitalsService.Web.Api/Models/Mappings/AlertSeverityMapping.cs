using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Web.Api.Models.AlertSeverities;

namespace VitalsService.Web.Api.Models.Mappings
{
    /// <summary>
    /// AlertMapping.
    /// </summary>
    public class AlertSeverityMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<AlertSeverityRequestDto, AlertSeverity>();

            CreateMap<AlertSeverity, AlertSeverityResponseDto>();
        }
    }
}