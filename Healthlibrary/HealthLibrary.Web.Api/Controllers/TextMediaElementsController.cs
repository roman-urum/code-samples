using System.Threading.Tasks;
using System.Web.Http;
using HealthLibrary.Web.Api.Filters;
using System;
using System.Net;
using System.Web.Http.Description;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models.Enums;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.TextMediaElements;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Controllers
{
    /// <summary>
    /// CRUD Selection Answer-set Element
    /// </summary>
    [TokenAuthorize]
    public class TextMediaElementsController : ApiController
    {
        private readonly ITextMediaElementsControllerHelper controllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextMediaElementsController"/> class.
        /// </summary>
        /// <param name="controllerHelper">The controller helper.</param>
        public TextMediaElementsController(ITextMediaElementsControllerHelper controllerHelper)
        {
            this.controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Search text and media elements
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchModel">The search model.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/text-media-elements")]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/text-media-elements/{language:regex(^([a-z]{2}|[a-z]{2}-[a-zA-Z]{2})$)}")]
        [ResponseType(typeof(PagedResultDto<TextMediaElementResponseDto>))]
        public async Task<IHttpActionResult> GetTextMediaElements(
            int customerId, 
            [FromUri]TextMediaElementSearchDto searchModel, 
            string language = null
        )
        {
            var elements = await controllerHelper.GetElements(customerId, searchModel);

            return Ok(elements);
        }

        /// <summary>
        /// Get specific text and media element by id
        /// </summary>
        /// <param name="customerId">cyustomer id</param>
        /// <param name="textMediaElementId">The text media element identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/text-media-elements/{textMediaElementId:guid}")]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/text-media-elements/{textMediaElementId:guid}/{language:regex(^([a-z]{2}|[a-z]{2}-[a-zA-Z]{2})$)}")]
        [ResponseType(typeof(TextMediaElementResponseDto))]
        public async Task<IHttpActionResult> GetTextMediaElement(
            int customerId, 
            Guid textMediaElementId, 
            string language = null
        )
        {
            var element = await this.controllerHelper.GetElement(customerId, textMediaElementId);

            if (element == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = GlobalStrings.TextMediaElement_NotFound
                    }
                );
            }

            return Ok(element);
        }

        /// <summary>
        /// Creates new TextMediaElement
        /// </summary>
        /// <param name="customerId">customer id (optional)</param>
        /// <param name="newTextMediaElementDto">TextMEdiaElement data</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/text-media-elements")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput(nameof(GetTextMediaElements), typeof(TextMediaElementsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetTextMediaElement), typeof(TextMediaElementsController), "customerId")]
        public async Task<IHttpActionResult> CreateTextMediaElement(
            int customerId,
            CreateTextMediaElementRequestDto newTextMediaElementDto
        )
        {
            var createResult = await controllerHelper.Create(customerId, newTextMediaElementDto);

            if (!createResult.Status.HasFlag(CreateTextMediaElementStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = createResult.Status.Description()
                    });
            }

            return Created(
                new Uri(Request.RequestUri, createResult.Content.ToString()),
                new { Id = createResult.Content }
            );
        }
        /// <summary>
        /// Updates TextMedia Element and it's default localization
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="textMediaElementId">The text media element identifier.</param>
        /// <param name="textMediaElementDto">The text media element dto.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/text-media-elements/{textMediaElementId:guid}")]
        [InvalidateCacheOutput(nameof(GetTextMediaElements), typeof(TextMediaElementsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetTextMediaElement), typeof(TextMediaElementsController), "customerId", "textMediaElementId")]
        public async Task<IHttpActionResult> UpdateTextMediaElement(
            int customerId,
            Guid textMediaElementId, 
            UpdateTextMediaElementRequestDto textMediaElementDto
        )
        {
            var result = await this.controllerHelper.Update(customerId, textMediaElementId, textMediaElementDto);

            if (result == UpdateTextMediaElementStatus.NotFound)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = UpdateTextMediaElementStatus.NotFound.Description()
                    }
                );
            }

            if (result != UpdateTextMediaElementStatus.Success)
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Description()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Updates TextMediaElement's localization
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="textMediaElementId">The text media element identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/text-media-elements/{textMediaElementId:guid}/{language:regex(^([a-z]{2}|[a-z]{2}-[a-zA-Z]{2})$)}")]
        [InvalidateCacheOutput(nameof(GetTextMediaElements), typeof(TextMediaElementsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetTextMediaElement), typeof(TextMediaElementsController), "customerId", "textMediaElementId")]
        public async Task<IHttpActionResult> UpdateTextMediaElementLocalization(
            int customerId, 
            Guid textMediaElementId,
            UpdateTextMediaElementLocalizedRequestDto request,
            string language = null
        )
        {
            var result = await controllerHelper.UpdateLocalization(customerId, textMediaElementId, request);

            if (result == UpdateTextMediaElementStatus.NotFound)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = UpdateTextMediaElementStatus.NotFound.Description()
                    }
                );
            }

            if (result != UpdateTextMediaElementStatus.Success)
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Description()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Soft delete element
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="textMediaElementId">The text media element identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/text-media-elements/{textMediaElementId:guid}")]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/text-media-elements/{textMediaElementId:guid}/{language:regex(^([a-z]{2}|[a-z]{2}-[a-zA-Z]{2})$)}")]
        [InvalidateCacheOutput(nameof(GetTextMediaElements), typeof(TextMediaElementsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetTextMediaElement), typeof(TextMediaElementsController), "customerId", "textMediaElementId")]
        public async Task<IHttpActionResult> DeleteTextMediaElement(
            int customerId, 
            Guid textMediaElementId,
            string language = null
        )
        {
            var result = await controllerHelper.Delete(customerId, textMediaElementId);

            if (result.HasFlag(DeteleTextMediaElementStatus.NotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = DeteleTextMediaElementStatus.NotFound.Description()
                    }
                );
            }

            if (result != DeteleTextMediaElementStatus.Success)
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Description()
                    }
                );

            return StatusCode(HttpStatusCode.NoContent);
        } 
    }
}