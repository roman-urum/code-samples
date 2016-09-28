using System;
using AutoMapper;
using CustomerService.Domain.Entities;
using CustomerService.Web.Api.Models.Dtos.CategoryOfCare;

namespace CustomerService.Web.Api.Models.Mapping
{
    /// <summary>
    /// CategoriesOfCareMapping.
    /// </summary>
    public class CategoriesOfCareMapping : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<CategoryOfCareRequestDto, CategoryOfCare>();

            CreateMap<CategoryOfCare, CategoryOfCareResponseDto>();

            CreateMap<Guid, CategoryOfCare>()
                .ConvertUsing(s => new CategoryOfCare() { Id = s });

            CreateMap<CategoryOfCare, Guid>().ConvertUsing(s => s.Id);
        }
    }
}