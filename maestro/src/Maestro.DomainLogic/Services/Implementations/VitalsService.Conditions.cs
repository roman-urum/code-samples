using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Conditions;
using Maestro.Domain.Dtos.VitalsService.PatientConditions;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// VitalsService.Conditions
    /// </summary>
    public partial class VitalsService
    {
        /// <summary>
        /// Gets the conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<IList<ConditionResponseDto>> GetConditions(int customerId, string bearerToken)
        {
            return vitalsDataProvider.GetConditions(customerId, bearerToken);
        }

        /// <summary>
        /// Gets the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<IList<ConditionResponseDto>> GetPatientConditions(int customerId, Guid patientId, string bearerToken)
        {
            return vitalsDataProvider.GetPatientConditions(customerId, patientId, bearerToken);
        }

        /// <summary>
        /// Creates the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task CreatePatientConditions(
            int customerId,
            Guid patientId,
            string bearerToken,
            PatientConditionsRequestDto request)
        {
            return vitalsDataProvider.CreatePatientConditions(customerId, patientId, bearerToken, request);
        }

        /// <summary>
        /// Creates the customer condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<PostResponseDto<Guid>> CreateCustomerCondition(
            int customerId, 
            ConditionRequestDto request, 
            string bearerToken
        )
        {
            return vitalsDataProvider.CreateCustomerCondition(customerId, request, bearerToken);
        }

        /// <summary>
        /// Updates the customer condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="conditionId">The condition identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task UpdateCustomerCondition(
            int customerId,
            Guid conditionId, 
            ConditionRequestDto request, 
            string bearerToken
        )
        {
            return vitalsDataProvider.UpdateCustomerCondition(customerId, conditionId, request, bearerToken);
        }
    }
}