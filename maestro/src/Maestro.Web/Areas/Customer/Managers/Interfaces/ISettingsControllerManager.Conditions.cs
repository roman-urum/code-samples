using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Conditions;
using Maestro.Web.Areas.Customer.Models.CareBuilder.Tags;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    /// <summary>
    /// ISettingsControllerManager.Conditions
    /// </summary>
    public partial interface ISettingsControllerManager
    {
        /// <summary>
        /// Gets the customer conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<IList<ConditionResponseDto>> GetCustomerConditions(int customerId);

        /// <summary>
        /// Creates the customer condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateCustomerCondition(int customerId, ConditionRequestDto request);

        /// <summary>
        /// Updates the customer condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="conditionId">The condition identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task UpdateCustomerCondition(int customerId, Guid conditionId, ConditionRequestDto request);

        /// <summary>
        /// Returns list of tags to display in tags cloud for conditions.
        /// Rate = assigned programs + assigned protocols.
        /// </summary>
        /// <returns></returns>
        Task<IList<CloudTagViewModel>> GetConditionsTags();
    }
}