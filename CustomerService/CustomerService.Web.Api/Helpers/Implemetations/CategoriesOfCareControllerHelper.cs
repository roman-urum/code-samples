using System;
using System.Threading.Tasks;
using AutoMapper;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Domain.Entities;
using CustomerService.DomainLogic.Services.Interfaces;
using CustomerService.Web.Api.Helpers.Interfaces;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.CategoryOfCare;

namespace CustomerService.Web.Api.Helpers.Implemetations
{
    /// <summary>
    /// CategoriesOfCareControllerHelper.
    /// </summary>
    public class CategoriesOfCareControllerHelper : ICategoriesOfCareControllerHelper
    {
        private readonly ICategoriesOfCareService сategoriesOfCareService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesOfCareControllerHelper"/> class.
        /// </summary>
        /// <param name="сategoriesOfCareService">The сategories of cares service.</param>
        public CategoriesOfCareControllerHelper(ICategoriesOfCareService сategoriesOfCareService)
        {
            this.сategoriesOfCareService = сategoriesOfCareService;
        }

        /// <summary>
        /// Creates the category of care.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, CategoryOfCareStatus>> CreateCategoryOfCare(
            int customerId, 
            CategoryOfCareRequestDto request
        )
        {
            var categoryOfCare = Mapper.Map<CategoryOfCareRequestDto, CategoryOfCare>(request);
            categoryOfCare.CustomerId = customerId;

            var createdCategoryOfCare = await сategoriesOfCareService.CreateCategoryOfCare(categoryOfCare);

            if (!createdCategoryOfCare.Status.HasFlag(CategoryOfCareStatus.Success))
            {
                return new OperationResultDto<Guid, CategoryOfCareStatus>()
                {
                    Status = createdCategoryOfCare.Status
                };
            }

            return new OperationResultDto<Guid, CategoryOfCareStatus>()
            {
                Content = createdCategoryOfCare.Content.Id,
                Status = createdCategoryOfCare.Status
            };
        }

        /// <summary>
        /// Updates the category of care.
        /// </summary>
        /// <param name="categoryOfCareId">The category of care identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<CategoryOfCareStatus> UpdateCategoryOfCare(
            Guid categoryOfCareId, 
            CategoryOfCareRequestDto request
        )
        {
            var categoryOfCare = Mapper.Map<CategoryOfCareRequestDto, CategoryOfCare>(request);
            categoryOfCare.Id = categoryOfCareId;

            return await сategoriesOfCareService.UpdateCategoryOfCare(categoryOfCare);
        }

        /// <summary>
        /// Deletes the category of care.
        /// </summary>
        /// <param name="categoryOfCareId">The category of care identifier.</param>
        /// <returns></returns>
        public async Task<CategoryOfCareStatus> DeleteCategoryOfCare(Guid categoryOfCareId)
        {
            return await сategoriesOfCareService.DeleteCategoryOfCare(categoryOfCareId);
        }

        /// <summary>
        /// Gets the category of care.
        /// </summary>
        /// <param name="categoryOfCareId">The category of care identifier.</param>
        /// <returns></returns>
        public async Task<CategoryOfCareResponseDto> GetCategoryOfCare(Guid categoryOfCareId)
        {
            var result = await сategoriesOfCareService.GetCategoryOfCare(categoryOfCareId);

            return Mapper.Map<CategoryOfCare, CategoryOfCareResponseDto>(result);
        }

        /// <summary>
        /// Gets the categories of care.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<CategoryOfCareResponseDto>> GetCategoriesOfCare(
            int customerId,
            BaseSearchDto request
        )
        {
            var categoriesOfCare = await сategoriesOfCareService.GetCategoriesOfCare(customerId, request);

            return Mapper.Map<PagedResult<CategoryOfCare>, PagedResultDto<CategoryOfCareResponseDto>>(categoriesOfCare);
        }
    }
}