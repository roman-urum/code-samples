using AutoMapper;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DomainLogic.Mappings
{
    /// <summary>
    /// PatientNotesMapping.
    /// </summary>
    public class PatientNotesMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<SuggestedNotable, SuggestedNotable>()
                .ForMember(d => d.CreatedUtc, m => m.Ignore())
                .ForMember(d => d.UpdatedUtc, m => m.Ignore());
        }
    }
}