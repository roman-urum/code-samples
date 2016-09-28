using System;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Domain.Dtos.VitalsService.Thresholds;
using Maestro.Web.Areas.Site.Models.Patients;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// PatientsControllerManager.Thresholds
    /// </summary>
    public partial class PatientsControllerManager
    {
        /// <summary>
        /// Gets the thresholds.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<PatientThresholdsViewModel> GetThresholds(Guid patientId)
        {
            var token = authDataStorage.GetToken();

            var result = new PatientThresholdsViewModel()
            {
                CustomerId = CustomerContext.Current.Customer.Id,
                PatientId = patientId
            };

            var patientThresholds = vitalsService.GetThresholds(
                CustomerContext.Current.Customer.Id,
                patientId,
                ThresholdSearchType.All, 
                token
            );

            var customerAlertSeverities = vitalsService.GetAlertSeverities(
                CustomerContext.Current.Customer.Id,
                token
            );

            var patientConditions = vitalsService.GetPatientConditions(
                CustomerContext.Current.Customer.Id,
                patientId,
                token
            );

            await Task.WhenAll(patientThresholds, customerAlertSeverities, patientConditions);

            result.Thresholds = patientThresholds.Result;
            result.AlertSeverities = customerAlertSeverities.Result;
            result.PatientConditions = patientConditions.Result;

            return result;
        }

        /// <summary>
        /// Creates the threshold.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateThreshold(CreateThresholdRequestDto request)
        {
            var token = authDataStorage.GetToken();

            return await vitalsService.CreateThreshold(CustomerContext.Current.Customer.Id, request, token);
        }

        /// <summary>
        /// Updates the threshold.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task UpdateThreshold(UpdateThresholdRequestDto request)
        {
            var token = authDataStorage.GetToken();

            return vitalsService.UpdateThreshold(CustomerContext.Current.Customer.Id, request, token);
        }

        /// <summary>
        /// Deletes the threshold.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <returns></returns>
        public Task DeleteThreshold(Guid patientId, Guid thresholdId)
        {
            var token = authDataStorage.GetToken();

            return vitalsService.DeleteThreshold(CustomerContext.Current.Customer.Id, patientId, thresholdId, token);
        }
    }
}