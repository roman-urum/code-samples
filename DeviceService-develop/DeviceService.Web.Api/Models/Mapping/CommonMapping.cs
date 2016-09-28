using AutoMapper;
using DeviceService.Domain.Dtos;
using DeviceService.Web.Api.Models.Dtos.RequestsResponses;

namespace DeviceService.Web.Api.Models.Mapping
{
    /// <summary>
    /// CommonMapping.
    /// </summary>
    public class CommonMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        ///             Avoid calling the <see cref="T:AutoMapper.Mapper"/> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap(typeof(PagedResult<>), typeof(PagedResultDto<>));
        }
    }
}