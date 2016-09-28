using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SearchCareElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.TextMediaElements;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// CareBuilderController.
    /// </summary>
    public partial class CareBuilderController
    {
        /// <summary>
        /// Returns list of text media elements as json.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("TextMediaElements")]
        public async Task<ActionResult> GetTextMediaElements(SearchCareElementsViewModel searchCriteria)
        {
            var result = await careBuilderManager.FindTextMediaElements(searchCriteria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates the text media element.
        /// </summary>
        /// <param name="createTextMediaElementModel">The create text media element model.</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("TextMediaElement")]
        public async Task<JsonResult> CreateTextMediaElement(CreateTextMediaElementViewModel createTextMediaElementModel)
        {
            await careBuilderManager.CreateTextMediaElement(createTextMediaElementModel);

            return Json("Ok");
        }

        /// <summary>
        /// Updates the text media eelement.
        /// </summary>
        /// <param name="updateTextMediaElementModel">The update text media element model.</param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("TextMediaElement")]
        public async Task<JsonResult> UpdateTextMediaElement(UpdateTextMediaElementViewModel updateTextMediaElementModel)
        {
            await careBuilderManager.UpdateTextMediaElement(updateTextMediaElementModel);

            return Json("Ok");
        }

        /// <summary>
        /// Gets the text media element.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("TextMediaElement")]
        public async Task<JsonResult> GetTextMediaElement(Guid id)
        {
            var result = await this.careBuilderManager.GetTextMediaElement(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}