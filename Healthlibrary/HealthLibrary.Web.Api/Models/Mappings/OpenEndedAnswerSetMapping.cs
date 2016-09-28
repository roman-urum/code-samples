using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HealthLibrary.Common;
using HealthLibrary.Common.Helpers;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Web.Api.Models.Elements;

namespace HealthLibrary.Web.Api.Models.Mappings
{
    public class OpenEndedAnswerSetMapping : IClassMapping
    {
        public void CreateMap()
        {
            Mapper.CreateMap<AnswerSet, OpenEndedAnswerSetResponseDto>();

            Mapper.CreateMap<AnswerSet, SearchEntryDto>()
                .ForMember(d => d.Tags,
                    m => m.MapFrom(
                        s => s.Tags == null
                            ? new List<string>()
                            : s.Tags.OrderBy(t => t.Name, new NaturalSortComparer()).Select(t => t.Name)))
                .ForMember(d => d.Type, m => m.MapFrom(s => SearchCategoryType.OpenEndedAnswerSet));
        }
    }
}