using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.DomainLogic.Services.Results;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.QuestionElements;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// Provides methods to manage question elements.
    /// </summary>
    public interface IQuestionElementControllerHelper
    {
        /// <summary>
        /// Creates new question element.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        Task<ServiceActionResult<QuestionElementActionStatus, Guid>> Create(
            int customerId,
            CreateQuestionElementRequestDto dto
        );

        /// <summary>
        /// Updates question element with provided dto.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        Task<ServiceActionResult<QuestionElementActionStatus, QuestionElementResponseDto>> Update(
            int customerId,
            Guid questionElementId,
            UpdateQuestionElementRequestDto dto
        );

        /// <summary>
        /// Updates question element with provided dto.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        Task<QuestionElementResponseDto> UpdateLocalizedString(
            int customerId,
            Guid questionElementId, 
            UpdateQuestionElementLocalizedRequestDto dto
        );

        /// <summary>
        /// Returns required answer set by id or null.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        Task<QuestionElementResponseDto> Get(int customerId, Guid questionElementId, bool isBrief);

        /// <summary>
        /// Deletes entity with specified id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <returns></returns>
        Task<DeleteStatus> Delete(int customerId, Guid questionElementId);

        /// <summary>
        /// Returns model with search results.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="criteria">The criteria.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        Task<PagedResultDto<QuestionElementResponseDto>> Find(int customerId, TagsSearchDto criteria, bool isBrief);
    }
}
