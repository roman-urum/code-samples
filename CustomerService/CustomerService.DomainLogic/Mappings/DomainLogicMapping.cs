using AutoMapper;
using CustomerService.Domain.Entities;

namespace CustomerService.DomainLogic.Mappings
{
    /// <summary>
    /// DomainLogicMapping.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class DomainLogicMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<Customer, Customer>();

            CreateMap<Site, Site>();

            CreateMap<Organization, Organization>()
                .ForMember(d => d.Sites, m => m.Ignore())
                .ForMember(d => d.ChildOrganizations, m => m.Ignore())
                .ForMember(d => d.ParentOrganization, m => m.Ignore())
                .ForMember(d => d.IsDeleted, m => m.Ignore())
                .ForMember(d => d.Customer, m => m.Ignore());
        }
    }
}