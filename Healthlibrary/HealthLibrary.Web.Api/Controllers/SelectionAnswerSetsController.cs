using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.DomainLogic.Services.Results;
using HealthLibrary.Web.Api.Filters;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.SelectionAnswerSets;
using HealthLibrary.Web.Api.Models.Enums;
using HealthLibrary.Web.Api.Resources;
using Swashbuckle.Swagger.Annotations;

namespace HealthLibrary.Web.Api.Controllers
{
    /// <summary>
    /// CRUD Selection Answer-set Element
    /// </summary>
    [TokenAuthorize]
    public class SelectionAnswerSetsController : BaseApiController
    {
        private readonly ISelectionAnswerSetControllerHelper selectionAnswerSetHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionAnswerSetsController"/> class.
        /// </summary>
        /// <param name="selectionAnswerSetHelper">The selection answer set helper.</param>
        public SelectionAnswerSetsController(ISelectionAnswerSetControllerHelper selectionAnswerSetHelper)
        {
            this.selectionAnswerSetHelper = selectionAnswerSetHelper;
        }

        /// <summary>
        /// Creates new answer answerset with default answer strings.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/selection")]
        [SwaggerResponse(HttpStatusCode.OK, "New selection answerset was created.", typeof (PostResponseDto<Guid>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in request.")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput(nameof(GetSelectionAnswerSets), typeof(SelectionAnswerSetsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetSelectionAnswerSet), typeof(SelectionAnswerSetsController), "customerId")]
        public async Task<IHttpActionResult> CreateSelectionAnswerSet(
            int customerId,
            CreateSelectionAnswerSetRequestDto model
        )
        {
            Guid result = await selectionAnswerSetHelper.Create(customerId, model);

            return Created(
                new Uri(Request.RequestUri, result.ToString()),
                new PostResponseDto<Guid> { Id = result }
            );
        }

        /// <summary>
        /// Updates data of specified answer set and default answer strings.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/selection/{selectionAnswerSetId:guid}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Answerset data was updated.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Required answerset not exists.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in request.")]
        [InvalidateCacheOutput(nameof(GetSelectionAnswerSets), typeof(SelectionAnswerSetsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetSelectionAnswerSet), typeof(SelectionAnswerSetsController), "customerId", "selectionAnswerSetId")]
        [InvalidateCacheOutput(nameof(QuestionElementsController.GetQuestionElements), typeof(QuestionElementsController), "customerId")]
        [InvalidateCacheOutput(nameof(QuestionElementsController.GetQuestionElement), typeof(QuestionElementsController), "customerId")]
        public async Task<IHttpActionResult> UpdateSelectionAnswerSet(
            int customerId,
            Guid selectionAnswerSetId, 
            UpdateSelectionAnswerSetRequestDto model
        )
        {
            var result = await selectionAnswerSetHelper.Update(customerId, selectionAnswerSetId, model);

            switch (result.Status)
            {
                case SelectionAnswerSetUpdateResult.NotFound:
                    return Content(
                        HttpStatusCode.NotFound,
                        new ErrorResponseDto()
                        {
                            Error = ErrorCode.InvalidRequest,
                            Message = ErrorCode.InvalidRequest.Description(),
                            Details = GlobalStrings.SelectionAnswerSet_NotFoundError
                        });

                case SelectionAnswerSetUpdateResult.IncorrectAnswerId:
                    return Content(
                        HttpStatusCode.BadRequest,
                        new ErrorResponseDto()
                        {
                            Error = ErrorCode.InvalidRequest,
                            Message = ErrorCode.InvalidRequest.Description(),
                            Details = GlobalStrings.SelectionAnswerSet_IncorrectAnswerChoiceId
                        });

                default:
                    return StatusCode(HttpStatusCode.NoContent);
            }
        }

        /// <summary>
        /// Includes or updates additional localized strings for answerchoices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/selection/{selectionAnswerSetId:guid}/{language:maxlength(5)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Answerset data was updated.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Required answerset not exists.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in request.")]
        [InvalidateCacheOutput(nameof(GetSelectionAnswerSets), typeof(SelectionAnswerSetsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetSelectionAnswerSet), typeof(SelectionAnswerSetsController), "customerId", "selectionAnswerSetId")]
        [InvalidateCacheOutput(nameof(QuestionElementsController.GetQuestionElements), typeof(QuestionElementsController), "customerId")]
        [InvalidateCacheOutput(nameof(QuestionElementsController.GetQuestionElement), typeof(QuestionElementsController), "customerId")]
        public async Task<IHttpActionResult> UpdateSelectionAnswerSetLocalizedStrings(
            int customerId,
            Guid selectionAnswerSetId, 
            UpdateSelectionAnswerSetLocalizedRequestDto model, 
            string language
        )
        {
            var result = await selectionAnswerSetHelper.UpdateLocalizedStrings(customerId, selectionAnswerSetId, model);

            if (result.Status == ServiceActionResultStatus.DataNotFound)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = GlobalStrings.SelectionAnswerSet_NotFoundError
                    }
                );
            }

            if (result.Status == ServiceActionResultStatus.IncorrectData)
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = GlobalStrings.SelectionAnswerSet_IncorrectAnswerChoiceId
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes selection answer set with specified id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/selection/{selectionAnswerSetId:guid}")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Required answerset not exists.")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Answerset was deleted.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Answerset is used.")]
        [InvalidateCacheOutput(nameof(GetSelectionAnswerSets), typeof(SelectionAnswerSetsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetSelectionAnswerSet), typeof(SelectionAnswerSetsController), "customerId", "selectionAnswerSetId")]
        public async Task<IHttpActionResult> DeleteSelectionAnswerSet(int customerId, Guid selectionAnswerSetId)
        {
            var result = await selectionAnswerSetHelper.Delete(customerId, selectionAnswerSetId);

            switch (result)
            {
                case DeleteStatus.NotFound:
                    return Content(
                        HttpStatusCode.NotFound,
                        new ErrorResponseDto()
                        {
                            Error = ErrorCode.InvalidRequest,
                            Message = ErrorCode.InvalidRequest.Description(),
                            Details = GlobalStrings.AnswerSet_NotFoundError
                        }
                    );

                case DeleteStatus.InUse:
                    return Content(
                        HttpStatusCode.BadRequest,
                        new ErrorResponseDto()
                        {
                            Error = ErrorCode.InvalidRequest,
                            Message = ErrorCode.InvalidRequest.Description(),
                            Details = GlobalStrings.SelectionAnswerSet_Delete_InUseError
                        }
                    );

                default:
                    return StatusCode(HttpStatusCode.NoContent);
            }
        }

        /// <summary>
        /// Returns list of answersets by search criteria.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/selection")]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/selection/{language:maxlength(5)}")]
        [SwaggerResponse(HttpStatusCode.OK, "Response with appropriate answersets.",
            typeof(PagedResultDto<SelectionAnswerSetResponseDto>))]
        [ResponseType(typeof(PagedResultDto<SelectionAnswerSetResponseDto>))]
        public async Task<IHttpActionResult> GetSelectionAnswerSets(
            int customerId,
            [FromUri]SelectionAnswerSetSearchDto model,
            string language = null
        )
        {
            var result = await selectionAnswerSetHelper.Find(customerId, model);

            return Ok(result);
        }

        /// <summary>
        /// Returns answerset by id with specified culture or default.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="language">Required culture. Returns default if not specified.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/selection/{selectionAnswerSetId:guid}")]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/answer-sets/selection/{selectionAnswerSetId:guid}/{language:maxlength(5)}")]
        [SwaggerResponse(HttpStatusCode.OK, "Answerset exists.",
            typeof(SelectionAnswerSetResponseDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Required answerset not exists.")]
        [ResponseType(typeof(SelectionAnswerSetResponseDto))]
        public async Task<IHttpActionResult> GetSelectionAnswerSet(
            int customerId, 
            Guid selectionAnswerSetId, 
            string language = null
        )
        {
            var result = await selectionAnswerSetHelper.Get(customerId, selectionAnswerSetId, language);

            if (result == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = GlobalStrings.AnswerSet_NotFoundError
                    }
                );
            }

            return Ok(result);
        }
    }
}