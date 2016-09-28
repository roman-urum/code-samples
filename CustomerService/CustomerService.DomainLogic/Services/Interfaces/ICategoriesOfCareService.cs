using System;
using System.Threading.Tasks;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Domain.Entities;

namespace CustomerService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// ICategoriesOfCareService.
    /// </summary>
    public interface ICategoriesOfCareService
    {
        /// <summary>
        /// Creates the category of care.
        /// </summary>
        /// <param name="categoryOfCare">The category of care.</param>
        /// <returns></returns>
        Task<OperationResultDto<CategoryOfCare, CategoryOfCareStatus>> 
            CreateCategoryOfCare(CategoryOfCare categoryOfCare);

        /// <summary>
        /// Updates the category of care.
        /// </summary>
        /// <param name="categoryOfCare">The category of care.</param>
        /// <returns></returns>
        Task<CategoryOfCareStatus> UpdateCategoryOfCare(CategoryOfCare categoryOfCare);

        /// <summary>
        /// Gets the category of care.
        /// </summary>
        /// <param name="categoryOfCareId">The category of care identifier.</param>
        /// <returns></returns>
        Task<CategoryOfCare> GetCategoryOfCare(Guid categoryOfCareId);

        /// <summary>
        /// Gets the category of care.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Task<CategoryOfCare> GetCategoryOfCare(int customerId, string name);

        /// <summary>
        /// Deletes the category of care.
        /// </summary>
        /// <param name="categoryOfCareId">The category of care identifier.</param>
        /// <returns></returns>
        Task<CategoryOfCareStatus> DeleteCategoryOfCare(Guid categoryOfCareId);

        /// <summary>
        /// Gets the categories of cares.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResult<CategoryOfCare>> GetCategoriesOfCare(int customerId, BaseSearchDto request);
    }
}