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
using HealthLibrary.Web.Api.Models.Enums;
using HealthLibrary.Web.Api.Models.Protocols;
using Swashbuckle.Swagger.Annotations;

namespace HealthLibrary.Web.Api.Controllers
{
    /// <summary>
    /// ProtocolsController.
    /// </summary>
    [TokenAuthorize]
    public class ProtocolsController : BaseApiController
    {
        private readonly IProtocolsControllerHelper protocolsControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolsController"/> class.
        /// </summary>
        /// <param name="protocolsControllerHelper">The protocols controller helper.</param>
        public ProtocolsController(IProtocolsControllerHelper protocolsControllerHelper)
        {
            this.protocolsControllerHelper = protocolsControllerHelper;
        }

        /// <summary>
        /// Creates the protocol (protocol name will be assigned to the default culture (customer or generic)).
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/protocols")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [SwaggerResponse(HttpStatusCode.Created, "New protocol was created.", typeof(PostResponseDto<Guid>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid parameter value(s).")]
        [InvalidateCacheOutput(nameof(GetProtocols), typeof(ProtocolsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetProtocol), typeof(ProtocolsController), "customerId")]
        public async Task<IHttpActionResult> CreateProtocol(
            int customerId,
            CreateProtocolRequestDto request
        )
        {
            var result = await protocolsControllerHelper.CreateProtocol(customerId, request);

            if (!result.Status.HasFlag(CreateUpdateProtocolStatus.Success))
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
        /// Updates the protocol (protocol name will be assigned to the default culture (customer or generic)).
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/protocols/{protocolId:guid}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Protocol was updated.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Protocol does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        [InvalidateCacheOutput(nameof(GetProtocols), typeof(ProtocolsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetProtocol), typeof(ProtocolsController), "customerId", "protocolId")]
        public async Task<IHttpActionResult> UpdateProtocol(
            int customerId,
            Guid protocolId,
            UpdateProtocolRequestDto request
        )
        {
            var status = await protocolsControllerHelper.UpdateProtocol(customerId, protocolId, request);

            if (status.HasFlag(CreateUpdateProtocolStatus.ProtocolWithSuchIdDoesNotExist))
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

            if (!status.HasFlag(CreateUpdateProtocolStatus.Success))
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
        /// Updates protocol's name with localization (depends on used language).
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/protocols/{protocolId:guid}/name/{language:regex(^([a-z]{2}|[a-z]{2}-[a-zA-Z]{2})$)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Protocol was updated.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Protocol does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        [InvalidateCacheOutput(nameof(GetProtocols), typeof(ProtocolsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetProtocol), typeof(ProtocolsController), "customerId", "protocolId")]
        public async Task<IHttpActionResult> UpdateProtocolLocalizedName(
            int customerId,
            Guid protocolId,
            UpdateProtocolLocalizedNameRequestDto request,
            string language = null
        )
        {
            var status = await protocolsControllerHelper
                .UpdateProtocolLocalizedName(customerId, protocolId, request.LocalizedName);

            if (status.HasFlag(CreateUpdateProtocolStatus.ProtocolWithSuchIdDoesNotExist))
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

            if (!status.HasFlag(CreateUpdateProtocolStatus.Success))
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
        /// Deletes the protocol.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/protocols/{protocolId:guid}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Protocol was deleted.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Protocol with such identifier does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        [InvalidateCacheOutput(nameof(GetProtocols), typeof(ProtocolsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetProtocol), typeof(ProtocolsController), "customerId", "protocolId")]
        public async Task<IHttpActionResult> DeleteProtocol(
            int customerId,
            Guid protocolId
        )
        {
            var status = await protocolsControllerHelper.DeleteProtocol(customerId, protocolId);

            if (status == DeleteProtocolStatus.NotFound)
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

            if (status == DeleteProtocolStatus.InUse)
            {
                return Content(
                    HttpStatusCode.BadRequest,
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
        /// Gets the protocol by identifier with specified or default culture.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="language">The language.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        [HttpGet]
        [CertificateAuthorize]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/protocols/{protocolId:guid}")]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/protocols/{protocolId:guid}/{language:regex(^([a-z]{2}|[a-z]{2}-[a-zA-Z]{2})$)}")]
        [ResponseType(typeof(ProtocolResponseDto))]
        [SwaggerResponse(HttpStatusCode.OK, "Existing protocol.", typeof(ProtocolResponseDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Protocol does not exist.")]
        public async Task<IHttpActionResult> GetProtocol(
            int customerId,
            Guid protocolId,
            string language = null,
            bool isBrief = true
        )
        {
            var result = await protocolsControllerHelper.GetProtocol(customerId, protocolId, isBrief);

            if (result.Status == GetProtocolStatus.NotFound)
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
        /// Gets the protocols using filters with specified or default culture.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="language">The language.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/protocols")]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/protocols/{language:regex(^([a-z]{2}|[a-z]{2}-[a-zA-Z]{2})$)}")]
        [ResponseType(typeof(PagedResultDto<ProtocolResponseDto>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with protocols.", typeof(PagedResultDto<ProtocolResponseDto>))]
        public async Task<IHttpActionResult> GetProtocols(
            int customerId,
            [FromUri]SearchProtocolDto request,
            string language = null,
            bool isBrief = true
        )
        {
            var result = await protocolsControllerHelper.GetProtocols(customerId, request, isBrief);

            return Ok(result);
        }
    }
}