using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Domain.Dtos.PatientsService.Calendar;
using Maestro.Domain.Dtos.PatientsService.DefaultSessions;
using Maestro.DomainLogic.Services.Interfaces;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Contains business logic methods to manage patients.
    /// </summary>
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsDataProvider patientsDataProvider;
        private readonly IHealthLibraryDataProvider healthLibraryDataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientsService" /> class.
        /// </summary>
        /// <param name="patientsDataProvider">The patients data provider.</param>
        public PatientsService(IPatientsDataProvider patientsDataProvider, IHealthLibraryDataProvider healthLibraryDataProvider)
        {
            this.patientsDataProvider = patientsDataProvider;
            this.healthLibraryDataProvider = healthLibraryDataProvider;
        }

        #region Patients

        public async Task<PagedResult<PatientDto>> GetPatients(string bearerToken, PatientsSearchDto searchRequest)
        {
            return await this.patientsDataProvider.GetPatients(bearerToken, searchRequest);
        }

        /// <summary>
        /// Creates the patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreatePatient(
            int customerId,
            CreatePatientRequestDto request,
            string token
        )
        {
            var createpatientResult = await patientsDataProvider.CreatePatient(customerId, request, token);

            return createpatientResult;
        }

        /// <summary>
        /// Gets the patient asynchronous.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<PatientDto> GetPatientAsync(int customerId, Guid patientId, bool isBrief, string bearerToken)
        {
            return await this.patientsDataProvider.GetPatientAsync(customerId, patientId, isBrief, bearerToken);
        }

        /// <summary>
        /// Gets the patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public PatientDto GetPatient(int customerId, Guid patientId, bool isBrief, string bearerToken)
        {
            return patientsDataProvider.GetPatient(customerId, patientId, isBrief, bearerToken);
        }

        /// <summary>
        /// Gets the identifiers within customer scope.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<IdentifierDto>> GetIdentifiersWithinCustomerScope(int customerId, string bearerToken)
        {
            return await patientsDataProvider.GetIdentifiersWithinCustomerScope(customerId, bearerToken);
        }

        /// <summary>
        /// Updates the patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task UpdatePatient(int customerId, UpdatePatientRequestDto request, string token)
        {
            await patientsDataProvider.UpdatePatient(customerId, request, token);
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
            return await this.patientsDataProvider.GetAdherences(customerId, patientId, searchCriteria, bearerToken);
        }

        /// <summary>
        /// Gets the calendar items.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<PagedResult<CalendarItemResponseDto>> GetCalendarItems(
            int customerId,
            Guid patientId, 
            CalendarItemsSearchDto searchCriteria, 
            string bearerToken
        )
        {
            return patientsDataProvider.GetCalendarItems(customerId, patientId, searchCriteria, bearerToken);
        }

        /// <summary>
        /// Generates events in calendar.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="model">The create events model.</param>
        /// <param name="token">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<CalendarItemResponseDto>> CreateCalendarItems(
            int customerId,
            Guid patientId,
            CreateCalendarItemsRequestDto model,
            string token)
        {
            return await patientsDataProvider.CreateCalendarItems(customerId, patientId, model, token);
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
            await patientsDataProvider.DeleteCalendarItem(customerId, patientId, calendarItemId, token);
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
            await patientsDataProvider.UpdateCalendarItem(customerId, patientId, calendarItemId, model, token);
        }

        /// <summary>
        /// Generates program schedule and creates events in calendar.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="programId"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task CreateCalendarProgram(int customerId, Guid patientId, Guid programId,
            ProgramScheduleRequestDto searchCriteria, string token)
        {
            var programRequestDto =
                await this.healthLibraryDataProvider.GetProgramSchedule(customerId, programId, searchCriteria, token);

            await this.patientsDataProvider.CreateCalendarProgram(customerId, patientId, programRequestDto, token);
        }

        /// <summary>
        /// Generates program schedule and creates events in calendar.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="programId"></param>
        /// <param name="calendarProgramId"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdateCalendarProgram(int customerId, Guid patientId, Guid programId, Guid calendarProgramId,
            ProgramScheduleRequestDto searchCriteria, string token)
        {
            await this.patientsDataProvider.DeleteCalendarProgram(
                customerId, patientId, calendarProgramId, token);
            await this.CreateCalendarProgram(customerId, patientId, programId, searchCriteria, token);
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
            return patientsDataProvider.TerminateProgram(customerId, patientId, calendarProgramId, request, bearerToken);
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
            return patientsDataProvider.TerminateCalendarItem(customerId, patientId, calendarItemId, request, bearerToken);
        }

        /// <summary>
        /// Returns calendar program by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PagedResult<CalendarProgramResponseDto>> GetCalendarPrograms(int customerId, Guid patientId,
            BaseSearchDto searchCriteria, string token)
        {
            return await this.patientsDataProvider.GetCalendarPrograms(customerId, patientId, searchCriteria, token);
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
            return
                await this.patientsDataProvider.GetCalendarProgramById(customerId, patientId, calendarProgramId, token);
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
            return await this.patientsDataProvider.GetCalendarHistory(customerId, patientId, searchDto, token);
        }

        /// <summary>
        /// Deletes the calendar program.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarProgramId">The calendar program identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task DeleteCalendarProgram(int customerId, Guid patientId, Guid calendarProgramId, string token)
        {
            await patientsDataProvider.DeleteCalendarProgram(customerId, patientId, calendarProgramId, token);
        }

        /// <summary>
        /// Clears the calendar.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="clearCalendarRequestDto">The clear calendar request dto.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task ClearCalendar(object customerId, Guid patientId, ClearCalendarRequestDto clearCalendarRequestDto, string token)
        {
            await patientsDataProvider.ClearCalendar(customerId, patientId, clearCalendarRequestDto, token);
        }

        /// <summary>
        /// Deletes calendar program
        /// </summary>
        /// <param name="customerId">The customer identifier</param>
        /// <param name="patientId">The patient identifier</param>
        /// <param name="calendarProgramId">The calendar program identifier</param>
        /// <param name="token">The authorization token</param>
        /// <returns></returns>
        public async Task DeleteProgram(int customerId, Guid patientId, Guid calendarProgramId, string token)
        {
            await patientsDataProvider.DeleteCalendarProgram(customerId, patientId, calendarProgramId, token);
        }

        #endregion

        #region Default Sessions

        /// <summary>
        /// Removes existing default sessions and creates new.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateDefaultSession(int customerId, Guid patientId, DefaultSessionDto defaultSessionDto, string token)
        {
            var existingDefaultSessions =
                await this.patientsDataProvider.GetDefaultSessions(customerId, patientId, null, token);

            foreach (var session in existingDefaultSessions.Results)
            {
                await this.patientsDataProvider.DeleteDefaultSession(customerId, patientId, session.Id, token);
            }

            return await this.patientsDataProvider.CreateDefaultSession(customerId, patientId, defaultSessionDto, token);
        }

        /// <summary>
        /// Returns default health session for the patient.
        /// Returns first session from list if patient has a lot of default sessions.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<DefaultSessionResponseDto> GetDefaultSession(int customerId, Guid patientId, string token)
        {
            var existingDefaultSessions = await this.patientsDataProvider.GetDefaultSessions(customerId, patientId, null, token);

            return existingDefaultSessions.Results.FirstOrDefault();
        }

        /// <summary>
        /// Updates existing session.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="defaultSessionId"></param>
        /// <param name="defaultSessionDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdateDefaultSession(int customerId, Guid patientId, Guid defaultSessionId,
            DefaultSessionDto defaultSessionDto, string token)
        {
            await
                this.patientsDataProvider.UpdateDefaultSession(customerId, patientId, defaultSessionId,
                    defaultSessionDto, token);
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
            await this.patientsDataProvider.DeleteDefaultSession(customerId, patientId, defaultSessionId, token);
        }

        #endregion
    }
}