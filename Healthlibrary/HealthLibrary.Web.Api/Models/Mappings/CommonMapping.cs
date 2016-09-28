using AutoMapper;
using HealthLibrary.Common;
using HealthLibrary.Domain.Dtos;

namespace HealthLibrary.Web.Api.Models.Mappings
{
    /// <summary>
    /// CommonMapping.
    /// </summary>
    public class CommonMapping : IClassMapping
    {
        /// <summary>
        /// Creates the map.
        /// </summary>
        public void CreateMap()
        {
            Mapper.CreateMap(typeof(PagedResult<>), typeof(PagedResultDto<>));
        }
    }
}