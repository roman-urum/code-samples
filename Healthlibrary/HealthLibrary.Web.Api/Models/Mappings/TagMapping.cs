using AutoMapper;
using HealthLibrary.Common;
using HealthLibrary.Domain.Entities;

namespace HealthLibrary.Web.Api.Models.Mappings
{
    /// <summary>
    /// TagMapping.
    /// </summary>
    public class TagMapping : IClassMapping
    {
        /// <summary>
        /// Creates the map.
        /// </summary>
        public void CreateMap()
        {
            Mapper.CreateMap<Tag, string>()
                .ConvertUsing(source => source.Name);
        }
    }
}