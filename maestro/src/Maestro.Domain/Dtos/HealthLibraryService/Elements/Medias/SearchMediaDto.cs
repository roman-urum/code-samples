using System.Collections.Generic;
using Maestro.Common.ApiClient;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias
{
    public class SearchMediaDto : TagsSearchDto
    {
        [RequestParameter(RequestParameterType.QueryString)]
        public List<MediaType> Types { get; set; }
    }
}