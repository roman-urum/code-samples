using Maestro.Domain.Dtos.HealthLibraryService.Protocols;
using System;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Web.Mvc;
using Maestro.Domain.Enums;
using Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// CareBuilderController.Protocols
    /// </summary>
    public partial class CareBuilderController
    {
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="language">The language.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Protocol")]
        [ResponseType(typeof(ProtocolResponseViewModel))]
        [CustomerAuthorize(
            CustomerUserRolePermissions.BrowseHealthContent,
            CustomerUserRolePermissions.ManageHealthProtocols
        )]
        public async Task<ActionResult> GetProtocol(
            Guid protocolId,
            string language = null,
            bool isBrief = true
        )
        {
            var result = await careBuilderManager.GetProtocol(protocolId, language, isBrief);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Searches the protocols.
        /// </summary>
        /// <param name="searchProtocolsModel">The search protocols model.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Protocols")]
        [CustomerAuthorize(CustomerUserRolePermissions.BrowseHealthContent)]
        public async Task<ActionResult> SearchProtocols(SearchProtocolsRequestDto searchProtocolsModel)
        {
            var searchResult = await careBuilderManager.SearchProtocols(searchProtocolsModel);

            return Json(searchResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates new answer answerset with default answer strings.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Protocol")]
        [CustomerAuthorize(
            CustomerUserRolePermissions.BrowseHealthContent,
            CustomerUserRolePermissions.ManageHealthProtocols
        )]
        public async Task<ActionResult> CreateProtocol(CreateProtocolRequestDto request)
        {
            var result = await careBuilderManager.CreateProtocol(request);

            return Json(result);
        }

        /// <summary>
        /// Updates the protocol.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("Protocol")]
        [CustomerAuthorize(
            CustomerUserRolePermissions.BrowseHealthContent,
            CustomerUserRolePermissions.ManageHealthProtocols
        )]
        public async Task<ActionResult> UpdateProtocol(UpdateProtocolRequestDto request)
        {
            await careBuilderManager.UpdateProtocol(request);

            return Json(string.Empty);
        }

        /// <summary>
        /// 'Create protocol' view with backbone templates.
        /// </summary>
        /// <returns></returns>
        [CustomerAuthorize(CustomerUserRolePermissions.ManageHealthProtocols)]
        public ActionResult CreateProtocol()
        {
            return View(
                "Templates", new
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
        /// 'Edit protocol' view with backbone templates.
        /// </summary>
        /// <returns></returns>
        [CustomerAuthorize(CustomerUserRolePermissions.BrowseHealthContent)]
        public ActionResult EditProtocol(Guid id)
        {
            return View(
                "Templates", new
                {
                    customer = new
                    {
                        id = CustomerContext.Current.Customer.Id,
                        categoriesOfCare = CustomerContext.Current.Customer.CategoriesOfCare
                    }
                }
            );
        }
    }
}