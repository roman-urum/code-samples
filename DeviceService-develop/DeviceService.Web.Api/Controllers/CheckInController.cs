using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using DeviceService.Common.Extensions;
using DeviceService.Domain.Dtos.Enums;
using DeviceService.Web.Api.Filters;
using DeviceService.Web.Api.Helpers.Interfaces;
using DeviceService.Web.Api.Models.Dtos.Enums;
using DeviceService.Web.Api.Models.Dtos.RequestsResponses;

namespace DeviceService.Web.Api.Controllers
{
    /// <summary>
    /// Controller to handle check in requests.
    /// </summary>
    public class CheckInController : ApiController
    {
        private readonly ICheckInControllerHelper checkInControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckInController"/> class.
        /// </summary>
        /// <param name="checkInControllerHelper">The check in controller helper.</param>
        public CheckInController(ICheckInControllerHelper checkInControllerHelper)
        {
            this.checkInControllerHelper = checkInControllerHelper;
        }

        /// <summary>
        /// Handles requests to update device last connected utc.
        /// </summary>
        /// <returns></returns>
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/devices/{deviceId:guid}/checkin")]
        [HttpPut]
        [TokenAuthorize]
        [CertificateAuthorize]
        [InvalidateCacheOutput("GetDevices", typeof(DevicesController))]
        [InvalidateCacheOutput("GetDevice", typeof(DevicesController), "customerId", "deviceId")]
        [InvalidateCacheOutput("QRCode", typeof(DevicesController), "customerId", "deviceId")]
        public async Task<IHttpActionResult> CheckIn(
            int customerId,
            Guid deviceId,
            string deviceTz = null
        )
        {
            if (!await checkInControllerHelper.UpdateLastConnectedUtc(customerId, deviceId, deviceTz))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = UpdateDeviceStatus.DeviceNotFound.Description()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}