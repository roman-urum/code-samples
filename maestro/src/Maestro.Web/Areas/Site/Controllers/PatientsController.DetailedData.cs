using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Web.Areas.Customer;
using Maestro.Web.Areas.Site.Models;
using Maestro.Web.Areas.Site.Models.Patients.DetailedData;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.DetailedData
    /// </summary>
    public partial class PatientsController
    {
        /// <summary>
        /// Gets the ungrouped health session detailed data.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("UngroupedHealthSessionsDetailedData")]
        public async Task<ActionResult> GetUngroupedHealthSessionsDetailedData(
            SearchUngroupedHealthSessionDetailedDataViewModel searchModel
        )
        {
            var ungroupedDetailedData = await patientsControllerManager.GetUngroupedHealthSessionsDetailedData(searchModel);

            return Content(
                JsonConvert.SerializeObject(
                    ungroupedDetailedData,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                ),
                "application/json"
            );
        }

        /// <summary>
        /// Gets the grouped health session detailed data.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("GroupedHealthSessionsDetailedData")]
        public async Task<ActionResult> GetGroupedHealthSessionsDetailedData(
            SearchDetailedDataViewModel searchModel
        )
        {
            var groupedDetailedData = await patientsControllerManager
                .GetGroupedHealthSessionsDetailedData(searchModel);

            return Content(
                JsonConvert.SerializeObject(
                    groupedDetailedData,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                ),
                "application/json"
            );
        }

        /// <summary>
        /// Gets the adhoc vitals detailed data.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("AdhocMeasurementsDetailedData")]
        public async Task<ActionResult> GetAdhocMeasurementsDetailedData(
            SearchAdhocMeasurementsDetailedDataViewModel searchModel
        )
        {
            var adhocMeasurementsDetailedData = await patientsControllerManager
                .GetAdhocMeasurementsDetailedData(searchModel);

            return Content(
                JsonConvert.SerializeObject(
                    adhocMeasurementsDetailedData,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                ),
                "application/json"
            );
        }

        /// <summary>
        /// Exports the detailed data to excel.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="groupByHealthSession">if set to <c>true</c> [group by health session].</param>
        /// <param name="observedFromUtc">The observed from UTC.</param>
        /// <param name="observedToUtc">The observed to UTC.</param>
        /// <param name="elementType">Type of the element.</param>
        /// <param name="vitalType">Type of the vital.</param>
        /// <param name="questionId">The question identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("ExportDetailedDataToExcel")]
        public async Task<ActionResult> ExportDetailedDataToExcel(
            Guid patientId,
            bool groupByHealthSession = false,
            DateTime? observedFromUtc = null,
            DateTime? observedToUtc = null,
            HealthSessionElementType? elementType = null,
            VitalType? vitalType = null,
            Guid? questionId = null
        )
        {
            var content = await patientsControllerManager
                .ExportDetailedDataToExcel(
                    patientId,
                    groupByHealthSession,
                    observedFromUtc,
                    observedToUtc,
                    elementType,
                    vitalType,
                    questionId
                );

            if (!content.Any())
            {
                return new EmptyResult();
            }

            var patient = PatientContext.Current.Patient;

            var fileName = observedFromUtc.HasValue && observedToUtc.HasValue ? 
                string.Format(
                    "detailed-data-patient-{0}-{1}-{2}-{3}.xlsx", 
                    patient.FirstName, 
                    patient.LastName,
                    observedFromUtc.Value.ToString("yyyy_M_d"), 
                    observedToUtc.Value.ToString("yyyy_M_d")
                ) : 
                string.Format(
                    "detailed-data-patient-{0}-{1}.xlsx", 
                    patient.FirstName,
                    patient.LastName
                );

            return File(content, "application/vnd.ms-excel", fileName);
        }
    }
}