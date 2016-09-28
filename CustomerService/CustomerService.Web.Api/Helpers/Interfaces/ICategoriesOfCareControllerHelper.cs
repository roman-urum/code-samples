using System;
using System.Threading.Tasks;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.CategoryOfCare;

namespace CustomerService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// ICategoriesOfCareControllerHelper.
    /// </summary>
    public interface ICategoriesOfCareControllerHelper
    {
        /// <summary>
        /// Creates the category of care.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, CategoryOfCareStatus>> CreateCategoryOfCare(
            int customerId, 
            CategoryOfCareRequestDto request
        );

        /// <summary>
        /// Updates the category of care.
        /// </summary>
        /// <param name="categoryOfCareId">The category of care identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<CategoryOfCareStatus> UpdateCategoryOfCare(
            Guid categoryOfCareId,
            CategoryOfCareRequestDto request
        );

        /// <summary>
        /// Deletes the category of care.
        /// </summary>
        /// <param name="categoryOfCareId">The category of care identifier.</param>
        /// <returns></returns>
        Task<CategoryOfCareStatus> DeleteCategoryOfCare(Guid categoryOfCareId);

        /// <summary>
        /// Gets the category of care.
        /// </summary>
        /// <param name="categoryOfCareId">The category of care identifier.</param>
        /// <returns></returns>
        Task<CategoryOfCareResponseDto> GetCategoryOfCare(Guid categoryOfCareId);

        /// <summary>
        /// Gets the categories of cares.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResultDto<CategoryOfCareResponseDto>> GetCategoriesOfCare(int customerId, BaseSearchDto request);
    }
}