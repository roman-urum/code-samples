using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.Description;
using DeviceService.Common.Extensions;
using DeviceService.Web.Api.Filters;
using DeviceService.Web.Api.Helpers.Interfaces;
using DeviceService.Web.Api.Models.Dtos.Entities;
using DeviceService.Web.Api.Models.Dtos.RequestsResponses;
using System.IO;
using System.Net.Http.Headers;
using DeviceService.Domain.Dtos;
using DeviceService.Domain.Dtos.Enums;
using DeviceService.Web.Api.Models.Dtos.Enums;

namespace DeviceService.Web.Api.Controllers
{
    /// <summary>
    /// DevicesController.
    /// </summary>
    public class DevicesController : ApiController
    {
        private readonly IDevicesControllerHelper devicesControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="devicesControllerHelper">The devices controller helper.</param>
        public DevicesController(IDevicesControllerHelper devicesControllerHelper)
        {
            this.devicesControllerHelper = devicesControllerHelper;
        }

        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/devices")]
        [ResponseType(typeof(PagedResultDto<DeviceDto>))]
        [TokenAuthorize]
        public async Task<IHttpActionResult> GetDevices(
            int customerId,
            [FromUri]DevicesSearchDto request
        )
        {
            var operationResult = await devicesControllerHelper.GetDevices(customerId, request);

            if (!operationResult.Status.HasFlag(GetDeviceStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Status.Description()
                    }
                );   
            }

            return Ok(operationResult.Content);
        }

        /// <summary>
        /// Creates the device.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [TokenAuthorize]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/devices")]
        [ResponseType(typeof(CreateDeviceResponseDto))]
        [InvalidateCacheOutput("GetDevices", typeof(DevicesController))]
        public async Task<IHttpActionResult> CreateDevice(
            int customerId,
            CreateDeviceRequestDto request
        )
        {
            var operationResult = await devicesControllerHelper.CreateDevice(customerId, request);

            if (operationResult.Status != CreateDeviceStatus.Success)
            {
                return Content(
                    HttpStatusCode.BadRequest, 
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Status.Description()
                    }
                );                 
            }

            return Created(
                new Uri(Request.RequestUri, operationResult.Content.Id.ToString()),
                new PostResponseDto<Guid> { Id = operationResult.Content.Id }
            );
        }

        /// <summary>
        /// Updates the device.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [TokenAuthorize]
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/devices/{deviceId:guid}")]
        [InvalidateCacheOutput("GetDevices", typeof(DevicesController))]
        [InvalidateCacheOutput("GetDevice", typeof(DevicesController), "customerId", "deviceId")]
        [InvalidateCacheOutput("QRCode", typeof(DevicesController), "customerId", "deviceId")]
        public async Task<IHttpActionResult> UpdateDevice(
            int customerId,
            Guid deviceId,
            BaseDeviceRequestDto request
        )
        {
            var result = await devicesControllerHelper.UpdateDevice(customerId ,deviceId, request);

            if (result == UpdateDeviceStatus.DeviceNotFound)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.GetConcatString()
                    }
                );
            }

            if (result != UpdateDeviceStatus.Success)
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
        /// Deletes the specified device.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [TokenAuthorize]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/devices/{deviceId:guid}")]
        [InvalidateCacheOutput("GetDevices", typeof(DevicesController))]
        [InvalidateCacheOutput("GetDevice", typeof(DevicesController), "customerId", "deviceId")]
        [InvalidateCacheOutput("QRCode", typeof(DevicesController), "customerId", "deviceId")]
        public async Task<IHttpActionResult> DeleteDevice(
            int customerId,
            Guid deviceId
        )
        {
            var operationResult = await devicesControllerHelper.DeleteDevice(customerId, deviceId);

            if (operationResult.HasFlag(DeleteDeviceStatus.DeviceNotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Description()
                    }
                );                   
            }

            if (!operationResult.HasFlag(DeleteDeviceStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Description()
                    }
                );                 
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Gets QR Code.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/devices/qr-code/{deviceId:guid}")]
        [ResponseType(typeof(byte[]))]
        public async Task<HttpResponseMessage> QRCode(
            int customerId,
            Guid deviceId
        )
        {
            var operationResult = await devicesControllerHelper.GetDevice(customerId, deviceId);

            if (operationResult.Status.HasFlag(GetDeviceStatus.DeviceNotFound))
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var ms = new MemoryStream(operationResult.Content.QRCode);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(ms)
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

            return response;
        }

        /// <summary>
        /// Gets the device by identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [TokenAuthorize]
        [CertificateAuthorize]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/devices/{deviceId:guid}")]
        [ResponseType(typeof(DeviceDto))]
        public async Task<IHttpActionResult> GetDevice(
            int customerId,
            Guid deviceId
        )
        {
            var operationResult = await devicesControllerHelper.GetDevice(customerId, deviceId);

            if (operationResult.Status.HasFlag(GetDeviceStatus.DeviceNotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Status.Description()
                    }
                );                  
            }

            if (!operationResult.Status.HasFlag(GetDeviceStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Status.Description()
                    }
                );      
            }

            return Ok(operationResult.Content);
        }

        /// <summary>
        /// Posts the specified activation data.
        /// </summary>
        /// <param name="activationData">The activation data.</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/activation")]
        [ResponseType(typeof(ActivationResponseDto))]
        [InvalidateCacheOutput("GetDevices", typeof(DevicesController))]
        [InvalidateCacheOutput("GetDevice", typeof(DevicesController), "customerId", "deviceId")]
        [InvalidateCacheOutput("QRCode", typeof(DevicesController), "customerId", "deviceId")]
        public async Task<IHttpActionResult> Activation(ActivationDto activationData)
        {
            var operationResult = await devicesControllerHelper.ActivateDevice(activationData);

            if (operationResult.Status.HasFlag(ActivateDeviceStatus.DeviceNotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Status.Description()
                    }
                );  
            }

            if (!operationResult.Status.HasFlag(ActivateDeviceStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Status.Description()
                    }
                );                  
            }

            return Ok(operationResult.Content);
        }

        /// <summary>
        /// Request device decomission
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/decommission/{deviceId:guid}")]
        [TokenAuthorize]
        [CertificateAuthorize]
        [InvalidateCacheOutput("GetDevices", typeof(DevicesController))]
        [InvalidateCacheOutput("GetDevice", typeof(DevicesController), "customerId", "deviceId")]
        [InvalidateCacheOutput("QRCode", typeof(DevicesController), "customerId", "deviceId")]
        public async Task<IHttpActionResult> Decomission(int customerId,  Guid deviceId, UpdateDeviceDecommissionStatusDto model)
        {
            var operationResult = await devicesControllerHelper.SetDeviceDecommissionStatus(customerId, deviceId, model.Status);

            if (operationResult.HasFlag(SetDecomissionStatusOperationStatus.DeviceNotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Description()
                    }
                );
            }

            if (!operationResult.HasFlag(SetDecomissionStatusOperationStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = operationResult.Description()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Clears the devices. Decommission activated devices and delete decommissioned devices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/devices/{patientId:guid}/clear")]
        [InvalidateCacheOutput("GetDevices", typeof(DevicesController))]
        public async Task<IHttpActionResult> ClearDevices(int customerId, Guid patientId)
        {
            await devicesControllerHelper.ClearDevices(customerId, patientId, Request);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}