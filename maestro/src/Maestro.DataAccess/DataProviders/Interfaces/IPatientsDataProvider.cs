using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Domain.Dtos.PatientsService.Calendar;
using Maestro.Domain.Dtos.PatientsService.DefaultSessions;
using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Interfaces
{
    /// <summary>
    /// IPatientsDataProvider.
    /// </summary>
    public interface IPatientsDataProvider
    {
        #region Patients

        /// <summary>
        /// Gets the patients.
        /// </summary>
        /// <param name="bearerToken">The bearer token.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns>Task&lt;IList&lt;PatientDto&gt;&gt;.</returns>
        Task<PagedResult<PatientDto>> GetPatients(
            string bearerToken,
            PatientsSearchDto searchRequest
        );

        /// <summary>
        /// Creates the patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreatePatient(int customerId, CreatePatientRequestDto request, string bearerToken);

        /// <summary>
        /// Gets the patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<PatientDto> GetPatientAsync(int customerId, Guid patientId, bool isBrief, string bearerToken);

        /// <summary>
        /// Gets the patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        PatientDto GetPatient(int customerId, Guid patientId, bool isBrief, string bearerToken);

        /// <summary>
        /// Updates the patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task UpdatePatient(int customerId, UpdatePatientRequestDto request, string bearerToken);

        /// <summary>
        /// Gets the identifiers within customer scope.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<IList<IdentifierDto>> GetIdentifiersWithinCustomerScope(int customerId, string bearerToken);

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
        Task<PagedResult<AdherenceDto>> GetAdherences(
            int customerId, 
            Guid patientId,
            AdherencesSearchDto searchCriteria,
            string bearerToken
        );

        /// <summary>
        /// Gets the calendar items.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<PagedResult<CalendarItemResponseDto>> GetCalendarItems(
            int customerId,
            Guid patientId, 
            CalendarItemsSearchDto searchCriteria, 
            string bearerToken
        );

        /// <summary>
        /// Creates set of calendar items in Calendar API.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IList<CalendarItemResponseDto>> CreateCalendarItems(int customerId, Guid patientId,
            CreateCalendarItemsRequestDto model, string token);

        /// <summary>
        /// Deletes existing calendar item.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemId">The calendar item identifier.</param>
        /// <param name="token">The bearer token.</param>
        /// <returns></returns>
        Task DeleteCalendarItem(int customerId, Guid patientId, Guid calendarItemId, string token);

        /// <summary>
        /// Updates the calendar item.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemId">The calendar item identifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="token">The bearer token.</param>
        /// <returns></returns>
        Task UpdateCalendarItem(int customerId, Guid patientId, Guid calendarItemId, CalendarItemRequestDto model, string token);

        /// <summary>
        /// Creates program record in patient's calendar.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateCalendarProgram(int customerId, Guid patientId,
            CreateCalendarProgramRequestDto model, string token);

        /// <summary>
        /// Deletes existing program and program events.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="programId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteCalendarProgram(int customerId, Guid patientId, Guid programId, string token);

        /// <summary>
        /// Terminates the program.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarProgramId">The calendar program identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task TerminateProgram(
            int customerId,
            Guid patientId, 
            Guid calendarProgramId,
            TerminateProgramRequest request,
            string bearerToken
        );

        /// <summary>
        /// Terminates the calendar item.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemId">The calendar item identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task TerminateCalendarItem(
            int customerId,
            Guid patientId, 
            Guid calendarItemId,
            TerminateProgramRequest request,
            string bearerToken
        );

        /// <summary>
        /// Returns calendar programs matches to search criteria.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<PagedResult<CalendarProgramResponseDto>> GetCalendarPrograms(int customerId, Guid patientId,
            BaseSearchDto searchCriteria, string token);

        /// <summary>
        /// Returns calendar program with specified id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="calendarProgramId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CalendarProgramResponseDto> GetCalendarProgramById(int customerId,
            Guid patientId, Guid calendarProgramId, string token);

        /// <summary>
        /// Returns list of records from calendar history for specified criteria.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="searchDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<PagedResult<CalendarChangeResponseDto>> GetCalendarHistory(int customerId, Guid patientId,
            BaseSearchDto searchDto, string token);
        /// <summary>
        /// Clears the calendar.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="clearCalendarRequestDto">The clear calendar request dto.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task.</returns>
        Task ClearCalendar(object customerId, Guid patientId, ClearCalendarRequestDto clearCalendarRequestDto, string token);

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
        Task<PagedResult<DefaultSessionResponseDto>> GetDefaultSessions(int customerId, Guid patientId,
            BaseSearchDto searchDto, string token);

        /// <summary>
        /// Returns details of default session by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<DefaultSessionResponseDto> GetDefaultSessionById(int customerId, Guid patientId, Guid defaultSessionId,
            string token);

        /// <summary>
        /// Creates new default session for specified patient.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateDefaultSession(int customerId, Guid patientId,
            DefaultSessionDto defaultSessionDto, string token);

        /// <summary>
        /// Updates existing default session.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionId"></param>
        /// <param name="defaultSessionDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateDefaultSession(int customerId, Guid patientId, Guid defaultSessionId,
            DefaultSessionDto defaultSessionDto, string token);

        /// <summary>
        /// Deletes default session.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteDefaultSession(int customerId, Guid patientId, Guid defaultSessionId, string token);

        #endregion
    }
}