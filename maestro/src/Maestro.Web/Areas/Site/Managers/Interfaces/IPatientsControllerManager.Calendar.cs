using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.PatientsService.Calendar;
using Maestro.Web.Areas.Site.Models.Patients.Calendar;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IPatientsControllerManager.CareManagers
    /// </summary>
    public partial interface IPatientsControllerManager
    {
        /// <summary>
        /// Returns list of adherences and programs from Calendar API.
        /// </summary>
        /// <returns></returns>
        Task<GetAdherencesAndProgramsViewModel> GetAdherencesAndPrograms(Guid patientId, AdherencesSearchDto searchCriteria);

        /// <summary>
        /// Generates event in calendar.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemRequest">The calendar item model.</param>
        /// <returns></returns>
        Task<CalendarItemViewModel> CreateCalendarItem(Guid patientId, CalendarItemViewModel calendarItemRequest);

        /// <summary>
        /// Deletes existing calendar item.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemId">The calendar item identifier.</param>
        /// <returns></returns>
        Task DeleteCalendarItem(Guid patientId, Guid calendarItemId);

        /// <summary>
        /// Updates the calendar item.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemId">The calendar item identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task UpdateCalendarItem(Guid patientId, Guid calendarItemId, CalendarItemViewModel model);

        /// <summary>
        /// Generates schedule with program in patient calendar.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task CreateCalendarProgram(Guid patientId, ScheduleCalendarProgramViewModel model);

        /// <summary>
        /// Generates program schedule and creates events in calendar.
        /// </summary>
        /// <returns></returns>
        Task UpdateCalendarProgram(Guid patientId, Guid calendarProgramId, ScheduleCalendarProgramViewModel model);

        /// <summary>
        /// Terminates the program.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarProgramId">The calendar program identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task TerminateProgram(Guid patientId, Guid calendarProgramId, TerminateProgramRequest model);
        
        /// <summary>
        /// Terminates the calendar item.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemId">The calendar item identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task TerminateCalendarItem(Guid patientId, Guid calendarItemId, TerminateProgramRequest model);

        /// <summary>
        /// Returns calendar program using program id.
        /// Returns null it program with match correlator is not found.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="calendarProgramId"></param>
        /// <returns></returns>
        Task<CalendarProgramViewModel> GetCalendarProgramById(Guid patientId, Guid calendarProgramId);

        /// <summary>
        /// Returns list of records with history of calendar changes.
        /// </summary>
        /// <returns></returns>
        Task<IList<CalendarChangeViewModel>> GetCalendarHistory(Guid patientId, BaseSearchDto searchDto);

        /// <summary>
        /// Deletes calendar program
        /// </summary>
        /// <param name="patientId">The patient identifier</param>
        /// <param name="calendarProgramId">The calendar program identifier</param>
        /// <returns></returns>
        Task DeleteCalendarProgram(Guid patientId, Guid calendarProgramId);
    }
}