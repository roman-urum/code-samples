using System;
using AutoMapper;
using Maestro.Domain.Dtos.HealthLibraryService;
using Maestro.Web.Areas.Customer.Models.CareBuilder;

namespace Maestro.Web.Models.Mappings.Converters
{
    public class SearchEntryConverter : ITypeConverter<SearchEntryDto, SearchEntryResponseViewModel>
    {
        public SearchEntryResponseViewModel Convert(ResolutionContext context)
        {
            if (context == null || context.SourceValue == null || !(context.SourceValue is SearchEntryDto))
            {
                throw new ArgumentException("context.SourceValue");
            }

            var searchEntry = (SearchEntryDto) context.SourceValue;

            if (searchEntry is SearchTextAndMediaDto)
            {
                var result =
                    Mapper.Map<SearchTextAndMediaDto, SearchTextAndMediaResponseViewModel>(
                        (SearchTextAndMediaDto) context.SourceValue);

                return result;
            }

            if (searchEntry is SearchProgramResultDto)
            {
                var result =
                    Mapper.Map<SearchProgramResultDto, SearchProgramResponseViewModel>(
                        (SearchProgramResultDto) context.SourceValue);

                return result;
            }

            return new SearchEntryResponseViewModel()
            {
                Id = searchEntry.Id,
                Name = searchEntry.Name,
                Tags = searchEntry.Tags,
                Type = searchEntry.Type
            };
        }
    }
}