using System;
using System.Threading.Tasks;

using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IConditionService.
    /// </summary>
    public interface IConditionsService
    {
        /// <summary>
        /// Gets the condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="conditionId">The condition identifier</param>
        /// <returns>Condition entity or null</returns>
        Task<Condition> GetCondition(int customerId, Guid conditionId);

        /// <summary>
        /// Get the list of conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The search request.</param>
        /// <returns>The list of conditions.</returns>
        Task<PagedResult<Condition>> GetConditions(int customerId, ConditionSearchDto request);

        /// <summary>
        /// Creates condition.
        /// </summary>
        /// <param name="request">The condition entity.</param>
        /// <returns>The creation result.</returns>
        Task<OperationResultDto<Guid, ConditionStatus>> CreateCondition(Condition request);

        /// <summary>
        /// Updates the condition.
        /// </summary>
        /// <param name="request">The condition entity.</param>
        /// <returns>The update result.</returns>
        Task<ConditionStatus> UpdateCondition(Condition request);

        /// <summary>
        /// Deletes the condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="conditionId">The condition identifier.</param>
        /// <returns>The delete result.</returns>
        Task<ConditionStatus> DeleteCondition(int customerId, Guid conditionId);
    }
}