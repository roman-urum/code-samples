using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HealthLibrary.Common;
using HealthLibrary.Common.Helpers;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Web.Api.Models.Elements.MeasurementElements;

namespace HealthLibrary.Web.Api.Models.Mappings
{
    /// <summary>
    /// MeasurementMapping.
    /// </summary>
    public class MeasurementMapping : IClassMapping
    {
        /// <summary>
        /// Creates the map.
        /// </summary>
        public void CreateMap()
        {
            Mapper.CreateMap<MeasurementElement, MeasurementResponseDto>();

            Mapper.CreateMap<MeasurementElement, SearchEntryDto>()
                .ForMember(d => d.Tags,
                    m => m.MapFrom(s => s.Tags == null
                        ? new List<string>()
                        : s.Tags.OrderBy(t => t.Name, new NaturalSortComparer()).Select(t => t.Name)))
                .ForMember(d => d.Type, m => m.MapFrom(s => SearchCategoryType.MeasurementElement));
        }
    }
}