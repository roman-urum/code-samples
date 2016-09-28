using System;
using AutoMapper;
using CustomerService.Domain.Entities;
using CustomerService.Web.Api.Models.Dtos.Organizations;

namespace CustomerService.Web.Api.Models.Mapping
{
    /// <summary>
    /// OrganizationsMapping.
    /// </summary>
    public class OrganizationsMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<Organization, OrganizationResponseDto>()
                .ForMember(d => d.IsArchived, m => m.MapFrom(s => s.IsDeleted));

            CreateMap<CreateOrganizationRequestDto, Organization>();

            CreateMap<UpdateOrganizationRequestDto, Organization>()
                .IncludeBase<CreateOrganizationRequestDto, Organization>();

            CreateMap<Organization, Guid>().ConvertUsing(s => s.Id);
        }
    }
}