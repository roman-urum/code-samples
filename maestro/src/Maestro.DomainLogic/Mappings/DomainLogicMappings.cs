using AutoMapper;
using Maestro.Domain.Dtos.TokenService.RequestsResponses;

namespace Maestro.DomainLogic.Mappings
{
    /// <summary>
    /// DomainLogicMappings.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class DomainLogicMappings : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<PrincipalResponseModel, UpdatePrincipalModel>();
        }
    }
}