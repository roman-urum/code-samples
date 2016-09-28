using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Conditions;
using Maestro.Domain.Dtos.VitalsService.PatientConditions;

using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// VitalsDataProvider.Conditions
    /// </summary>
    public partial class VitalsDataProvider
    {
        /// <summary>
        /// Gets the conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<ConditionResponseDto>> GetConditions(int customerId, string bearerToken)
        {
            string endpointUrl = string.Format("api/{0}/conditions", customerId);

            var result = await apiClient.SendRequestAsync<PagedResult<ConditionResponseDto>>(
                endpointUrl,
                null,
                Method.GET,
                null,
                bearerToken
            );

            return result.Results;
        }

        /// <summary>
        /// Gets the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<ConditionResponseDto>> GetPatientConditions(int customerId, Guid patientId, string bearerToken)
        {
            string endpointUrl = string.Format("api/{0}/patient-conditions/{1}", customerId, patientId);

            var result = await apiClient.SendRequestAsync<List<ConditionResponseDto>>(
                endpointUrl,
                null,
                Method.GET,
                null,
                bearerToken
            );

            return result;
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
            string endpointUrl = string.Format("api/{0}/patient-conditions/{1}", customerId, patientId);

            return apiClient.SendRequestAsync(endpointUrl, request, Method.POST, null, bearerToken);
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
            string endpointUrl = string.Format("api/{0}/conditions", customerId);

            return apiClient.SendRequestAsync<PostResponseDto<Guid>>(
                endpointUrl,
                request,
                Method.POST,
                null,
                bearerToken
            );
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
            string endpointUrl = string.Format("api/{0}/conditions/{1}", customerId, conditionId);

            return apiClient.SendRequestAsync(
                endpointUrl,
                request,
                Method.PUT,
                null,
                bearerToken
            );
        }
    }
}