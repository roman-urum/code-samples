using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Extensions;
using VitalsService.Web.Api.DataAnnotations;
using VitalsService.Web.Api.Filters;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.AssessmentMedias;

namespace VitalsService.Web.Api.Controllers
{
    /// <summary>
    /// Provides endpoints to manage assessments media.
    /// </summary>
    /// <summary>
    /// AlertsController.
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/assessment-media/{patientId:guid}")]
    public class AssessmentMediaController : BaseApiController
    {
        private readonly IAssessmentMediaControllerHelper assessmentMediaControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentMediaController"/> class.
        /// </summary>
        /// <param name="assessmentMediaControllerHelper">The assessment media controller helper.</param>
        public AssessmentMediaController(IAssessmentMediaControllerHelper assessmentMediaControllerHelper)
        {
            this.assessmentMediaControllerHelper = assessmentMediaControllerHelper;
        }

        /// <summary>
        /// Creates new record for assessment media without file.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput(nameof(GetAll), typeof(AssessmentMediaController), "customerId", "patientId")]
        [InvalidateCacheOutput(nameof(GetById), typeof(AssessmentMediaController), "customerId", "patientId")]
        public async Task<IHttpActionResult> CreateAssessmentMedia(
            int customerId, 
            Guid patientId,
            CreateAssessmentMediaRequestDto request
        )
        {
            var result = await assessmentMediaControllerHelper.CreateAssessmentMedia(customerId, patientId, request);

            return Ok(new PostResponseDto<Guid>(result));
        }

        /// <summary>
        /// Uploads file for existing assessment media without file.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("{assessmentMediaId:guid}")]
        [SwaggerAddFileUpload("FileContent", "Assessment media file", true)]
        [InvalidateCacheOutput(nameof(GetAll), typeof(AssessmentMediaController), "customerId", "patientId")]
        [InvalidateCacheOutput(nameof(GetById), typeof(AssessmentMediaController), "customerId", "patientId", "assessmentMediaId")]
        public async Task<IHttpActionResult> UpdateAssessmentMedia(
            int customerId,
            Guid patientId,
            Guid assessmentMediaId
        )
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest(UpdateAssessmentMediaStatus.InvalidContentProvided.Description());
            }

            var multipartDataProvider = await Request.Content.ReadAsMultipartAsync();
            var file = multipartDataProvider.Contents.FirstOrDefault();

            if (file == null)
            {
                return BadRequest(UpdateAssessmentMediaStatus.InvalidContentProvided.Description());
            }

            var result = await assessmentMediaControllerHelper.UpdateAssessmentMedia(
                customerId, patientId, assessmentMediaId, file);

            if (result.Status == UpdateAssessmentMediaStatus.NotFound)
            {
                return NotFound(result.Status.Description());
            }

            if (result.Status != UpdateAssessmentMediaStatus.Success)
            {
                return BadRequest(result.Status.Description());
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Returns assessment media by identifier.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="assessmentMediaId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{assessmentMediaId:guid}")]
        [ResponseType(typeof(AssessmentMediaResponseDto))]
        public async Task<IHttpActionResult> GetById(
            int customerId,
            Guid patientId,
            Guid assessmentMediaId
        )
        {
            var result = await
                assessmentMediaControllerHelper.GetAssessmentMediaById(customerId, patientId, assessmentMediaId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Returns all assessment media for patient with match criteria.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll(
            int customerId,
            Guid patientId, 
            [FromUri]AssessmentMediaSearchDto searchDto
        )
        {
            var results =
                await assessmentMediaControllerHelper.GetAllAssessmentMedia(customerId, patientId, searchDto);

            return Ok(results);
        }
    }
}