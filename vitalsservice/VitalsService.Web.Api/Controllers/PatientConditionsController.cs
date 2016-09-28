using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Swagger.Annotations;
using VitalsService.Domain.Enums;
using VitalsService.Extensions;
using VitalsService.Web.Api.Filters;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Enums;
using VitalsService.Web.Api.Models.PatientConditions;

namespace VitalsService.Web.Api.Controllers
{
    /// <summary>
    /// PatientConditionsController.
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/patient-conditions/{patientId:guid}")]
    public class PatientConditionsController : ApiController
    {
        private readonly IPatientConditionsControllerHelper patientConditionsControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientConditionsController"/> class.
        /// </summary>
        /// <param name="patientConditionsControllerHelper">The patient conditions controller helper.</param>
        public PatientConditionsController(IPatientConditionsControllerHelper patientConditionsControllerHelper)
        {
            this.patientConditionsControllerHelper = patientConditionsControllerHelper;
        }

        /// <summary>
        /// Creates the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [InvalidateCacheOutput(nameof(GetPatientConditions), typeof(PatientConditionsController), "customerId", "patientId")]
        public async Task<IHttpActionResult> CreatePatientConditions(
            int customerId,
            Guid patientId,
            PatientConditionsRequest request
        )
        {
            var result = await patientConditionsControllerHelper
                .CreatePatientConditions(customerId, patientId, request);

            if (!result.HasFlag(CreateUpdatePatientConditionsStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.GetConcatString()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Updates the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [InvalidateCacheOutput(nameof(GetPatientConditions), typeof(PatientConditionsController), "customerId", "patientId")]
        public async Task<IHttpActionResult> UpdatePatientConditions(
            int customerId,
            Guid patientId,
            PatientConditionsRequest request
        )
        {
            var result = await patientConditionsControllerHelper
                .CreatePatientConditions(customerId, patientId, request);

            if (!result.HasFlag(CreateUpdatePatientConditionsStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.GetConcatString()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Gets the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IList<Guid>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with patient's conditions.", typeof(IList<Guid>))]
        public async Task<IHttpActionResult> GetPatientConditions(
            int customerId,
            Guid patientId
        )
        {
            var result = await patientConditionsControllerHelper.GetPatientConditions(customerId, patientId);

            return Ok(result);
        }
    }
}