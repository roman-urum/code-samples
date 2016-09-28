using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Domain.Constants;
using Maestro.Web.Managers.Interfaces;
using Maestro.Web.Models.Users;

namespace Maestro.Web.Areas.Settings.Controllers
{
    /// <summary>
    /// AdminsController.
    /// </summary>
    public class AdminsController : Controller
    {
        private readonly IAdminsControllerManager adminsControllerManager;
        private readonly IUsersControllerManager usersManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminsController"/> class.
        /// </summary>
        /// <param name="adminsControllerManager">The admins controller manager.</param>
        /// <param name="usersManager">The users manager.</param>
        public AdminsController(IAdminsControllerManager adminsControllerManager, IUsersControllerManager usersManager)
        {
            this.adminsControllerManager = adminsControllerManager;
            this.usersManager = usersManager;
        }

        /// <summary>
        /// Returns list of existed admins.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var admins = await adminsControllerManager.GetAdmins();

            return View(admins);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            var model = new UserViewModel() { Role = Roles.SuperAdmin, IsEnabled = true};

            return View(model);
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ValidationException("Invalid model");
                }

                var error = await this.usersManager.ValidateCreateUser(model);

                if (error != null)
                {
                    throw new ValidationException("Invalid user");
                }

                model = await this.adminsControllerManager.CreateAdmin(model);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("CreateException", exception);

                return this.View(model);
            }
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var adminModel = await this.adminsControllerManager.GetAdmin(id);

            return this.View(adminModel);
        }

        /// <summary>
        /// Edits the specified edited admin.
        /// </summary>
        /// <param name="editedAdmin">The edited admin.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(UserViewModel editedAdmin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ValidationException("Invalid model");
                }

                var error = await this.usersManager.ValidateEditUser(editedAdmin);

                if (error != null)
                {
                    throw new ValidationException("Invalid user");
                }

                await adminsControllerManager.EditAdmin(editedAdmin);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CreateException", ex);

                return this.View(editedAdmin);
            }
        }

        /// <summary>
        /// Action to change customer user enabled/disabled state.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SetEnabledState(Guid userId, bool isEnabled)
        {
            await adminsControllerManager.SetEnabledState(userId, isEnabled);

            return Json(new { success = true });
        }
    }
}