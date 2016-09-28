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
    /// SearchController.
    /// </summary>
    [TokenAuthorize]
    public class SearchController : ApiController
    {
        private readonly ISearchControllerHelper searchControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController"/> class.
        /// </summary>
        /// <param name="searchControllerHelper">The search controller helper.</param>
        public SearchController(ISearchControllerHelper searchControllerHelper)
        {
            this.searchControllerHelper = searchControllerHelper;
        }

        /// <summary>
        /// Performs global search.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/search")]
        [IgnoreCacheOutput]
        [ResponseType(typeof(PagedResultDto<SearchEntryDto>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with search entities.", typeof(PagedResultDto<SearchEntryDto>))]
        public async Task<IHttpActionResult> Search(
            int customerId,
            [FromUri]GlobalSearchDto request
        )
        {
            var result = await searchControllerHelper.Search(customerId, request);

            return Ok(result);
        }
    }
}