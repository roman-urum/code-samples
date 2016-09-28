using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Domain.Dtos.HealthLibraryService.Programs;
using Maestro.Domain.Enums;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// CareBuilderController.
    /// </summary>
    public partial class CareBuilderController
    {
        /// <summary>
        /// Gets the program.
        /// </summary>
        /// <param name="id">The program identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Program")]
        [CustomerAuthorize(
            CustomerUserRolePermissions.BrowseHealthContent,
            CustomerUserRolePermissions.ManageHealthPrograms
        )]
        public async Task<ActionResult> GetProgram(Guid id)
        {
            var result = await careBuilderManager.GetProgram(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Searches the programs.
        /// </summary>
        /// <param name="searchProgramsDto">The search programs dto.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Programs")]
        [CustomerAuthorize(CustomerUserRolePermissions.BrowseHealthContent)]
        public async Task<ActionResult> SearchPrograms(SearchProgramsRequestDto searchProgramsDto)
        {
            var searchResult = await careBuilderManager.SearchPrograms(searchProgramsDto);

            return Json(searchResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Saves program data.
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Program")]
        [CustomerAuthorize(
            CustomerUserRolePermissions.ManageHealthPrograms,
            CustomerUserRolePermissions.BrowseHealthContent)]
        public async Task<ActionResult> CreateProgram(ProgramRequestDto program)
        {
            var programId = await this.careBuilderManager.CreateProgram(program);

            return Json(programId);
        }

        /// <summary>
        /// Updates info about program.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="program">The program.</param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("Program")]
        [CustomerAuthorize(
            CustomerUserRolePermissions.ManageHealthPrograms,
            CustomerUserRolePermissions.BrowseHealthContent
        )]
        public async Task<ActionResult> UpdateProgram(Guid id, ProgramRequestDto program)
        {
            await this.careBuilderManager.UpdateProgram(id, program);

            return Json(string.Empty);
        }

        /// <summary>
        /// 'Create program' view with backbone templates.
        /// </summary>
        /// <returns></returns>
        [CustomerAuthorize(CustomerUserRolePermissions.ManageHealthPrograms)]
        public ActionResult CreateProgram()
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
        /// 'Edit program' view with backbone templates.
        /// </summary>
        /// <returns></returns>
        [CustomerAuthorize(CustomerUserRolePermissions.BrowseHealthContent)]
        public ActionResult EditProgram(Guid id)
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