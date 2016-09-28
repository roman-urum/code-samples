using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Web.Areas.Customer;
using Maestro.Web.Areas.Site.Models.Patients.Notes;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.Notes
    /// </summary>
    public partial class PatientsController
    {
        /// <summary>
        /// Creates note.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [CustomerAuthorize]
        [ActionName("Notes")]
        public async Task<ActionResult> CreateNote(CreateNoteViewModel request)
        {
            var createNoteResult = await patientsControllerManager.CreateNote(request);

            return Content(
                JsonConvert.SerializeObject(
                    createNoteResult,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                ),
                "application/json"
            );
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("Notes")]
        public async Task<ActionResult> GetNotes(SearchNotesViewModel searchModel)
        {
            var notesResult = await patientsControllerManager.GetNotes(searchModel);

            return Content(
                JsonConvert.SerializeObject(
                    notesResult,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                ),
                "application/json"
            );
        }

        /// <summary>
        /// Gets the notables.
        /// </summary>
        /// <param name="patientId">The search model.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("Notables")]
        public async Task<ActionResult> GetNotables(Guid patientId)
        {
            var notablesResult = await patientsControllerManager.GetNotables(patientId);

            return Content(JsonConvert.SerializeObject(notablesResult), "application/json");
        }

        /// <summary>
        /// Gets the notables.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("SuggestedNotables")]
        public async Task<ActionResult> GetSuggestedNotables()
        {
            var suggestedNotablesResult = await patientsControllerManager.GetSuggestedNotables();

            return Content(JsonConvert.SerializeObject(suggestedNotablesResult), "application/json");
        }
    }
}