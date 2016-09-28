using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.PatientsService.Calendar;
using Maestro.Web.Areas.Site.Models.Patients.Calendar;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// PatientsControllerManager.Devices
    /// </summary>
    public partial class PatientsControllerManager
    {


        public async Task<GetAdherencesAndProgramsViewModel> GetAdherencesAndPrograms(Guid patientId, AdherencesSearchDto searchCriteria)
        {
            var getAdherencesTask = patientsService.GetAdherences(
                CustomerContext.Current.Customer.Id,
                patientId,
                searchCriteria,
                authDataStorage.GetToken()
            );

            var getCalendarProgramsTask = patientsService.GetCalendarPrograms(
                CustomerContext.Current.Customer.Id,
                patientId,
                new BaseSearchDto()
                {
                    Skip = 0,
                    Take = int.MaxValue
                }, 
                authDataStorage.GetToken()
            );

            await Task.WhenAll(getAdherencesTask, getCalendarProgramsTask);

            var targetCalendarPrograms = getCalendarProgramsTask.Result.Results.Where(p => getAdherencesTask.Result.Results.Any(a => a.CalendarEvent.CalendarProgramId == p.Id));
            
            return new GetAdherencesAndProgramsViewModel()
            {
                Adherences = Mapper.Map<List<AdherenceViewModel>>(getAdherencesTask.Result.Results),
                Programs = Mapper.Map<List<CalendarProgramViewModel>>(targetCalendarPrograms)
            };
        }

        /// <summary>
        /// Generates event in calendar.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemRequest">The calendar item model.</param>
        /// <returns></returns>
        public async Task<CalendarItemViewModel> CreateCalendarItem(
            Guid patientId,
            CalendarItemViewModel calendarItemRequest)
        {
            var model = new CreateCalendarItemsRequestDto()
            {
                CalendarEvents = Mapper.Map<List<CalendarItemRequestDto>>(new List<CalendarItemViewModel> () { calendarItemRequest })
            };

            var calendarItemResponses = await patientsService.CreateCalendarItems(
                CustomerContext.Current.Customer.Id,
                patientId,
                model,
                authDataStorage.GetToken());
            
            return Mapper.Map<CalendarItemViewModel>(calendarItemResponses.FirstOrDefault());
        }

        /// <summary>
        /// Deletes existing calendar item.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemId">The calendar item identifier.</param>
        /// <returns></returns>
        public async Task DeleteCalendarItem(Guid patientId, Guid calendarItemId)
        {
            await patientsService.DeleteCalendarItem(
                CustomerContext.Current.Customer.Id,
                patientId,
                calendarItemId,
                authDataStorage.GetToken());
        }

        /// <summary>
        /// Updates the calendar item.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemId">The calendar item identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task UpdateCalendarItem(Guid patientId, Guid calendarItemId, CalendarItemViewModel model)
        {
            var dto = Mapper.Map<CalendarItemRequestDto>(model);

            await patientsService.UpdateCalendarItem(
                CustomerContext.Current.Customer.Id,
                patientId,
                calendarItemId,
                dto,
                authDataStorage.GetToken());
        }

        /// <summary>
        /// Generates schedule with program in patient calendar.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateCalendarProgram(Guid patientId, ScheduleCalendarProgramViewModel model)
        {
            var programScheduleModel = Mapper.Map<ProgramScheduleRequestDto>(model);
            var authData = authDataStorage.GetUserAuthData();

            await patientsService.CreateCalendarProgram(
                CustomerContext.Current.Customer.Id,
                patientId,
                model.ProgramId,
                programScheduleModel,
                authData.Token
            );
        }

        /// <summary>
        /// Generates program schedule and creates events in calendar.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateCalendarProgram(Guid patientId, Guid calendarProgramId, ScheduleCalendarProgramViewModel model)
        {
            var programScheduleModel = Mapper.Map<ProgramScheduleRequestDto>(model);
            var authData = authDataStorage.GetUserAuthData();

            await patientsService.UpdateCalendarProgram(
                CustomerContext.Current.Customer.Id, 
                patientId,
                model.ProgramId, 
                calendarProgramId, 
                programScheduleModel, 
                authData.Token
            );
        }

        /// <summary>
        /// Terminates the program.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarProgramId">The calendar program identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Task TerminateProgram(Guid patientId, Guid calendarProgramId, TerminateProgramRequest model)
        {
            return patientsService.TerminateProgram(
                CustomerContext.Current.Customer.Id,
                patientId,
                calendarProgramId,
                model,
                authDataStorage.GetToken()
            );
        }

        /// <summary>
        /// Terminates the calendar item.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemId">The calendar item identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Task TerminateCalendarItem(Guid patientId, Guid calendarItemId, TerminateProgramRequest model)
        {
            return patientsService.TerminateCalendarItem(
                CustomerContext.Current.Customer.Id,
                patientId,
                calendarItemId,
                model,
                authDataStorage.GetToken()
            );
        }

        /// <summary>
        /// Returns calendar program using program id.
        /// Returns null it program with match correlator is not found.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="calendarProgramId"></param>
        /// <returns></returns>
        public async Task<CalendarProgramViewModel> GetCalendarProgramById(Guid patientId, Guid calendarProgramId)
        {
            var result = await patientsService.GetCalendarProgramById(
                CustomerContext.Current.Customer.Id,
                patientId,
                calendarProgramId,
                authDataStorage.GetToken()
            );

            return Mapper.Map<CalendarProgramViewModel>(result);
        }

        /// <summary>
        /// Returns list of records with history of calendar changes.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CalendarChangeViewModel>> GetCalendarHistory(Guid patientId, BaseSearchDto searchDto)
        {
            var result = await patientsService.GetCalendarHistory(
                CustomerContext.Current.Customer.Id,
                patientId,
                searchDto,
                authDataStorage.GetToken()
            );

            return Mapper.Map<IList<CalendarChangeViewModel>>(result.Results);
        }

        /// <summary>
        /// Deletes calendar program.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarProgramId">The calendar program identifier</param>
        /// <returns></returns>
        public async Task DeleteCalendarProgram(Guid patientId, Guid calendarProgramId)
        {
            await patientsService.DeleteProgram(
                CustomerContext.Current.Customer.Id,
                patientId,
                calendarProgramId,
                authDataStorage.GetToken()
            );
        }
    }
}