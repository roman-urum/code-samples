using System;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.TextMediaElements;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// ITextMediaElementsControllerHelper.
    /// </summary>
    public interface ITextMediaElementsControllerHelper
    {
        /// <summary>
        /// Creates the specified new text media element dto.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="newTextMediaElementDto">The new text media element dto.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, CreateTextMediaElementStatus>> Create(
            int customerId, 
            CreateTextMediaElementRequestDto newTextMediaElementDto
        );

        /// <summary>
        /// Gets the elements.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        Task<PagedResultDto<TextMediaElementResponseDto>> GetElements(
            int customerId,
            TextMediaElementSearchDto searchCriteria
        );

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="textMediaElementId">The identifier.</param>
        /// <param name="textMediaElementDto">The text media element dto.</param>
        /// <returns></returns>
        Task<UpdateTextMediaElementStatus> Update(
            int customerId, 
            Guid textMediaElementId, 
            UpdateTextMediaElementRequestDto textMediaElementDto
        );

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="textMediaElementId">The text media element identifier.</param>
        /// <returns></returns>
        Task<DeteleTextMediaElementStatus> Delete(int customerId, Guid textMediaElementId);

        /// <summary>
        /// Updates the localization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="textMediaElementId">The text media element identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<UpdateTextMediaElementStatus> UpdateLocalization(
            int customerId, 
            Guid textMediaElementId,
            UpdateTextMediaElementLocalizedRequestDto model
        );

        /// <summary>
        /// Get text and mediaelement by id
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="textMediaElementId">The text media element identifier.</param>
        /// <returns></returns>
        Task<TextMediaElementResponseDto> GetElement(int customerId, Guid textMediaElementId);
    }
} 