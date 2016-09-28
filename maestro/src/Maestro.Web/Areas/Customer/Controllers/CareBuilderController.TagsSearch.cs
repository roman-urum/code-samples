using System.Threading.Tasks;
using System.Web.Mvc;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// CareBuilderController.TagsSearch
    /// </summary>
    public partial class CareBuilderController
    {
        /// <summary>
        /// Searches the tags.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Tags")]
        public async Task<ActionResult> SearchTags(string term = null)
        {
            var searchResult = await careBuilderManager.SearchTags(term);

            return Json(searchResult, JsonRequestBehavior.AllowGet);
        }
    }
}