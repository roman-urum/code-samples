using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Extensions;
using VitalsService.Web.Api.Filters;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Enums;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Controllers
{
    /// <summary>
    /// VitalsController.
    /// </summary>
    [TokenAuthorize]
    [PublicKeyPins]
    public class VitalsController : ApiController
    {
        private readonly IVitalsControllerHelper vitalsControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="VitalsController" /> class.
        /// </summary>
        /// <param name="vitalsControllerHelper">The vitals controller helper.</param>
        public VitalsController(IVitalsControllerHelper vitalsControllerHelper)
        {
            this.vitalsControllerHelper = vitalsControllerHelper;
        }

        /// <summary>
        /// Returns list of all saved vitals for specified patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/vitals/{patientId:guid}")]
        [CertificateAuthorize]
        [ResponseType(typeof(PagedResultDto<MeasurementResponseDto>))]
        public async Task<IHttpActionResult> GetVitals(
            int customerId,
            Guid patientId,
            [FromUri]MeasurementsSearchDto filter
        )
        {
            var results = await vitalsControllerHelper.GetVitals(customerId, patientId, filter);

            return Ok(results);
        }

        /// <summary>
        /// Returns vital by measurement id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="measurementId">The measurement identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/vitals/{patientId:guid}/{measurementId:guid}")]
        [CertificateAuthorize]
        [ResponseType(typeof(MeasurementResponseDto))]
        public async Task<IHttpActionResult> GetVital(
            int customerId,
            Guid patientId,
            Guid measurementId
        )
        {
            var vital = await vitalsControllerHelper.GetVital(customerId, patientId, measurementId);

            if (vital == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = GlobalStrings.Measurement_NotFound
                    }
                );
            }

            return Ok(vital);
        }

        /// <summary>
        /// Creates new vital for patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/vitals/{patientId:guid}")]
        [CertificateAuthorize]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput(nameof(GetVitals), typeof(VitalsController), "customerId", "patientId")]
        [InvalidateCacheOutput(nameof(GetVital), typeof(VitalsController), "customerId", "patientId")]
        public async Task<IHttpActionResult> CreateVital(
            int customerId, 
            Guid patientId,
            MeasurementRequestDto request
        )
        {
            var result = await vitalsControllerHelper.CreateVital(customerId, patientId, request);

            if (result.Status.HasFlag(CreateMeasurementStatus.MeasurementWithClientIdAlreadyExists))
            {
                return Content(
                    HttpStatusCode.Conflict,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = $"{result.Status.Description()} (provided ClientId: {request.ClientId})"
                    }
                );
            }

            return Created(
                new Uri(Request.RequestUri, result.Content.ToString()),
                result.Content
            );
        }

        /// <summary>
        /// Updates data of vital.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="measurementId">The measurement identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/vitals/{patientId:guid}/{measurementId:guid}")]
        [ResponseType(typeof(MeasurementResponseDto))]
        [InvalidateCacheOutput(nameof(GetVitals), typeof(VitalsController), "customerId", "patientId")]
        [InvalidateCacheOutput(nameof(GetVital), typeof(VitalsController), "customerId", "patientId", "measurementId")]
        [InvalidateCacheOutput("GetHealthSession", typeof(HealthSessionsController), "customerId", "patientId")]
        [InvalidateCacheOutput("GetHealthSessions", typeof(HealthSessionsController), "customerId", "patientId")]
        public async Task<IHttpActionResult> UpdateVital(
            int customerId,
            Guid patientId,
            Guid measurementId,
            UpdateMeasurementRequestDto request
        )
        {
            var result = await vitalsControllerHelper.UpdateVital(customerId, patientId, measurementId, request);

            if (result.HasFlag(UpdateMeasurementStatus.MeasurementNotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = UpdateMeasurementStatus.MeasurementNotFound.Description()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}