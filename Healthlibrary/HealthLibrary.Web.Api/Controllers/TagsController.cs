using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Web.Api.Filters;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;
using Swashbuckle.Swagger.Annotations;
using WebApi.OutputCache.V2;

namespace HealthLibrary.Web.Api.Controllers
{
    /// <summary>
    /// TagsController.
    /// </summary>
    [TokenAuthorize]
    public class TagsController : ApiController
    {
        private readonly ITagsControllerHelper tagsControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController" /> class.
        /// </summary>
        /// <param name="tagsControllerHelper">The search controller helper.</param>
        public TagsController(ITagsControllerHelper tagsControllerHelper)
        {
            this.tagsControllerHelper = tagsControllerHelper;
        }

        /// <summary>
        /// Performs tags search.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/tags")]
        [IgnoreCacheOutput]
        [ResponseType(typeof(PagedResultDto<string>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with search entities.", typeof(PagedResultDto<string>))]
        public async Task<IHttpActionResult> SearchTags(
            int customerId,
            [FromUri]BaseSearchDto request
        )
        {
            var result = await tagsControllerHelper.SearchTags(customerId, request);

            return Ok(result);
        }
    }
}