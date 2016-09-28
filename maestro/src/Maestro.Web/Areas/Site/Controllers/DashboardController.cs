using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Maestro.Web.Filters;
using System.Threading.Tasks;
using System.Web.SessionState;
using AutoMapper;

using Maestro.Web.Areas.Customer;
using Maestro.Web.Areas.Site.Managers.Interfaces;
using Maestro.Web.Areas.Site.Models.Dashboard;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Controllers;
using Maestro.Web.Security;

using Newtonsoft.Json;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// DashboardController.
    /// </summary>
    [MaestroAuthorize]
    [CustomerAuthorize]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class DashboardController : BaseController
    {
        private readonly IDashboardControllerHelper dashboardControllerHelper;
        private readonly IAuthDataStorage authDataStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        /// <param name="dashboardControllerHelper">The dashboard controller helper.</param>
        /// <param name="authDataStorage"></param>
        public DashboardController(
            IDashboardControllerHelper dashboardControllerHelper,
            IAuthDataStorage authDataStorage)
        {
            this.dashboardControllerHelper = dashboardControllerHelper;
            this.authDataStorage = authDataStorage;
        }

        /// <summary>
        /// Index action.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            dashboardControllerHelper.ClearCache();

            var currentCareManager = Mapper.Map<CareManagerViewModel>(authDataStorage.GetUserAuthData());

            return View(new {
                currentCareManager
            });
        }

        /// <summary>
        /// Gets the patient card.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("PatientCards")]
        public async Task<ActionResult> GetPatientCards(GetPatientCardsRequestViewModel request)
        {
            var result = await dashboardControllerHelper.GetPatientCards(request);

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        /// <summary>
        /// Gets the patients cards.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("PatientsCards")]
        public async Task<ActionResult> GetPatientsCards(GetPatientsCardsRequestViewModel request)
        {
            var result = await dashboardControllerHelper.GetPatientsCards(request);

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        /// <summary>
        /// Acknowledges alerts.
        /// </summary>
        /// <param name="alertIds">The list if alert identifiers.</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AcknowledgeAlerts")]
        public async Task<ActionResult> AcknowledgeAlerts(List<Guid> alertIds)
        {
            await dashboardControllerHelper.AcknowledgeAlerts(alertIds);
            
            return Content("null", "application/json");
        }
        /// <summary>
        /// Ignores the reading.
        /// </summary>
        /// <param name="measurementId">The measurement identifier.</param>
        /// <param name="alertIds">The alerts identifiers.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("IgnoreReading")]
        public async Task<ActionResult> IgnoreReading(Guid measurementId, List<Guid> alertIds, Guid patientId)
        {
            await dashboardControllerHelper.IgnoreReading(User, measurementId, alertIds, patientId);

            return Content("null", "application/json");
        }
    }
}