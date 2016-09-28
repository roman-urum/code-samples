using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Web.Api.Filters;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.ScaleAnswerSets;
using HealthLibrary.Web.Api.Models.Enums;
using HealthLibrary.Web.Api.Resources;
using Swashbuckle.Swagger.Annotations;

namespace HealthLibrary.Web.Api.Controllers
{
    /// <summary>
    /// ScaleAnswerSetsController.
    /// </summary>
    [TokenAuthorize]
    public class ScaleAnswerSetsController : BaseApiController
    {
        private readonly IScaleAnswerSetControllerHelper scaleAnswerSetHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleAnswerSetsController"/> class.
        /// </summary>
        /// <param name="scaleAnswerSetHelper">The scale answer set helper.</param>
        public ScaleAnswerSetsController(IScaleAnswerSetControllerHelper scaleAnswerSetHelper)
        {
            this.scaleAnswerSetHelper = scaleAnswerSetHelper;
        }

        /// <summary>
        /// Create scale answer set.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/scale")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [SwaggerResponse(HttpStatusCode.OK, "New selection answerset was created.", typeof(PostResponseDto<Guid>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in request.")]
        [InvalidateCacheOutput(nameof(GetScaleAnswerSets), typeof(ScaleAnswerSetsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetScaleAnswerSet), typeof(ScaleAnswerSetsController), "customerId")]
        public async Task<IHttpActionResult> CreateScaleAnswerSet(int customerId, [FromBody]CreateScaleAnswerSetRequestDto model)
        {
            var result = await scaleAnswerSetHelper.Create(customerId, model);

            if (!result.Status.HasFlag(CreateScaleAnswerSetStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Status.GetConcatString(),
                    });
            }

            return Created(new Uri(Request.RequestUri, result.Content.ToString()),
                new PostResponseDto<Guid> { Id = result.Content });
        }

        /// <summary>
        /// Updates data of specified answer set and default answer strings.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/scale/{scaleAnswerSetId:guid}")]
        [SwaggerResponse(HttpStatusCode.OK, "Answerset data was updated.", typeof(ScaleAnswerSetResponseDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Required answerset does not exists.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in request.")]
        [InvalidateCacheOutput(nameof(GetScaleAnswerSets), typeof(ScaleAnswerSetsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetScaleAnswerSet), typeof(ScaleAnswerSetsController), "customerId", "scaleAnswerSetId")]
        [InvalidateCacheOutput(nameof(QuestionElementsController.GetQuestionElements), typeof(QuestionElementsController), "customerId")]
        [InvalidateCacheOutput(nameof(QuestionElementsController.GetQuestionElement), typeof(QuestionElementsController), "customerId")]
        public async Task<IHttpActionResult> UpdateScaleAnswerSet(
            int customerId,
            Guid scaleAnswerSetId,
            UpdateScaleAnswerSetRequestDto model
        )
        {
            var status = await scaleAnswerSetHelper.Update(customerId, scaleAnswerSetId, model);

            if (status == UpdateScaleAnswerSetStatus.NotFound)
            {
                return Content(HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = status.GetConcatString()
                    }
                );
            }

            if (!status.HasFlag(UpdateScaleAnswerSetStatus.Success))
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
        /// Includes or updates additional localized strings for answerchoices.
        /// </summary>
        /// <param name="customerId">Customer's Id</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="language">Culture</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/scale/{scaleAnswerSetId:guid}/{language:regex(^[a-z]{2}-[A-Z]{2}$)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Answerset data was updated.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Required answerset does not exists.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes are provided in request.")]
        [InvalidateCacheOutput(nameof(GetScaleAnswerSets), typeof(ScaleAnswerSetsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetScaleAnswerSet), typeof(ScaleAnswerSetsController), "customerId", "scaleAnswerSetId")]
        [InvalidateCacheOutput(nameof(QuestionElementsController.GetQuestionElements), typeof(QuestionElementsController), "customerId")]
        [InvalidateCacheOutput(nameof(QuestionElementsController.GetQuestionElement), typeof(QuestionElementsController), "customerId")]
        public async Task<IHttpActionResult> UpdateScaleAnswerSetLocalizedStrings(
            int customerId,
            Guid scaleAnswerSetId, 
            UpdateScaleAnswerSetLocalizedRequestDto model,
            string language
        )
        {
            var status = await scaleAnswerSetHelper.UpdateLocalizedStrings(customerId, scaleAnswerSetId, model, language);

            if (status == UpdateScaleAnswerSetLocalization.NotFound)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = status.GetConcatString()
                    }
                );
            }
            if (!status.HasFlag(UpdateScaleAnswerSetLocalization.Success))
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
        /// Deletes selection answer set with specified id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/scale/{scaleAnswerSetId:guid}")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Required answerset does not exists.")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Answerset was deleted.")]
        [InvalidateCacheOutput(nameof(GetScaleAnswerSets), typeof(ScaleAnswerSetsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetScaleAnswerSet), typeof(ScaleAnswerSetsController), "customerId", "scaleAnswerSetId")]
        public async Task<IHttpActionResult> DeleteScaleAnswerSet(int customerId, Guid scaleAnswerSetId)
        {
            var status = await scaleAnswerSetHelper.Delete(customerId, scaleAnswerSetId);

            if (status == DeleteStatus.NotFound)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = status.Description("Answerset")
                    }
                );
            }

            if (status == DeleteStatus.InUse)
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = status.Description("Answerset")
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Returns answerset by id with specified culture or default.
        /// </summary>
        /// <param name="customerId">Id of customer(opt.)</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <param name="language">Language</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/scale/{scaleAnswerSetId:guid}")]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/scale/{scaleAnswerSetId:guid}/{language:regex(^[a-z]{2}-[A-Z]{2}$)}")]
        [SwaggerResponse(HttpStatusCode.OK, "Answerset exists.",
            typeof(ScaleAnswerSetResponseDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Required answerset does not exist.")]
        [ResponseType(typeof(ScaleAnswerSetResponseDto))]
        public async Task<IHttpActionResult> GetScaleAnswerSet(int customerId, Guid scaleAnswerSetId, string language = null)
        {
            var result = await scaleAnswerSetHelper.Get(customerId, scaleAnswerSetId, language);

            if (result == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = GlobalStrings.ScaleAnswerSet_NotFoundError
                    }
                );
            }

            return Ok(result);
        }

        /// <summary>
        /// Returns list of answersets by search criteria.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/scale")]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/scale/{language:regex(^[a-z]{2}-[A-Z]{2}$)}")]
        [SwaggerResponse(HttpStatusCode.OK, "Response with appropriate answersets.",
            typeof(PagedResultDto<ScaleAnswerSetResponseDto>))]
        [ResponseType(typeof(PagedResultDto<ScaleAnswerSetResponseDto>))]
        public async Task<IHttpActionResult> GetScaleAnswerSets(
            int customerId,
            [FromUri]TagsSearchDto model,
            string language = null
        )
        {
            var result = await scaleAnswerSetHelper.Find(customerId, model);

            return Ok(result);
        }
    }
}