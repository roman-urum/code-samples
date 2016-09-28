using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Swagger.Annotations;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Extensions;
using VitalsService.Web.Api.Filters;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Enums;
using VitalsService.Web.Api.Models.PatientNotes;

namespace VitalsService.Web.Api.Controllers
{
    /// <summary>
    /// SuggestedNotablesController.
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/notables")]
    public class SuggestedNotablesController : ApiController
    {
        private readonly ISuggestedNotablesControllerHelper suggestedNotablesControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestedNotablesController"/> class.
        /// </summary>
        /// <param name="suggestedNotablesControllerHelper">The suggested notables controller helper.</param>
        public SuggestedNotablesController(ISuggestedNotablesControllerHelper suggestedNotablesControllerHelper)
        {
            this.suggestedNotablesControllerHelper = suggestedNotablesControllerHelper;
        }

        /// <summary>
        /// Creates the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput(nameof(GetSuggestedNotables), typeof(SuggestedNotablesController), "customerId")]
        [InvalidateCacheOutput(nameof(GetSuggestedNotable), typeof(SuggestedNotablesController), "customerId")]
        public async Task<IHttpActionResult> CreateSuggestedNotable(
            int customerId,
            SuggestedNotableRequestDto request
        )
        {
            var result = await suggestedNotablesControllerHelper.CreateSuggestedNotable(customerId, request);

            if (!result.Status.HasFlag(SuggestedNotableStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Status.GetConcatString()
                    }
                );
            }

            return Created(
                new Uri(Request.RequestUri, result.Content.ToString()),
                new PostResponseDto<Guid> { Id = result.Content }
            );
        }

        /// <summary>
        /// Updates the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="suggestedNotableId">The suggested notable identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{suggestedNotableId:guid}")]
        [SwaggerResponse(HttpStatusCode.OK, "Suggested notable was updated.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Suggested notable does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        [InvalidateCacheOutput(nameof(GetSuggestedNotables), typeof(SuggestedNotablesController), "customerId")]
        [InvalidateCacheOutput(nameof(GetSuggestedNotable), typeof(SuggestedNotablesController), "customerId", "suggestedNotableId")]
        public async Task<IHttpActionResult> UpdateSuggestedNotable(
            int customerId,
            Guid suggestedNotableId,
            SuggestedNotableRequestDto request
        )
        {
            var status = await suggestedNotablesControllerHelper.UpdateSuggestedNotable(customerId, suggestedNotableId, request);

            if (!status.HasFlag(SuggestedNotableStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = status.GetConcatString()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="suggestedNotableId">The suggested notable identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{suggestedNotableId:guid}")]
        [SwaggerResponse(HttpStatusCode.OK, "Suggested notable was deleted.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Suggested notable with such identifier does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        [InvalidateCacheOutput(nameof(GetSuggestedNotables), typeof(SuggestedNotablesController), "customerId")]
        [InvalidateCacheOutput(nameof(GetSuggestedNotable), typeof(SuggestedNotablesController), "customerId", "suggestedNotableId")]
        public async Task<IHttpActionResult> DeleteSuggestedNotable(
            int customerId,
            Guid suggestedNotableId
        )
        {
            var status = await suggestedNotablesControllerHelper.DeleteSuggestedNotable(customerId, suggestedNotableId);

            if (status == SuggestedNotableStatus.NotFound)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = status.Description()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Gets the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="suggestedNotableId">The suggested notable identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{suggestedNotableId:guid}")]
        [ResponseType(typeof(SuggestedNotableDto))]
        [SwaggerResponse(HttpStatusCode.OK, "Existing suggested notable.", typeof(SuggestedNotableDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Suggested notable does not exist.")]
        public async Task<IHttpActionResult> GetSuggestedNotable(
            int customerId,
            Guid suggestedNotableId
        )
        {
            var result = await suggestedNotablesControllerHelper.GetSuggestedNotable(customerId, suggestedNotableId);

            if (result.Status == SuggestedNotableStatus.NotFound)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Status.Description()
                    }
                );
            }

            return Ok(result.Content);
        }

        /// <summary>
        /// Gets the suggested notables.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(PagedResultDto<SuggestedNotableDto>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with suggested notables.", typeof(PagedResultDto<SuggestedNotableDto>))]
        public async Task<IHttpActionResult> GetSuggestedNotables(
            int customerId,
            [FromUri]BaseSearchDto request
        )
        {
            var result = await suggestedNotablesControllerHelper.GetSuggestedNotables(customerId, request);

            return Ok(result);
        }
    }
}