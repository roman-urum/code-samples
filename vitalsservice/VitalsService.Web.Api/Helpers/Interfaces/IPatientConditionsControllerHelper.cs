using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models.Conditions;
using VitalsService.Web.Api.Models.PatientConditions;

namespace VitalsService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IPatientConditionsControllerHelper.
    /// </summary>
    public interface IPatientConditionsControllerHelper
    {
        /// <summary>
        /// Creates the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<CreateUpdatePatientConditionsStatus> CreatePatientConditions(
            int customerId,
            Guid patientId,
            PatientConditionsRequest request
        );

        /// <summary>
        /// Gets the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<IList<ConditionResponseDto>> GetPatientConditions(int customerId, Guid patientId);
    }
}