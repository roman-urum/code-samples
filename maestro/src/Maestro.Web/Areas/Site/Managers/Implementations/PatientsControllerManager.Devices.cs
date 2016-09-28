using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.DevicesService;
using Maestro.Domain.Enums;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Helpers;

using MessagingToolkit.QRCode.Codec;

using WebGrease.Css.Extensions;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// PatientsControllerManager.Devices
    /// </summary>
    public partial class PatientsControllerManager
    {
        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<IList<DeviceDto>> GetDevices(Guid patientId)
        {
            var token = authDataStorage.GetToken();

            var devices = await devicesService.GetDevices(
                CustomerContext.Current.Customer.Id,
                patientId,
                token
            );

            devices = devices.OrderBy(d => d.Status.ToString()).ThenBy(d => d, new DeviceTypeComparer()).ToList();

            devices.ForEach(d => d.Settings.PinCode = string.Empty);

            return devices;
        }

        /// <summary>
        /// Generates the device qr code.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        public async Task<FileViewModel> GenerateDeviceQRCode(Guid deviceId)
        {
            var token = authDataStorage.GetToken();

            var device = await devicesService.GetDevice(
                CustomerContext.Current.Customer.Id,
                deviceId,
                token
            );

            if (device == null)
            {
                return null;
            }
            
            var qrEncoder = new QRCodeEncoder();

            Bitmap qrBitmap = qrEncoder.Encode(device.ActivationCode);

            ImageConverter converter = new ImageConverter();

            var content = (byte[])converter.ConvertTo(qrBitmap, typeof (byte[]));

            return new FileViewModel()
            {
                FileName = "QRCode.png",
                ContentType = "image/png",
                Content = content
            };
        }

        /// <summary>
        /// Creates the device.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateDevice(CreateDeviceRequestDto request)
        {
            var token = authDataStorage.GetToken();

            var patient = await this.patientsService.GetPatientAsync(CustomerContext.Current.Customer.Id, request.PatientId, true, token);
            request.BirthDate = patient.BirthDate;

            return await devicesService.CreateDevice(CustomerContext.Current.Customer.Id, request, token);
        }

        /// <summary>
        /// Removes the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        public async Task DeleteDevice(Guid deviceId)
        {
            var token = authDataStorage.GetToken();

            await devicesService.DeleteDevice(
                CustomerContext.Current.Customer.Id,
                deviceId,
                token
            );
        }

        public async Task UpdateDevices(List<DeviceDto> devices)
        {
            var token = this.authDataStorage.GetToken();
            List<Task> updateTasks = new List<Task>();
            foreach (var deviceDto in devices)
            {
                updateTasks.Add(this.devicesService.UpdateDevice(token, CustomerContext.Current.Customer.Id, deviceDto));               
            }

            await Task.WhenAll(updateTasks);
        }

        public async Task UpdateDevice(DeviceDto deviceDto)
        {
            var token = this.authDataStorage.GetToken();

            await this.devicesService.UpdateDevice(token, CustomerContext.Current.Customer.Id, deviceDto);
        }

        public async Task RequestDeviceDecomission(Guid deviceId)
        {
            var token = this.authDataStorage.GetToken();
            var deviceDecomissionDto = new UpdateDeviceDecomissionStatusDto()
                                           {
                                               Status = DecommissionStatusDto.Requested,
                                               DeviceId = deviceId
                                           };

            await this.devicesService.RequestDeviceDecomission(token, CustomerContext.Current.Customer.Id, deviceDecomissionDto);

        }
    }
}