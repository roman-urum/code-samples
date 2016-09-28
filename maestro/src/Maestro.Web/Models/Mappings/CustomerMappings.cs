using System.Linq;
using AutoMapper;
using Maestro.Common.Helpers;
using Maestro.Domain.Dtos.CustomerService;
using Maestro.Web.Areas.Customer.Models.Settings.CustomerUsers;
using Maestro.Web.Areas.Customer.Models.Settings.General;
using Maestro.Web.Areas.Customer.Models.Settings.Sites;
using Maestro.Web.Models.Api.Dtos.Entities;
using Maestro.Web.Models.Customers;

namespace Maestro.Web.Models.Mappings
{
    /// <summary>
    /// CustomerMappings.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class CustomerMappings : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<CustomerResponseDto, GeneralSettingsViewModel>()
                .ForMember(d => d.PasswordExpiration, m => m.MapFrom(s => s.PasswordExpirationDays))
                .ForMember(d => d.IdleSessionTimeout, m => m.MapFrom(s => s.IddleSessionTimeout))
                .ForMember(d => d.CustomerName, m => m.MapFrom(s => s.Name));

            CreateMap<CustomerResponseDto, FullCustomerViewModel>()
                .ForMember(d => d.PasswordExpiration, m => m.MapFrom(s => s.PasswordExpirationDays))
                .ForMember(d => d.IdleSessionTimeout, m => m.MapFrom(s => s.IddleSessionTimeout))
                .ForMember(d => d.CustomerName, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Organizations,
                    m => m.MapFrom(s => s.Organizations.OrderBy(o => o.Name, new NaturalSortComparer())))
                .ForMember(d => d.Sites, m => m.MapFrom(s => s.Sites.OrderBy(o => o.Name, new NaturalSortComparer())));

            CreateMap<CustomerResponseDto, SingleSiteViewModel>()
                .ForMember(d => d.Website, m => m.MapFrom(s => s.Sites.FirstOrDefault()));

            CreateMap<CustomerResponseDto, MultipleSitesViewModel>()
                .ForMember(d => d.Websites, m => m.MapFrom(s => s.Sites.OrderBy(e => e.Name, new NaturalSortComparer())));

            CreateMap<SiteRequestDto, CustomerWebsiteViewModel>();

            CreateMap<CreateCustomerViewModel, CreateCustomerRequestDto>()
                .ForMember(d => d.Name, m => m.MapFrom(s => s.CustomerName))
                .ForMember(d => d.Subdomain, m => m.MapFrom(s => s.SubdomainName));

            CreateMap<CreateCustomerViewModel, SiteRequestDto>()
                .ForMember(d => d.Name, m => m.MapFrom(s => s.FirstSiteName));

            CreateMap<CreateUpdateSiteViewModel, SiteRequestDto>();

            CreateMap<SiteResponseDto, SiteViewModel>();

            CreateMap<GeneralSettingsViewModel, UpdateCustomerRequestDto>()
                .ForMember(d => d.PasswordExpirationDays, m => m.MapFrom(s => s.PasswordExpiration))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.CustomerName))
                .ForMember(d => d.IddleSessionTimeout, m => m.MapFrom(s => s.IdleSessionTimeout));

            CreateMap<OrganizationResponseDto, OrganizationViewModel>();

            CreateMap<CreateUpdateOrganizationViewModel, OrganizationRequestDto>();
        }
    }
}