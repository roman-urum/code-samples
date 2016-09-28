using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Web.Api.Filters;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.MeasurementElements;
using HealthLibrary.Web.Api.Models.Enums;
using HealthLibrary.Web.Api.Resources;
using Swashbuckle.Swagger.Annotations;

namespace HealthLibrary.Web.Api.Controllers
{
    /// <summary>
    /// Provides endpoints to receive measurements by customer.
    /// </summary>
    [TokenAuthorize]
    public class MeasurementElementsController : BaseApiController
    {
        private readonly IMeasurementElementsControllerHelper measurementElementsHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementElementsController"/> class.
        /// </summary>
        /// <param name="measurementElementsHelper">The measurement elements helper.</param>
        public MeasurementElementsController(IMeasurementElementsControllerHelper measurementElementsHelper)
        {
            this.measurementElementsHelper = measurementElementsHelper;
        }

        /// <summary>
        /// Returns all measurement elements for required customer.
        /// </summary>
        /// <returns></returns>
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/measurement-elements")]
        [ResponseType(typeof(PagedResultDto<MeasurementResponseDto>))]
        [SwaggerResponse(HttpStatusCode.OK, "Returns list of measurement elements in response.", typeof(PagedResultDto<MeasurementResponseDto>))]
        public async Task<IHttpActionResult> Get(int customerId, [FromUri]TagsSearchDto request)
        {
            var result = await measurementElementsHelper.GetAll(customerId, request);

            return Ok(result);
        }

        /// <summary>
        /// Returns measurement element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="measurementElementId"></param>
        /// <returns></returns>
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/measurement-elements/{measurementElementId:guid}")]
        [SwaggerResponse(HttpStatusCode.OK, "Required measurement element exists.", typeof(MeasurementResponseDto))]
        public async Task<IHttpActionResult> Get(int customerId, Guid measurementElementId)
        {
            var result = await measurementElementsHelper.GetById(customerId, measurementElementId);

            if (result == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = GlobalStrings.Measurement_NotFoundError
                    });
            }

            return Ok(result);
        }
    }
}