using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using DeviceService.DomainLogic.Services.Interfaces;
using DeviceService.Web.Api.Helpers.Interfaces;
using System.Threading.Tasks;
using DeviceService.Web.Api.Models.Dtos.Entities;
using DeviceService.Web.Api.Models.Dtos.RequestsResponses;
using AutoMapper;
using DeviceService.Common.Security;
using DeviceService.Domain.Dtos;
using DeviceService.Domain.Dtos.Enums;
using DeviceService.Domain.Entities;
using DeviceService.Domain.Entities.Enums;
using DeviceService.Web.Api.Controllers;
using DeviceService.Web.Api.Models.Dtos.Enums;
using WebApi.OutputCache.V2;

#if !DEBUG
using System.Web;
using System.Web.Http;
using HttpConfigurationExtensions = WebApi.OutputCache.V2.HttpConfigurationExtensions;
#endif

namespace DeviceService.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// DevicesControllerHelper.
    /// </summary>
    public class DevicesControllerHelper : IDevicesControllerHelper
    {
        private readonly IDevicesService deviceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesControllerHelper"/> class.
        /// </summary>
        /// <param name="deviceService">The device service.</param>
        public DevicesControllerHelper(IDevicesService deviceService)
        {
            this.deviceService = deviceService;
        }

        /// <summary>
        /// Creates the device.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceDto">The device dto.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<CreateDeviceResponseDto, CreateDeviceStatus>> CreateDevice(int customerId,
            CreateDeviceRequestDto deviceDto)
        {
            var device = Mapper.Map<CreateDeviceRequestDto, Device>(deviceDto);
            device.CustomerId = customerId;

            var createDeviceResult = await deviceService.CreateDevice(device);

            if (createDeviceResult.Status != CreateDeviceStatus.Success)
            {
                return createDeviceResult.Clone<CreateDeviceResponseDto>();
            }

            var createDeviceResponse = Mapper.Map<Device, CreateDeviceResponseDto>(createDeviceResult.Content);

            return new OperationResultDto<CreateDeviceResponseDto, CreateDeviceStatus>()
            {
                Content = createDeviceResponse,
                Status = CreateDeviceStatus.Success
            };
        }

        /// <summary>
        /// Deletes the device.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        public async Task<DeleteDeviceStatus> DeleteDevice(int customerId, Guid deviceId)
        {
            return await this.deviceService.DeleteDevice(customerId, deviceId);
        }

        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<PagedResultDto<DeviceDto>, GetDeviceStatus>> GetDevices(int customerId, DevicesSearchDto request)
        {
            var result = await deviceService.GetDevices(customerId, request);

            return new OperationResultDto<PagedResultDto<DeviceDto>, GetDeviceStatus>()
            {
                Status = GetDeviceStatus.Success,
                Content = Mapper.Map<PagedResult<Device>, PagedResultDto<DeviceDto>>(result)
            };
        }

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<DeviceDto, GetDeviceStatus>> GetDevice(int customerId, Guid id)
        {
            var device = await this.deviceService.GetDevice(customerId, id);

            if (device == null) return new OperationResultDto<DeviceDto, GetDeviceStatus>() { Status = GetDeviceStatus.DeviceNotFound };

            return new OperationResultDto<DeviceDto, GetDeviceStatus>()
            {
                Content = Mapper.Map<DeviceDto>(device),
                Status = GetDeviceStatus.Success
            };
        }

        /// <summary>
        /// Updates the device.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<UpdateDeviceStatus> UpdateDevice(int customerId, Guid deviceId, BaseDeviceRequestDto request)
        {
            Device device = Mapper.Map<BaseDeviceRequestDto, Device>(request);
            device.Id = deviceId;
            device.CustomerId = customerId;

            var updateDeviceResult = await deviceService.UpdateDevice(device);

            return updateDeviceResult;
        }

        /// <summary>
        /// Activates the device.
        /// </summary>
        /// <param name="activationData">The activation data.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<ActivationResponseDto, ActivateDeviceStatus>> ActivateDevice(ActivationDto activationData)
        {
            Device device = await deviceService.GetDevice(activationData.ActivationCode, DateTime.Parse(activationData.BirthDate));

            if (device == null)
            {
                return new OperationResultDto<ActivationResponseDto, ActivateDeviceStatus>()
                {
                    Status = ActivateDeviceStatus.DeviceNotFound
                };
            }

            X509Certificate2 certificate = GenerateClientCertificate(activationData.Certificate, device.Id);

            if (certificate == null)
            {
                return new OperationResultDto<ActivationResponseDto, ActivateDeviceStatus>()
                {
                    Status = ActivateDeviceStatus.CertificateSigningRequestInvalid
                };
            }

            var activation = Mapper.Map<Activation>(activationData);
            activation.Certificate = Convert.ToBase64String(certificate.RawData);
            activation.Thumbprint = certificate.Thumbprint;

            var activationResult = await deviceService.ActivateDevice(activation);

            if (activationResult.Status != ActivateDeviceStatus.Success)
            {
                return new OperationResultDto<ActivationResponseDto, ActivateDeviceStatus>()
                {
                    Status = activationResult.Status
                };
            }

            var activationResponseDto = Mapper.Map<ActivationResponseDto>(activationResult.Content);
            activationResponseDto.Certificate = Convert.ToBase64String(certificate.GetRawCertData());

            return new OperationResultDto<ActivationResponseDto, ActivateDeviceStatus>()
            {
                Status = ActivateDeviceStatus.Success,
                Content = activationResponseDto
            };
        }


        /// <summary>
        /// Converts string to bytes array and generates client certificate.
        /// </summary>
        /// <param name="csrBase64String">CSR as base-64 string.</param>
        /// <param name="deviceId">Id of device record in db.</param>
        /// <returns></returns>
        private static X509Certificate2 GenerateClientCertificate(string csrBase64String, Guid deviceId)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(csrBase64String);

                return SertificateGenerator.SignRequest(bytes, deviceId.ToString());
            }
            catch (FormatException)
            {
                return null;
            }
            catch (InvalidCastException)
            {
                return null;
            }
        }

        /// <summary>
        /// Set decommission status of device
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<SetDecomissionStatusOperationStatus> SetDeviceDecommissionStatus(int customerId, Guid id,
            DecommissionStatusDto status)
        {
            var setDecomissionStatusResult = await deviceService.SetDecomissionStatus(customerId, id, (Status) status);

            return setDecomissionStatusResult.Status;
        }

        /// <summary>
        /// Clears the devices. Decommission activated devices and delete decommissioned devices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task ClearDevices(int customerId, Guid patientId, HttpRequestMessage request)
        {
            var devices = await deviceService.ClearDevices(customerId, patientId);

            if (devices.Any())
            {
#if !DEBUG
                InvalidatePatientDevicesCache(
                    customerId,
                    devices.Select(d => d.Id).ToList(),
                    request
                );
#endif
            }
        }

        private void InvalidatePatientDevicesCache(int customerId, IList<Guid> deviceIds, HttpRequestMessage request)
        {
            var cacheOutputConfiguration = request.GetConfiguration().CacheOutputConfiguration();
            var cacheOutputProvider = cacheOutputConfiguration.GetCacheOutputProvider(request);

            foreach (var deviceId in deviceIds)
            {
                var cacheKey1 = cacheOutputConfiguration.MakeBaseCachekey(typeof(DevicesController).FullName, "GetDevice");
                var cacheKey2 = cacheOutputConfiguration.MakeBaseCachekey(typeof(DevicesController).FullName, "QRCode");

                cacheOutputProvider.RemoveStartsWith(string.Format("{0}-customerId={1}&deviceId={2}", cacheKey1, customerId, deviceId));
                cacheOutputProvider.RemoveStartsWith(string.Format("{0}-customerId={1}&deviceId={2}", cacheKey2, customerId, deviceId));
            }
        }
    }
}