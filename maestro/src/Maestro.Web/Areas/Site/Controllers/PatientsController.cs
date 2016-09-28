using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.SessionState;
using AutoMapper;

using Maestro.Domain.Constants;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Web.Areas.Customer;
using Maestro.Web.Areas.Site.Managers.Interfaces;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Areas.Site.Models.Patients.CareManagers;
using Maestro.Web.Areas.Site.Models.Patients.SearchPatients;
using Maestro.Web.Controllers;
using Maestro.Web.Filters;
using Maestro.Web.Security;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.
    /// </summary>
    [MaestroAuthorize]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public partial class PatientsController : BaseController
    {
        private readonly IPatientsControllerManager patientsControllerManager;
        private readonly IAuthDataStorage authDataStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientsController"/> class.
        /// </summary>
        /// <param name="patientsControllerManager">The patients controller manager.</param>
        /// <param name="authDataStorage">The authentication data storage.</param>
        public PatientsController(
            IPatientsControllerManager patientsControllerManager,
            IAuthDataStorage authDataStorage)
        {
            this.patientsControllerManager = patientsControllerManager;
            this.authDataStorage = authDataStorage;
        }

        /// <summary>
        /// 'Index' view with Patient templates.
        /// </summary>
        /// <returns></returns>
        [CustomerAuthorize]
        public async Task<ActionResult> Index()
        {   
            return View(
                "Templates",
                new
                {
                    customer = new
                    {
                        patientIdentifiers = await patientsControllerManager.GetIdentifiersWithinCustomerScope()
                    },
                    site = new
                    {
                        id = SiteContext.Current.Site.Id,
                        categoriesOfCare = SiteContext.Current.Site.CategoriesOfCare
                    },
                    currentCareManager = User.IsInCustomerRoles(CustomerUserRoles.ManageAllPatients)
                        ? Mapper.Map<CareManagerViewModel>(authDataStorage.GetUserAuthData())
                        : null
                }
            );
        }

        /// <summary>
        /// 'PatientDetails' view with Patient templates.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CustomerAuthorize]
        public async Task<ActionResult> PatientDetails(Guid id)
        {
            patientsControllerManager.ClearMeasurementsCache(id);

            return View(
                "Templates",
                new
                {
                    customer = new
                    {
                        patientIdentifiers =
                            await patientsControllerManager.GetIdentifiersWithinCustomerScope()
                    },
                    site = new
                    {
                        id = SiteContext.Current.Site.Id,
                        categoriesOfCare = SiteContext.Current.Site.CategoriesOfCare
                    },
                    currentCareManager = User.IsInCustomerRoles(CustomerUserRoles.ManageAllPatients)
                        ? Mapper.Map<CareManagerViewModel>(authDataStorage.GetUserAuthData())
                        : null
                }
            );
        }

        /// <summary>
        /// 'CreatePatient' view with Patient templates.
        /// </summary>
        /// <returns></returns>
        [CustomerAuthorize]
        public async Task<ActionResult> CreatePatient()
        {
            return View(
                "Templates",
                new
                {
                    customer = new
                    {
                        patientIdentifiers =
                            await patientsControllerManager.GetIdentifiersWithinCustomerScope()
                    },
                    site = new
                    {
                        id = SiteContext.Current.Site.Id,
                        categoriesOfCare = SiteContext.Current.Site.CategoriesOfCare
                    },
                    currentCareManager = User.IsInCustomerRoles(CustomerUserRoles.ManageAllPatients)
                        ? Mapper.Map<CareManagerViewModel>(authDataStorage.GetUserAuthData())
                        : null
                }
            );
        }

        /// <summary>
        /// 'EditPatient' view with Patient templates.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CustomerAuthorize]
        public async Task<ActionResult> EditPatient(Guid id)
        {
            return View(
                "Templates",
                new
                {
                    customer = new
                    {
                        patientIdentifiers =
                            await patientsControllerManager.GetIdentifiersWithinCustomerScope()
                    },
                    site = new
                    {
                        id = SiteContext.Current.Site.Id,
                        categoriesOfCare = SiteContext.Current.Site.CategoriesOfCare
                    },
                    currentCareManager = User.IsInCustomerRoles(CustomerUserRoles.ManageAllPatients)
                        ? Mapper.Map<CareManagerViewModel>(authDataStorage.GetUserAuthData())
                        : null
                }
            );
        }

        /// <summary>
        /// Gets the patient with short details about CareManagers.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("Patient")]
        public async Task<ActionResult> GetPatient(Guid patientId, bool isBrief = true)
        {
            if (isBrief)
            {
                return Json(await patientsControllerManager.GetBriefPatient(patientId), JsonRequestBehavior.AllowGet);
            }

            return Json(await patientsControllerManager.GetFullPatient(patientId), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates the patient.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("Patient")]
        public async Task<ActionResult> CreatePatient(CreatePatientRequestDto request)
        {
            var result = await patientsControllerManager.CreatePatient(request);

            return Json(result);
        }

        /// <summary>
        /// Updates the patient.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPut]
        [CustomerAuthorize]
        [System.Web.Mvc.ActionName("Patient")]
        public async Task<ActionResult> UpdatePatient(UpdatePatientRequestDto request)
        {
            await patientsControllerManager.UpdatePatient(request);

            return Json(string.Empty);
        }

        /// <summary>
        /// Suggestions the search.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<ActionResult> SuggestionSearch([FromUri]SearchPatientsViewModel searchRequest)
        {
            var result = await patientsControllerManager.SuggestionSearch(searchRequest);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Searches the specified search request.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<ActionResult> Search([FromUri]SearchPatientsViewModel searchRequest)
        {
            var result = await patientsControllerManager.Search(searchRequest);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Patients the search details.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<ActionResult> PatientSearchDetails(Guid patientId)
        {
            var result = await patientsControllerManager.GetPatientSearchDetails(patientId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Checks the connection.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<ActionResult> CheckConnection(Guid patientId)
        {
            await patientsControllerManager.CheckConnection(patientId);

            return Json(new { Success = true });
        }
    }
}