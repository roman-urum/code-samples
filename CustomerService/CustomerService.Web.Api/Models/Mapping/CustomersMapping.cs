using AutoMapper;
using CustomerService.Domain.Entities;
using CustomerService.Web.Api.Models.Dtos.Customer;

namespace CustomerService.Web.Api.Models.Mapping
{
    /// <summary>
    /// CustomerMapping.
    /// </summary>
    public class CustomersMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<Customer, BriefCustomerResponseDto>()
                .ForMember(d => d.IsArchived, m => m.MapFrom(s => s.IsDeleted));

            CreateMap<Customer, FullCustomerResponseDto>()
                .ForMember(d => d.IsArchived, m => m.MapFrom(s => s.IsDeleted));

            CreateMap<UpdateCustomerRequestDto, Customer>();

            CreateMap<CreateCustomerRequestDto, Customer>();
        }
    }
}