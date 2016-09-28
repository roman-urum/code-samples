using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maestro.Common.Helpers;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.HealthLibraryService;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;
using Maestro.Domain.Dtos.VitalsService.Conditions;
using Maestro.Web.Areas.Customer.Models.CareBuilder.Tags;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    /// <summary>
    /// SettingsControllerManager.Conditions
    /// </summary>
    /// <seealso cref="Maestro.Web.Areas.Customer.Managers.Interfaces.ISettingsControllerManager" />
    public partial class SettingsControllerManager
    {
        #region Implementation of ISettingsControllerManager

        /// <summary>
        /// Gets the customer conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<IList<ConditionResponseDto>> GetCustomerConditions(int customerId)
        {
            var conditions = await vitalsService.GetConditions(customerId, authDataStorage.GetToken());

            return conditions
                .OrderBy(c => c.Name, new NaturalSortComparer())
                .ToList();
        }

        /// <summary>
        /// Creates the customer condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task<PostResponseDto<Guid>> CreateCustomerCondition(int customerId, ConditionRequestDto request)
        {
            return vitalsService.CreateCustomerCondition(customerId, request, authDataStorage.GetToken());
        }

        /// <summary>
        /// Updates the customer condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="conditionId">The condition identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task UpdateCustomerCondition(int customerId, Guid conditionId, ConditionRequestDto request)
        {
            return vitalsService.UpdateCustomerCondition(customerId, conditionId, request, authDataStorage.GetToken());
        }

        /// <summary>
        /// Returns list of tags to display in tags cloud for conditions.
        /// Rate = assigned programs + assigned protocols.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CloudTagViewModel>> GetConditionsTags()
        {
            var token = this.authDataStorage.GetToken();
            var customerId = this.customerContext.Customer.Id;
            var careElementFilter = new GlobalSearchDto
            {
                Categories = new List<SearchCategoryType>
                {
                    SearchCategoryType.Program,
                    SearchCategoryType.Protocol
                }
            };
            var careElements = await this.healthLibraryService.GlobalSearch(token, customerId, careElementFilter);
            var tags = careElements.SelectMany(e => e.Tags).Distinct();

            return tags.Select(tag => new CloudTagViewModel
            {
                Name = tag,
                Rate = careElements.Count(e => e.Tags.Contains(tag))
            })
                .Where(m => m.Rate > 0)
                .ToList();
        }

        #endregion
    }
}