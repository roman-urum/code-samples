using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SearchCareElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerSet;
using Maestro.Web.Filters;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// CareBuilderController.
    /// </summary>
    public partial class CareBuilderController
    {
        /// <summary>
        /// Creates new answer answerset with default answer strings.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [ValidateModelStateJson]
        [HttpPost]
        [ActionName("SelectionAnswerSet")]
        public async Task<ActionResult> CreateSelectionAnswerSet(CreateSelectionAnswerSetViewModel request)
        {
            var result = await careBuilderManager.CreateSelectionAnswerSet(request);

            return Json(result);
        }

        /// <summary>
        /// Gets the selection answer set.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("SelectionAnswerSet")]
        public async Task<ActionResult> GetSelectionAnswerSet(Guid id)
        {
            var result = await careBuilderManager.GetSelectionAnswerSet(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Updates the selection answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [ValidateModelStateJson]
        [HttpPut]
        [ActionName("SelectionAnswerSet")]
        public async Task<ActionResult> UpdateSelectionAnswerSet(UpdateSelectionAnswerSetViewModel request)
        {
            await careBuilderManager.UpdateSelectionAnswerSet(request);

            return Json(string.Empty);
        }

        /// <summary>
        /// Returns list of selection answer sets as json.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("SelectionAnswerSets")]
        public async Task<ActionResult> GetSelectionAnswerSets(SearchCareElementsViewModel searchCriteria)
        {
            var result = await careBuilderManager.FindSelectionAnswerSets(searchCriteria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}