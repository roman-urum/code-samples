using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Web.Areas.Customer.Models.CareBuilder.QuestionElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SearchCareElements;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// CareBuilderController.
    /// </summary>
    public partial class CareBuilderController
    {
        /// <summary>
        /// Handles requests to create new customer record.
        /// </summary>
        /// <param name="createQuestionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("QuestionElement")]
        public async Task<JsonResult> CreateQuestion(CreateQuestionElementViewModel createQuestionViewModel)
        {
            await careBuilderManager.CreateQuestionElement(createQuestionViewModel);

            return Json("Ok");
        }

        /// <summary>
        /// Updates question record.
        /// </summary>
        /// <param name="updateQuestionElementViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("QuestionElement")]
        public async Task<JsonResult> UpdateQuestion(UpdateQuestionElementViewModel updateQuestionElementViewModel)
        {
            await careBuilderManager.UpdateQuestionElement(updateQuestionElementViewModel);

            return Json("Ok");
        }

        [HttpGet]
        [ActionName("QuestionElement")]
        public async Task<JsonResult> GetQuestionElement(Guid id)
        {
            var questionElement =  await careBuilderManager.GetQuestionElement(id);

            return Json(questionElement, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns list of question elements as json.
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("QuestionElements")]
        public async Task<ActionResult> GetQuestionElements(SearchCareElementsViewModel searchCriteria)
        {
            var result = await careBuilderManager.FindQuestionElements(searchCriteria);
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}