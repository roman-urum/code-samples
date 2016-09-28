using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HealthLibrary.Web.Api.Filters;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models.Elements;
using Swashbuckle.Swagger.Annotations;

namespace HealthLibrary.Web.Api.Controllers
{
    /// <summary>
    /// CRUD OpenEnded Answer-set Element
    /// </summary>
    [TokenAuthorize]
    public class OpenEndedAnswerSetsController : BaseApiController
    {
        private readonly IOpenEndedAnswerSetsControllerHelper openEndedAnswerSetsControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenEndedAnswerSetsController"/> class.
        /// </summary>
        /// <param name="openEndedAnswerSetsControllerHelper">The open ended answer sets controller helper.</param>
        public OpenEndedAnswerSetsController(IOpenEndedAnswerSetsControllerHelper openEndedAnswerSetsControllerHelper)
        {
            this.openEndedAnswerSetsControllerHelper = openEndedAnswerSetsControllerHelper;
        }

        /// <summary>
        /// Returns open ended answerset for specified customer.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/open-ended")]
        [SwaggerResponse(HttpStatusCode.OK, "Response with appropriate answerset.",
            typeof(OpenEndedAnswerSetResponseDto))]
        [ResponseType(typeof(OpenEndedAnswerSetResponseDto))]
        public async Task<IHttpActionResult> GetOpenEndedAnswerSet(int customerId)
        {
            var result = await this.openEndedAnswerSetsControllerHelper.Get(customerId);

            return Ok(result);
        }
    }
}