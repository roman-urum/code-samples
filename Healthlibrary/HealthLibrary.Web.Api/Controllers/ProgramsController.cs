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
using HealthLibrary.Web.Api.Models.Programs;

namespace HealthLibrary.Web.Api.Controllers
{
    /// <summary>
    /// ProgramsControllerю
    /// </summary>
    [TokenAuthorize]
    public class ProgramsController : ApiController
    {
        private readonly IProgramsControllerHelper controllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramsController"/> class.
        /// </summary>
        /// <param name="controllerHelper">The controller helper.</param>
        public ProgramsController(IProgramsControllerHelper controllerHelper)
        {
            this.controllerHelper = controllerHelper;
        }

        /// <summary>
        /// Gets the program.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">The program identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/programs/{programId:guid}")]
        [ResponseType(typeof (ProgramBriefResponseDto))]
        public async Task<IHttpActionResult> GetProgram(
            int customerId,
            Guid programId,
            bool isBrief = true
        )
        {
            var programResult = await controllerHelper.GetProgram(customerId, programId, isBrief);

            if (programResult.Status == GetProgramStatus.Success)
            {
                return Ok(programResult.Content);
            }

            return Content(
                HttpStatusCode.NotFound,
                new ErrorResponseDto()
                {
                    Error = ErrorCode.InvalidRequest,
                    Message = ErrorCode.InvalidRequest.Description(),
                    Details = programResult.Status.Description()
                }
            );
        }

        /// <summary>
        /// Gets the programs.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchProgram">The search program.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/programs")]
        [ResponseType(typeof (PagedResultDto<ProgramBriefResponseDto>))]
        public async Task<IHttpActionResult> GetPrograms(
            int customerId,
            [FromUri]SearchProgramDto searchProgram = null,
            bool isBrief = true
        )
        {
            var findProgramsResult = await controllerHelper.FindPrograms(customerId, searchProgram, isBrief);

            return Ok(findProgramsResult);
        }

        /// <summary>
        /// Create programs.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/programs")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput(nameof(GetPrograms), typeof(ProgramsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetProgram), typeof(ProgramsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetProgramSchedule), typeof(ProgramsController), "customerId")]
        public async Task<IHttpActionResult> CreateProgram(
            int customerId,
            ProgramRequestDto request
        )
        {
            var result = await controllerHelper.CreateProgram(customerId, request);

            if (result.Status != CreateUpdateProgramStatus.Success)
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Status.Description()
                    }
                );
            }

            return Created(
                new Uri(Request.RequestUri, result.Content.Id.ToString()),
                new PostResponseDto<Guid> { Id = result.Content.Id }
            );
        }

        /// <summary>
        /// Updates the program.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">The program identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/programs/{programId:guid}")]
        [ResponseType(typeof(CreateUpdateProgramStatus))]
        [InvalidateCacheOutput(nameof(GetPrograms), typeof(ProgramsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetProgram), typeof(ProgramsController), "customerId", "programId")]
        [InvalidateCacheOutput(nameof(GetProgramSchedule), typeof(ProgramsController), "customerId", "programId")]
        public async Task<IHttpActionResult> UpdateProgram(int customerId, Guid programId, ProgramRequestDto request)
        {
            var updateResult = await controllerHelper.UpdateProgram(customerId, programId, request);

            if (updateResult.HasFlag(CreateUpdateProgramStatus.NotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = CreateUpdateProgramStatus.NotFound.Description()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes the program.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">The program identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/programs/{programId:guid}")]
        [ResponseType(typeof(DeleteProgramStatus))]
        [InvalidateCacheOutput(nameof(GetPrograms), typeof(ProgramsController), "customerId")]
        [InvalidateCacheOutput(nameof(GetProgram), typeof(ProgramsController), "customerId", "programId")]
        [InvalidateCacheOutput(nameof(GetProgramSchedule), typeof(ProgramsController), "customerId", "programId")]
        public async Task<IHttpActionResult> DeleteProgram(int customerId, Guid programId)
        {
            var deleteResult = await controllerHelper.DeleteProgram(customerId, programId);

            if (deleteResult.HasFlag(DeleteProgramStatus.NotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = DeleteProgramStatus.NotFound.Description()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Returns a schedule based on the program.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="programId">The program identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/programs/{programId:guid}/schedule")]
        [ResponseType(typeof(ProgramScheduleDto))]
        public async Task<IHttpActionResult> GetProgramSchedule(
            int customerId,
            Guid programId,
            [FromUri]ProgramScheduleRequestDto model = null
        )
        {
            if (model == null)
            {
                model = new ProgramScheduleRequestDto();
            }

            var result = await controllerHelper.GetProgramSchedule(customerId, programId, model);

            if (result == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = DeleteProgramStatus.NotFound.Description()
                    }
                );
            }

            return Ok(result);
        }
    }
}