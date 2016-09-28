using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Web.Api.Filters;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.AssessmentElements;
using HealthLibrary.Web.Api.Models.Enums;
using HealthLibrary.Web.Api.Resources;
using Swashbuckle.Swagger.Annotations;

namespace HealthLibrary.Web.Api.Controllers
{
    /// <summary>
    /// Provides endpoints to receive Assessments by customer.
    /// </summary>
    [TokenAuthorize]
    public class AssessmentElementsController : BaseApiController
    {
        private readonly IAssessmentElementsControllerHelper assessmentElementsHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentElementsController"/> class.
        /// </summary>
        /// <param name="assessmentElementsHelper">The Assessment elements helper.</param>
        public AssessmentElementsController(IAssessmentElementsControllerHelper assessmentElementsHelper)
        {
            this.assessmentElementsHelper = assessmentElementsHelper;
        }

        /// <summary>
        /// Returns all Assessment elements for required customer.
        /// </summary>
        /// <returns></returns>
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/assessment-elements")]
        [ResponseType(typeof(PagedResultDto<AssessmentResponseDto>))]
        [SwaggerResponse(HttpStatusCode.OK, "Returns list of Assessment elements in response.", typeof(PagedResultDto<AssessmentResponseDto>))]
        public async Task<IHttpActionResult> Get(int customerId, [FromUri]TagsSearchDto request)
        {
            var result = await assessmentElementsHelper.GetAll(customerId, request);

            return Ok(result);
        }

        /// <summary>
        /// Returns Assessment element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="assessmentElementId"></param>
        /// <returns></returns>
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/assessment-elements/{assessmentElementId:guid}")]
        [SwaggerResponse(HttpStatusCode.OK, "Required Assessment element exists.", typeof(AssessmentResponseDto))]
        public async Task<IHttpActionResult> Get(int customerId, Guid assessmentElementId)
        {
            var result = await assessmentElementsHelper.GetById(customerId, assessmentElementId);

            if (result == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = GlobalStrings.Assessment_NotFoundError
                    });
            }

            return Ok(result);
        }
    }
}