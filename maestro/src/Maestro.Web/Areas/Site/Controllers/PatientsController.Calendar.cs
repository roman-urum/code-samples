using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.PatientsService.Calendar;
using Maestro.Web.Areas.Customer;
using Maestro.Web.Areas.Site.Models.Patients.Calendar;
using Maestro.Web.Extensions;
using Newtonsoft.Json;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.Calendar
    /// </summary>
    public partial class PatientsController
    {
        /// <summary>
        /// Gets the json with adherences and programs in accordance with search criteria.
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("AdherencesAndPrograms")]
        public async Task<ActionResult> GetAdherencesAndPrograms(
            Guid patientId,
            [FromUri]AdherencesSearchDto searchCriteria
        )
        {
            var adherences = await patientsControllerManager.GetAdherencesAndPrograms(patientId, searchCriteria);

            return Content(JsonConvert.SerializeObject(adherences), "application/json");
        }

        /// <summary>
        /// Generates calendar item for patient.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="calendarItemModel"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("CalendarItems")]
        public async Task<ActionResult> CreateCalendarItem(
            [FromUri] Guid patientId,
            CalendarItemViewModel calendarItemModel)
        {
            var result = await patientsControllerManager.CreateCalendarItem(patientId, calendarItemModel);

            return Json(result);
        }

        /// <summary>
        /// Deletes existing calendar item. 
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="calendarItemId">The calendar item identifier.</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpDelete]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("CalendarItems")]
        public async Task<ActionResult> DeleteCalendarItem(Guid patientId, Guid calendarItemId)
        {
            await patientsControllerManager.DeleteCalendarItem(patientId, calendarItemId);

            return Json(string.Empty);
        }

        /// <summary>
        /// Updates calendar item.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="calendarItemId"></param>
        /// <param name="calendarItemModel"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPut]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("CalendarItems")]
        public async Task<ActionResult> UpdateCalendarItem(
            Guid patientId,
            Guid calendarItemId,
            CalendarItemViewModel calendarItemModel)
        {
            await patientsControllerManager.UpdateCalendarItem(patientId, calendarItemId, calendarItemModel);

            return Json(string.Empty);
        }

        /// <summary>
        /// Terminates the calendar item
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="calendarItemId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("TerminateCalendarItem")]
        public async Task<ActionResult> TerminateCalendarItem(
            [FromUri]Guid patientId,
            [FromUri]Guid calendarItemId,
            TerminateProgramRequest model
        )
        {
            await patientsControllerManager.TerminateCalendarItem(patientId, calendarItemId, model);

            return Json(string.Empty);
        }

        /// <summary>
        /// Generates schedule for specified program in patient calendar.
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("Programs")]
        public async Task<ActionResult> CreateCalendarProgram(
            [FromUri]Guid patientId, 
            ScheduleCalendarProgramViewModel model
        )
        {
            if (!ModelState.IsValid)
            {
                return Json(ModelState.GetErrorMessages(), JsonRequestBehavior.AllowGet);
            }

            await this.patientsControllerManager.CreateCalendarProgram(patientId, model);

            return Json(string.Empty);
        }

        /// <summary>
        /// Removes scheduled program with events and generates new with updated settings.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="calendarProgramId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPut]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("Programs")]
        public async Task<ActionResult> UpdateCalendarProgram(
            [FromUri]Guid patientId, 
            [FromUri]Guid calendarProgramId,
            ScheduleCalendarProgramViewModel model
        )
        {
            if (!ModelState.IsValid)
            {
                return Json(ModelState.GetErrorMessages(), JsonRequestBehavior.AllowGet);
            }

            await this.patientsControllerManager.UpdateCalendarProgram(patientId, calendarProgramId, model);

            return Json(string.Empty);
        }

        [System.Web.Mvc.HttpPost]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("TerminateProgram")]
        public async Task<ActionResult> TerminateProgram(
            [FromUri]Guid patientId,
            [FromUri]Guid calendarProgramId,
            TerminateProgramRequest model
        )
        {
            await patientsControllerManager.TerminateProgram(patientId, calendarProgramId, model);

            return Json(string.Empty);
        }

        /// <summary>
        /// Returns calendar program by correlator value.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="calendarProgramId"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("Programs")]
        public async Task<ActionResult> GetProgramById([FromUri] Guid patientId, [FromUri] Guid calendarProgramId)
        {
            var result = await this.patientsControllerManager.GetCalendarProgramById(patientId, calendarProgramId);

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        /// <summary>
        /// Delete calendar program.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="calendarProgramId"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpDelete]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("Programs")]
        public async Task<ActionResult> DeleteProgram([FromUri] Guid patientId, [FromUri] Guid calendarProgramId)
        {
            await this.patientsControllerManager.DeleteCalendarProgram(patientId, calendarProgramId);

            return Content("null", "application/json");
        }

        /// <summary>
        /// Returns list of records with history of calendar changes.
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("History")]
        public async Task<ActionResult> GetCalendarHistory([FromUri] Guid patientId, [FromUri] BaseSearchDto searchDto)
        {
            var result = await this.patientsControllerManager.GetCalendarHistory(patientId, searchDto);

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
    }
}