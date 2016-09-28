using System;
using System.Threading.Tasks;

using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Conditions;

namespace VitalsService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IConditionsControllerHelper.
    /// </summary>
    public interface IConditionsControllerHelper
    {
        /// <summary>
        /// Gets the condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="conditionId">The condition identifier</param>
        /// <returns>Condition entity or null</returns>
        Task<OperationResultDto<ConditionResponseDto, ConditionStatus>> GetCondition(int customerId, Guid conditionId);

        /// <summary>
        /// Get the list of conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The search request.</param>
        /// <returns>The list of conditions.</returns>
        Task<PagedResultDto<ConditionResponseDto>> GetConditions(int customerId, ConditionSearchDto request);

        /// <summary>
        /// Creates condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The condition entity.</param>
        /// <returns>The creation result.</returns>
        Task<OperationResultDto<Guid, ConditionStatus>> CreateCondition(int customerId, ConditionRequestDto request);

        /// <summary>
        /// Updates the condition.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="request">The condition entity.</param>
        /// <param name="customerId"></param>
        /// <returns>The update result.</returns>
        Task<ConditionStatus> UpdateCondition(int customerId, Guid organizationId, ConditionRequestDto request);

        /// <summary>
        /// Deletes the condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="conditionId">The condition identifier.</param>
        /// <returns>The delete result.</returns>
        Task<ConditionStatus> DeleteCondition(int customerId, Guid conditionId);
    }
}