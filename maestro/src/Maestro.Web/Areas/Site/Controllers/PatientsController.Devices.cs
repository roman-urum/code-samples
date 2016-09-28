using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maestro.Domain.Dtos.DevicesService;
using Maestro.Web.Areas.Customer;

namespace Maestro.Web.Areas.Site.Controllers
{
    /// <summary>
    /// PatientsController.Devices
    /// </summary>
    public partial class PatientsController
    {
        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        [ActionName("Devices")]
        public async Task<ActionResult> GetDevices(Guid patientId)
        {
            var devices = await patientsControllerManager.GetDevices(patientId);

            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Generates QR code for device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [CustomerAuthorize]
        public async Task<ActionResult> QRCode(Guid deviceId)
        {
            var qrCode = await patientsControllerManager.GenerateDeviceQRCode(deviceId);

            if (qrCode == null)
            {
                return new EmptyResult();
            }

            return File(qrCode.Content, qrCode.ContentType, qrCode.FileName);
        }

        /// <summary>
        /// Creates the device.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [CustomerAuthorize]
        [ActionName("Device")]
        public async Task<ActionResult> CreateDevice(CreateDeviceRequestDto request)
        {
            var result = await patientsControllerManager.CreateDevice(request);

            return Json(result);
        }

        /// <summary>
        /// Removes the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [CustomerAuthorize]
        [ActionName("Device")]
        public async Task<ActionResult> DeleteDevice(Guid deviceId)
        {
            await patientsControllerManager.DeleteDevice(deviceId);

            return Json(string.Empty);
        }
        
        [HttpPut]
        [CustomerAuthorize]
        [ActionName("Device")]
        public async Task<ActionResult> UpdateDevice(DeviceDto deviceDto)
        {
            await patientsControllerManager.UpdateDevice(deviceDto);

            return Json(string.Empty);
        }


        /// <summary>
        /// Updates the devices.
        /// </summary>
        /// <param name="devicesDtos">The devices dtos.</param>
        /// <returns></returns>
        [HttpPut]
        [CustomerAuthorize]
        [ActionName("Devices")]
        public async Task<ActionResult> UpdateDevices(List<DeviceDto> devicesDtos)
        {
            await patientsControllerManager.UpdateDevices(devicesDtos);

            return Json(string.Empty);
        }


        /// <summary>
        /// Requests the device decomission.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        [HttpPatch]
        [CustomerAuthorize]
        [ActionName("Device")]
        public async Task<ActionResult> RequestDeviceDecomission(Guid deviceId)
        {
            await patientsControllerManager.RequestDeviceDecomission(deviceId);

            return Json(deviceId);
        }
    }
}