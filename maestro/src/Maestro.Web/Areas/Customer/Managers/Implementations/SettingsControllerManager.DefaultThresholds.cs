using System;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Thresholds;
using Maestro.Web.Areas.Customer.Models.Settings.DefaultThresholds;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    /// <summary>
    /// SettingsControllerManager.DefaultThresholds
    /// </summary>
    /// <seealso cref="Maestro.Web.Areas.Customer.Managers.Interfaces.ISettingsControllerManager" />
    public partial class SettingsControllerManager
    {
        /// <summary>
        /// Gets customer's default thresholds.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<CustomerDefaultThresholdsViewModel> GetDefaultThresholds(DefaultThresholdsSearchDto request)
        {
            var token = authDataStorage.GetToken();
            var customerId = CustomerContext.Current.Customer.Id;

            var customerAlertSeveritiesTask = vitalsService.GetAlertSeverities(customerId, token);
            var customerDefaultThresholdsTask = vitalsService.GetDefaultThresholds(customerId, request, token);

            await Task.WhenAll(customerDefaultThresholdsTask, customerAlertSeveritiesTask);

            return new CustomerDefaultThresholdsViewModel()
            {
                AlertSeverities = customerAlertSeveritiesTask.Result,
                DefaultThresholds = customerDefaultThresholdsTask.Result
            };
        }

        /// <summary>
        /// Creates customer's default threshold.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task<PostResponseDto<Guid>> CreateDefaultThreshold(CreateDefaultThresholdRequestDto request)
        {
            var token = authDataStorage.GetToken();

            return vitalsService.CreateDefaultThreshold(CustomerContext.Current.Customer.Id, request, token);
        }

        /// <summary>
        /// Updates customer's default threshold.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task UpdateDefaultThreshold(UpdateDefaultThresholdRequestDto request)
        {
            var token = authDataStorage.GetToken();

            return vitalsService.UpdateDefaultThreshold(CustomerContext.Current.Customer.Id, request, token);
        }

        /// <summary>
        /// Deletes customer's default threshold.
        /// </summary>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <returns></returns>
        public Task DeleteDefaultThreshold(Guid defaultThresholdId)
        {
            var token = authDataStorage.GetToken();

            return vitalsService.DeleteDefaultThreshold(CustomerContext.Current.Customer.Id, defaultThresholdId, token);
        }
    }
}