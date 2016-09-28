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
using HealthLibrary.Web.Api.Models.Elements.Medias;
using HealthLibrary.Web.Api.Models.Enums;
using Swashbuckle.Swagger.Annotations;

namespace HealthLibrary.Web.Api.Controllers
{
    /// <summary>
    /// MediasController.
    /// </summary>
    [TokenAuthorize]
    public class MediasController : ApiController
    {
        private IMediaControllerHelper controllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediasController"/> class.
        /// </summary>
        /// <param name="controllerHelper">The controller helper.</param>
        public MediasController(IMediaControllerHelper controllerHelper)
        {
            this.controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Gets the media.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="mediaId">The media identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/medias/{mediaId:guid}")]
        [ResponseType(typeof(MediaResponseDto))]
        public async Task<IHttpActionResult> GetMedia(int customerId, Guid mediaId)
        {
            var resultMedia = await controllerHelper.GetMedia(customerId, mediaId);

            if (resultMedia == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = UpdateMediaStatus.NotFound.Description()
                    }
                );
            }

            return Ok(resultMedia);
        }

        /// <summary>
        /// Gets the medias.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/medias")]
        [ResponseType(typeof(PagedResultDto<MediaResponseDto>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with medias.", typeof(PagedResultDto<MediaResponseDto>))]
        public async Task<IHttpActionResult> GetMedias(
            int customerId,
            [FromUri] MediaSearchDto request
        )
        {
            var result = await controllerHelper.GetMedias(customerId, request);

            return Ok(result);
        }

        /// <summary>
        /// Creates the media.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/medias")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput(nameof(GetMedias), typeof(MediasController), "customerId")]
        [InvalidateCacheOutput(nameof(GetMedia), typeof(MediasController), "customerId")]
        public async Task<IHttpActionResult> CreateMedia(int customerId, CreateMediaRequestDto request)
        {
            var createResult = await controllerHelper.CreateMedia(customerId, request);

            if (createResult.Status != CreateMediaStatus.Success)
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = createResult.Status.Description()
                    }
                );
            }

            return Created(
                new Uri(Request.RequestUri, createResult.Content.ToString()),
                new PostResponseDto<Guid> { Id = createResult.Content }
            );
        }

        /// <summary>
        /// Updates the media.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="mediaId">The media identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/medias/{mediaId:guid}")]
        [InvalidateCacheOutput(nameof(GetMedias), typeof(MediasController), "customerId")]
        [InvalidateCacheOutput(nameof(GetMedia), typeof(MediasController), "customerId", "mediaId")]
        public async Task<IHttpActionResult> UpdateMedia(
            int customerId, 
            Guid mediaId,
            UpdateMediaRequestDto request
        )
        {
            var updateResult = await controllerHelper.UpdateMedia(customerId, mediaId, request);

            if (updateResult == UpdateMediaStatus.NotFound)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = UpdateMediaStatus.NotFound.Description()
                    });
            }

            if (updateResult != UpdateMediaStatus.Success)
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = updateResult.Description()
                    });
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes the media.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="mediaId">The media identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/medias/{mediaId:guid}")]
        [InvalidateCacheOutput(nameof(GetMedias), typeof(MediasController), "customerId")]
        [InvalidateCacheOutput(nameof(GetMedia), typeof(MediasController), "customerId", "mediaId")]
        public async Task<IHttpActionResult> DeleteMedia(int customerId, Guid mediaId)
        {
            var deleteResult = await controllerHelper.DeleteMedia(customerId, mediaId);

            if (deleteResult == DeleteMediaStatus.NotFound)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = DeleteMediaStatus.NotFound.Description()
                    });
            }

            if (deleteResult != DeleteMediaStatus.Success)
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = deleteResult.Description()
                    });
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}