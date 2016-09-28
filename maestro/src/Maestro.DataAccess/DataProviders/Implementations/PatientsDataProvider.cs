using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Common;
using Maestro.DataAccess.Api.ApiClient;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Domain.Dtos.PatientsService.Calendar;
using Maestro.Domain.Dtos.PatientsService.DefaultSessions;
using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// PatientsDataProvider.
    /// </summary>
    public class PatientsDataProvider : IPatientsDataProvider
    {
        private readonly IRestApiClient apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientsDataProvider" /> class.
        /// </summary>
        /// <param name="apiClientFactory">The API client factory.</param>
        public PatientsDataProvider(
            IRestApiClientFactory apiClientFactory
        )
        {
            this.apiClient = apiClientFactory.Create(Settings.PatientServiceUrl);
        }

        #region Patients

        /// <summary>
        /// Creates the patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreatePatient(
            int customerId,
            CreatePatientRequestDto request,
            string bearerToken
        )
        {
            string endpointUrl = string.Format("api/{0}", customerId);

            var response = await apiClient.SendRequestAsync<PostResponseDto<Guid>>(
                endpointUrl,
                request,
                Method.POST,
                null,
                bearerToken
            );

            return response;
        }

        /// <summary>
        /// Updates the patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task UpdatePatient(int customerId, UpdatePatientRequestDto request, string bearerToken)
        {
            string endpointUrl = string.Format("api/{0}/{1}", customerId, request.Id);

            await apiClient.SendRequestAsync(endpointUrl, request, Method.PUT, null, bearerToken);
        }

        /// <summary>
        /// Gets the identifiers within customer scope.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<IdentifierDto>> GetIdentifiersWithinCustomerScope(int customerId, string bearerToken)
        {
            string endpointUrl = string.Format("api/{0}/identifiers", customerId);
            var getIdentifiersResult = await this.apiClient.SendRequestAsync<PagedResult<IdentifierDto>>(endpointUrl, null, Method.GET, null, bearerToken);

            return getIdentifiersResult.Results;
        }

        /// <summary>
        /// Gets the patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<PatientDto> GetPatientAsync(int customerId, Guid patientId, bool isBrief, string bearerToken)
        {
            string endpointUrl = string.Format("api/{0}/{1}?isBrief={2}", customerId, patientId, isBrief);

            return await this.apiClient.SendRequestAsync<PatientDto>(endpointUrl, null, Method.GET, null, bearerToken);
        }

        /// <summary>
        /// Gets the patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public PatientDto GetPatient(int customerId, Guid patientId, bool isBrief, string bearerToken)
        {
            string endpointUrl = string.Format("api/{0}/{1}?isBrief={2}", customerId, patientId, isBrief);

            return apiClient.SendRequest<PatientDto>(endpointUrl, null, Method.GET, null, bearerToken);
        }

        /// <summary>
        /// Returns search results with patients list from Patients service for specified criteria.
        /// </summary>
        /// <param name="bearerToken"></param>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        public async Task<PagedResult<PatientDto>> GetPatients(string bearerToken, PatientsSearchDto searchRequest)
        {
            string endpointUrl = string.Format("api/{0}", searchRequest.CustomerId);

            var response = await this.apiClient.SendRequestAsync<PagedResult<PatientDto>>(
                endpointUrl, searchRequest, Method.GET, null, bearerToken);

            return response;
        }

        #endregion

        #region Calendar 

        /// <summary>
        /// Returns list of adherences from Calendar API.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<PagedResult<AdherenceDto>> GetAdherences(
            int customerId,
            Guid patientId,
            AdherencesSearchDto searchCriteria,
            string bearerToken
        )
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/adherences", customerId, patientId);

            return await
                this.apiClient.SendRequestAsync<PagedResult<AdherenceDto>>(endpointUrl, searchCriteria,
                    Method.GET, null, bearerToken);
        }

        /// <summary>
        /// Gets the calendar items.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<PagedResult<CalendarItemResponseDto>> GetCalendarItems(
            int customerId,
            Guid patientId,
            CalendarItemsSearchDto searchCriteria,
            string bearerToken
        )
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/calendar", customerId, patientId);

            return await apiClient.SendRequestAsync<PagedResult<CalendarItemResponseDto>>(
                endpointUrl,
                searchCriteria,
                Method.GET,
                null,
                bearerToken
            );
        }

        /// <summary>
        /// Creates set of calendar items in Calendar API.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IList<CalendarItemResponseDto>> CreateCalendarItems(
            int customerId,
            Guid patientId,
            CreateCalendarItemsRequestDto model,
            string token
        )
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/calendar", customerId, patientId);

            return await this.apiClient.SendRequestAsync<List<CalendarItemResponseDto>>(
                endpointUrl, model, Method.POST, null, token);
        }


        /// <summary>
        /// Deletes existing calendar item.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemId">The calendar item identifier.</param>
        /// <param name="token">The bearer token.</param>
        /// <returns></returns>
        public async Task DeleteCalendarItem(int customerId, Guid patientId, Guid calendarItemId, string token)
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/calendar/{2}", customerId, patientId, calendarItemId);

            await this.apiClient.SendRequestAsync(endpointUrl, null, Method.DELETE, null, token);
        }

        /// <summary>
        /// Updates the calendar item.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemId">The calendar item identifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="token">The bearer token.</param>
        /// <returns></returns>
        public async Task UpdateCalendarItem(
            int customerId,
            Guid patientId,
            Guid calendarItemId,
            CalendarItemRequestDto model,
            string token)
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/calendar/{2}", customerId, patientId, calendarItemId);

            await this.apiClient.SendRequestAsync(endpointUrl, model, Method.PUT, null, token);
        }

        /// <summary>
        /// Creates program record in patient's calendar.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateCalendarProgram(
            int customerId,
            Guid patientId,
            CreateCalendarProgramRequestDto model,
            string token
        )
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/programs", customerId, patientId);

            return await
                this.apiClient.SendRequestAsync<PostResponseDto<Guid>>(endpointUrl, model, Method.POST, null, token);
        }

        /// <summary>
        /// Deletes existing program and program events.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="programId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteCalendarProgram(int customerId, Guid patientId, Guid programId, string token)
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/programs/{2}", customerId, patientId, programId);

            await this.apiClient.SendRequestAsync(endpointUrl, null, Method.DELETE, null, token);
        }

        /// <summary>
        /// Returns calendar programs matches to search criteria.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PagedResult<CalendarProgramResponseDto>> GetCalendarPrograms(int customerId, Guid patientId,
            BaseSearchDto searchCriteria, string token)
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/programs", customerId, patientId);

            return await this.apiClient.SendRequestAsync<PagedResult<CalendarProgramResponseDto>>(endpointUrl,
                searchCriteria, Method.GET, null, token);
        }

        /// <summary>
        /// Returns calendar program with specified id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="calendarProgramId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<CalendarProgramResponseDto> GetCalendarProgramById(int customerId,
            Guid patientId, Guid calendarProgramId, string token)
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/programs/{2}", customerId, patientId,
                calendarProgramId);

            return await
                this.apiClient.SendRequestAsync<CalendarProgramResponseDto>(endpointUrl, null,
                    Method.GET, null, token);
        }

        /// <summary>
        /// Terminates the program.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarProgramId">The calendar program identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task TerminateProgram(
            int customerId,
            Guid patientId,
            Guid calendarProgramId,
            TerminateProgramRequest request,
            string bearerToken
        )
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/programs/{2}/terminate", customerId, patientId, calendarProgramId);

            return apiClient.SendRequestAsync(endpointUrl, request, Method.POST, null, bearerToken);
        }

        /// <summary>
        /// Terminates the calendar item.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemId">The calendar item identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task TerminateCalendarItem(
            int customerId,
            Guid patientId,
            Guid calendarItemId,
            TerminateProgramRequest request,
            string bearerToken
        )
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/calendar/{2}/terminate", customerId, patientId, calendarItemId);

            return apiClient.SendRequestAsync(endpointUrl, request, Method.POST, null, bearerToken);
        }

        /// <summary>
        /// Returns list of records from calendar history for specified criteria.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="searchDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PagedResult<CalendarChangeResponseDto>> GetCalendarHistory(int customerId, Guid patientId,
            BaseSearchDto searchDto, string token)
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/history", customerId, patientId);

            return await apiClient.SendRequestAsync<PagedResult<CalendarChangeResponseDto>>(
                endpointUrl, searchDto, Method.GET, null, token);
        }

        /// <summary>
        /// Clears the calendar.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="clearCalendarRequestDto">The clear calendar request dto.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ClearCalendar(object customerId, Guid patientId, ClearCalendarRequestDto clearCalendarRequestDto, string token)
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/calendar/clear", customerId, patientId);

            await apiClient.SendRequestAsync(
                endpointUrl, clearCalendarRequestDto, Method.POST, null, token);
        }

        #endregion

        #region Default Sessions

        /// <summary>
        /// Returns list of all default sessions for specified patient.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="searchDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PagedResult<DefaultSessionResponseDto>> GetDefaultSessions(int customerId, Guid patientId, BaseSearchDto searchDto, string token)
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/default-sessions", customerId, patientId);

            return await
                this.apiClient.SendRequestAsync<PagedResult<DefaultSessionResponseDto>>(endpointUrl, searchDto,
                    Method.GET, null, token);
        }

        /// <summary>
        /// Returns details of default session by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<DefaultSessionResponseDto> GetDefaultSessionById(int customerId, Guid patientId, Guid defaultSessionId, string token)
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/default-sessions/{2}", customerId, patientId, defaultSessionId);

            return await
                this.apiClient.SendRequestAsync<DefaultSessionResponseDto>(endpointUrl, null,
                    Method.GET, null, token);
        }

        /// <summary>
        /// Creates new default session for specified patient.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateDefaultSession(int customerId, Guid patientId, DefaultSessionDto defaultSessionDto, string token)
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/default-sessions", customerId, patientId);

            return await
                this.apiClient.SendRequestAsync<PostResponseDto<Guid>>(endpointUrl, defaultSessionDto,
                    Method.POST, null, token);
        }

        /// <summary>
        /// Updates existing default session.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionId"></param>
        /// <param name="defaultSessionDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdateDefaultSession(int customerId, Guid patientId,
            Guid defaultSessionId, DefaultSessionDto defaultSessionDto, string token)
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/default-sessions/{2}", customerId, patientId,
                defaultSessionId);

            await this.apiClient.SendRequestAsync(endpointUrl, defaultSessionDto, Method.PUT, null, token);
        }

        /// <summary>
        /// Deletes default session.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteDefaultSession(int customerId, Guid patientId, Guid defaultSessionId, string token)
        {
            string endpointUrl = string.Format("/api/{0}/patient/{1}/default-sessions/{2}", customerId, patientId,
                defaultSessionId);

            await this.apiClient.SendRequestAsync(endpointUrl, null, Method.DELETE, null, token);
        }

        #endregion
    }
}