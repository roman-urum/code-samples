using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Web.Areas.Customer.Models.CareBuilder.ScaleAnsweSet;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SearchCareElements;
using Maestro.Web.Filters;
using Newtonsoft.Json;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// CareBuilderController.
    /// </summary>
    public partial class CareBuilderController
    {
        /// <summary>
        /// Creates the scale answer set.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [ValidateModelStateJson]
        [HttpPost]
        [ActionName("ScaleAnswerSet")]

        public async Task<JsonResult> CreateScaleAnswerSet(ScaleAnswerSetRequestViewModel model)
        {
            var result = await careBuilderManager.CreateScaleAnswerSet(model);

            return Json(JsonConvert.SerializeObject(result));
        }

        /// <summary>
        /// Gets the scale answer set.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ScaleAnswerSet")]
        public async Task<JsonResult> GetScaleAnswerSet(Guid id)
        {
            var result = await careBuilderManager.GetScaleAnswerSet(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Updates the scale answer set.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [ValidateModelStateJson]
        [HttpPut]
        [ActionName("ScaleAnswerSet")]
        public async Task<JsonResult> UpdateScaleAnswerSet(ScaleAnswerSetUpdateRequestViewModel model)
        {
            await careBuilderManager.UpdateScaleAnswerSet(model);

            return Json(string.Empty);
        }

        /// <summary>
        /// Returns list of selection answer sets as json.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ScaleAnswerSets")]
        public async Task<ActionResult> GetScaleAnswerSets(SearchCareElementsViewModel searchCriteria)
        {
            var result = await careBuilderManager.FindScaleAnswerSets(searchCriteria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
