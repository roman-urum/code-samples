using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Conditions;
using Maestro.Domain.Dtos.VitalsService.PatientConditions;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IVitalsService.Conditions.
    /// </summary>
    public partial interface IVitalsService
    {
        /// <summary>
        /// Gets the conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<IList<ConditionResponseDto>> GetConditions(
            int customerId,
            string bearerToken
        );

        /// <summary>
        /// Gets the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<IList<ConditionResponseDto>> GetPatientConditions(
            int customerId,
            Guid patientId,
            string bearerToken
        );

        /// <summary>
        /// Creates the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task CreatePatientConditions(int customerId, Guid patientId, string bearerToken, PatientConditionsRequestDto request);

        /// <summary>
        /// Creates the customer condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateCustomerCondition(
            int customerId,
            ConditionRequestDto request, 
            string bearerToken
        );

        /// <summary>
        /// Updates the customer condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="conditionId">The condition identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task UpdateCustomerCondition(
            int customerId, 
            Guid conditionId, 
            ConditionRequestDto request,
            string bearerToken
        );
    }
}