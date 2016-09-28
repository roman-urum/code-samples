using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;
using Maestro.Domain.Enums;
using Maestro.Web.Areas.Customer.Managers.Interfaces;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SearchAnswerSets;
using Maestro.Web.Filters;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// CareBuilderController.
    /// </summary>
    [MaestroAuthorize]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public partial class CareBuilderController : Controller
    {
        private readonly ICareBuilderControllerManager careBuilderManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CareBuilderController" /> class.
        /// </summary>
        /// <param name="careBuilderManager">The care builder manager.</param>
        public CareBuilderController(ICareBuilderControllerManager careBuilderManager)
        {
            this.careBuilderManager = careBuilderManager;
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns></returns>
        [CustomerAuthorize(
            CustomerUserRolePermissions.BrowseHealthContent,
            CustomerUserRolePermissions.ManageCareElements
        )]
        public ActionResult CareElements()
        {
            return View(
                "Templates",
                new
                {
                    customer = new
                    {
                        id = CustomerContext.Current.Customer.Id,
                        categoriesOfCare = CustomerContext.Current.Customer.CategoriesOfCare
                    }
                }
            );
        }

        /// <summary>
        /// Action for Protocols and Programs tab.
        /// </summary>
        /// <returns></returns>
        [CustomerAuthorize(
            CustomerUserRolePermissions.BrowseHealthContent,
            CustomerUserRolePermissions.ManageHealthPrograms,
            CustomerUserRolePermissions.ManageHealthProtocols
        )]
        public ActionResult ProtocolsAndPrograms()
        {
            return View(
                "Templates",
                new
                {
                    customer = new
                    {
                        id = CustomerContext.Current.Customer.Id,
                        categoriesOfCare = CustomerContext.Current.Customer.CategoriesOfCare
                    }
                }
            );
        }

        [HttpGet]
        [ActionName("AnswerSets")]
        public async Task<JsonResult> FindAnswerSets(SearchAnswerSetViewModel searchAnswerSetModel)
        {
            var searchResult = await this.careBuilderManager.FindAnswerSets(searchAnswerSetModel);

            return this.Json(searchResult, JsonRequestBehavior.AllowGet);
        }
    }
}