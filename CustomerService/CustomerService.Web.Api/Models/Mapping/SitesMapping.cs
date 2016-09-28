using System;
using AutoMapper;
using CustomerService.Domain.Entities;
using CustomerService.Web.Api.Models.Dtos.Site;

namespace CustomerService.Web.Api.Models.Mapping
{
    /// <summary>
    /// SiteMapping.
    /// </summary>
    public class SitesMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<Site, SiteResponseDto>()
                .ForMember(d => d.IsArchived, m => m.MapFrom(s => s.IsDeleted));

            CreateMap<BaseSiteDto, Site>();

            CreateMap<CreateSiteRequestDto, Site>().IncludeBase<BaseSiteDto, Site>();

            CreateMap<UpdateSiteRequestDto, Site>().IncludeBase<BaseSiteDto, Site>();

            CreateMap<Site, Guid>().ConvertUsing(s => s.Id);
        }
    }
}