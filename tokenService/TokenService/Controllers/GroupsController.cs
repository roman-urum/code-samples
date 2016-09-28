using CareInnovations.HealthHarmony.Maestro.TokenService.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using CareInnovations.HealthHarmony.Maestro.TokenService.Helpers.Interfaces;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Controllers
{
    /// <summary>
    /// GroupsController.
    /// </summary>
    public class GroupsController : ServiceController
    {
        private readonly IGroupsControllerHelper groupsControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsController"/> class.
        /// </summary>
        /// <param name="groupsControllerHelper">The groups controller helper.</param>
        public GroupsController(IGroupsControllerHelper groupsControllerHelper)
        {
            this.groupsControllerHelper = groupsControllerHelper;
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <param name="take">The take.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(
            int take = MAX_PAGE_SIZE,
            int skip = 0,
            int? customerId = null
        )
        {
            var result = await this.groupsControllerHelper.GetGroups(customerId, skip, Math.Min(take, MAX_PAGE_SIZE));

            return Ok(result);
        }

        /// <summary>
        /// Gets the specified group.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var group = await this.groupsControllerHelper.GetGroupById(id);

            if (group != null)
            {
                return Ok(group);
            }

            return NotFound();
        }

        /// <summary>
        /// Creates the specified group.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(CreateGroupModel model)
        {
            var result = await this.groupsControllerHelper.CreateGroup(model);

            return Ok(result);
        }

        /// <summary>
        /// Updates the specified group.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put(Guid id, GroupModel model)
        {
            var result = await this.groupsControllerHelper.UpdateGroup(id, model);

            if (result == null)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes the specified group.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            if (await this.groupsControllerHelper.DeleteGroup(id))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return NotFound();
        }
    }
}