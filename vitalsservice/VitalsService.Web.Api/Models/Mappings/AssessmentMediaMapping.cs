using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Web.Api.Models.AssessmentMedias;
using VitalsService.Web.Api.Models.Mappings.Resolvers;

namespace VitalsService.Web.Api.Models.Mappings
{
    /// <summary>
    /// AlertMapping.
    /// </summary>
    public class AssessmentMediaMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<CreateAssessmentMediaRequestDto, AssessmentMedia>();

            CreateMap<AssessmentMedia, AssessmentMediaResponseDto>()
                .ForMember(d=>d.AssessmentMediaUrl, s=>s.ResolveUsing<AssessmentMediaUrlResolver>());
        }
    }
}