using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.DomainLogic.Services.Results;
using HealthLibrary.Web.Api.Filters;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.QuestionElements;
using HealthLibrary.Web.Api.Resources;
using Swashbuckle.Swagger.Annotations;

namespace HealthLibrary.Web.Api.Controllers
{
    /// <summary>
    /// Provides methods to manage question elements.
    /// </summary>
    [TokenAuthorize]
    public class QuestionElementsController : BaseApiController
    {
        private readonly IQuestionElementControllerHelper questionElementHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionElementsController"/> class.
        /// </summary>
        /// <param name="questionElementHelper">The question element helper.</param>
        public QuestionElementsController(IQuestionElementControllerHelper questionElementHelper)
        {
            this.questionElementHelper = questionElementHelper;
        }

        /// <summary>
        /// Creates new question element.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/question-elements")]
        [SwaggerResponse(HttpStatusCode.OK, "New question element was created.", typeof (PostResponseDto<Guid>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in request.")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput(nameof(GetQuestionElements), typeof(QuestionElementsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetQuestionElement), typeof(QuestionElementsController), "customerId")]
        public async Task<IHttpActionResult> CreateQuestionElement(
            int customerId,
            CreateQuestionElementRequestDto model
        )
        {
            var result = await questionElementHelper.Create(customerId, model);

            switch (result.Status)
            {
                case QuestionElementActionStatus.AnswerSetNotExists:
                    return BadRequest(GlobalStrings.AnswerSet_NotFoundError);

                case QuestionElementActionStatus.AnswerSetCannotBeUsed:
                    return BadRequest(GlobalStrings.QuestionElement_AnswerSetCannotBeUsedError);

                case QuestionElementActionStatus.AnswerChoiceCannotBeUsed:
                    return BadRequest(GlobalStrings.QuestionElement_AnswerChoiceCannotBeUsedError);

                case QuestionElementActionStatus.InvalidScaleValueRange:
                    return BadRequest(GlobalStrings.QuestionElement_InvalidScaleRange);

                default:
                    return Created(
                        new Uri(Request.RequestUri, result.Content.ToString()),
                        new PostResponseDto<Guid> { Id = result.Content }
                    );
            }
        }

        /// <summary>
        /// Updates info about question element with provided id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/question-elements/{questionElementId:guid}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Question successfully updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in request.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Question with specified id not exists")]
        [InvalidateCacheOutput(nameof(GetQuestionElements), typeof(QuestionElementsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetQuestionElement), typeof(QuestionElementsController), "customerId", "questionElementId")]
        public async Task<IHttpActionResult> UpdateQuestionElement(
            int customerId,
            Guid questionElementId,
            UpdateQuestionElementRequestDto model
        )
        {
            var result = await questionElementHelper.Update(customerId, questionElementId, model);

            switch (result.Status)
            {
                case QuestionElementActionStatus.NotFound:
                    return NotFound(GlobalStrings.QuestionElement_NotFoundError);

                case QuestionElementActionStatus.AnswerSetNotExists:
                    return BadRequest(GlobalStrings.AnswerSet_NotFoundError);

                case QuestionElementActionStatus.AnswerSetCannotBeUsed:
                    return BadRequest(GlobalStrings.QuestionElement_AnswerSetCannotBeUsedError);

                case QuestionElementActionStatus.AnswerChoiceCannotBeUsed:
                    return BadRequest(GlobalStrings.QuestionElement_AnswerChoiceCannotBeUsedError);

                case QuestionElementActionStatus.InvalidScaleValueRange:
                    return BadRequest(GlobalStrings.QuestionElement_InvalidScaleRange);

                default:
                    return StatusCode(HttpStatusCode.NoContent);
            }
        }

        /// <summary>
        /// Creates or updates localized strings for question element.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/question-elements/{questionElementId:guid}/{language:regex(^([a-z]{2}|[a-z]{2}-[a-zA-Z]{2})$)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Question successfully updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in request.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Question with specified id not exists")]
        [InvalidateCacheOutput(nameof(GetQuestionElements), typeof(QuestionElementsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetQuestionElement), typeof(QuestionElementsController), "customerId", "questionElementId")]
        public async Task<IHttpActionResult> UpdateQuestionElementLocalizedStrings(
            int customerId,
            Guid questionElementId,
            UpdateQuestionElementLocalizedRequestDto model,
            string language
        )
        {
            var result = await questionElementHelper.UpdateLocalizedString(customerId, questionElementId, model);

            if (result == null)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes answer element with specified id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/question-elements/{questionElementId:guid}")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Question with specified id not exists")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Question successfully updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Question element is used")]
        [InvalidateCacheOutput(nameof(GetQuestionElements), typeof(QuestionElementsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetQuestionElement), typeof(QuestionElementsController), "customerId", "questionElementId")]
        public async Task<IHttpActionResult> DeleteQuestionElement(int customerId, Guid questionElementId)
        {
            var result = await questionElementHelper.Delete(customerId, questionElementId);

            switch (result)
            {
                case DeleteStatus.NotFound:
                    return NotFound(GlobalStrings.QuestionElement_NotFoundError);

                case DeleteStatus.InUse:
                    return BadRequest(GlobalStrings.QuestionElement_Delete_InUseError);

                default:
                    return StatusCode(HttpStatusCode.NoContent);
            }
        }

        /// <summary>
        /// Returns question element info for specified language or default.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <param name="language">The language.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/question-elements/{questionElementId:guid}")]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/question-elements/{questionElementId:guid}/{language:regex(^([a-z]{2}|[a-z]{2}-[a-zA-Z]{2})$)}")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Question with specified id not exists")]
        [SwaggerResponse(HttpStatusCode.OK, "Question element with specified id exists and accessible", typeof(QuestionElementResponseDto))]
        [ResponseType(typeof(QuestionElementResponseDto))]
        public async Task<IHttpActionResult> GetQuestionElement(
            int customerId,
            Guid questionElementId,
            string language = null,
            bool isBrief = true
        )
        {
            var result = await questionElementHelper.Get(customerId, questionElementId, isBrief);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Gets the question elements.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="language">The language.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/question-elements")]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/question-elements/{language:regex(^([a-z]{2}|[a-z]{2}-[a-zA-Z]{2})$)}")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Question with specified id not exists")]
        [SwaggerResponse(HttpStatusCode.OK, "Question element with specified id exists and accessible", typeof(PagedResultDto<QuestionElementResponseDto>))]
        [ResponseType(typeof(PagedResultDto<QuestionElementResponseDto>))]
        public async Task<IHttpActionResult> GetQuestionElements(
            int customerId,
            [FromUri]TagsSearchDto model,
            string language = null,
            bool isBrief = true
        )
        {
            var result = await questionElementHelper.Find(customerId, model, isBrief);

            return Ok(result);
        }
    }
}