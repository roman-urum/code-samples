using System;
using System.Collections.Generic;
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
    /// NotesController.
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/notes/{patientId:guid}")]
    public class NotesController : ApiController
    {
        private readonly INotesControllerHelper notesControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="notesControllerHelper">The notes controller helper.</param>
        public NotesController(INotesControllerHelper notesControllerHelper)
        {
            this.notesControllerHelper = notesControllerHelper;
        }

        /// <summary>
        /// Creates the patient note.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput(nameof(GetNotes), typeof(NotesController), "customerId", "patientId")]
        [InvalidateCacheOutput(nameof(GetPatientNoteNotables), typeof(NotesController), "customerId", "patientId")]
        [InvalidateCacheOutput(nameof(GetNote), typeof(NotesController), "customerId", "patientId")]
        public async Task<IHttpActionResult> CreateNote(
            int customerId,
            Guid patientId,
            NoteRequestDto request
        )
        {
            var result = await notesControllerHelper.CreateNote(customerId, patientId, request);

            if (!result.Status.HasFlag(NoteStatus.Success))
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
                result.Content
            );
        }

        /// <summary>
        /// Gets the patient note.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{noteId:guid}")]
        [ResponseType(typeof(NoteBriefResponseDto))]
        [SwaggerResponse(HttpStatusCode.OK, "Existing note.", typeof(NoteBriefResponseDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Note does not exist.")]
        public async Task<IHttpActionResult> GetNote(
            int customerId,
            Guid patientId,
            Guid noteId
        )
        {
            var result = await notesControllerHelper.GetNote(customerId, patientId, noteId);

            if (result.Status == NoteStatus.NotFound)
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
        /// Gets the patient notes.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(PagedResultDto<NoteBriefResponseDto>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with notes.", typeof(PagedResultDto<NoteBriefResponseDto>))]
        public async Task<IHttpActionResult> GetNotes(
            int customerId,
            Guid patientId,
            [FromUri]NotesSearchDto request
        )
        {
            var result = await notesControllerHelper.GetNotes(customerId, patientId, request);

            return Ok(result);
        }

        /// <summary>
        /// Gets the notables that are currently used by the patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("notables")]
        [ResponseType(typeof(IList<string>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with notables that are currently used by the patient.", typeof(IList<string>))]
        public async Task<IHttpActionResult> GetPatientNoteNotables(
            int customerId,
            Guid patientId
        )
        {
            var result = await notesControllerHelper.GetPatientNoteNotables(customerId, patientId);

            return Ok(result);
        }
    }
}