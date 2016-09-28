using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.DevicesService;
using Maestro.DomainLogic.Services.Interfaces;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// DevicesService.
    /// </summary>
    public class DevicesService : IDevicesService
    {
        private readonly IDevicesDataProvider devicesDataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesService"/> class.
        /// </summary>
        /// <param name="devicesDataProvider">The devices data provider.</param>
        public DevicesService(IDevicesDataProvider devicesDataProvider)
        {
            this.devicesDataProvider = devicesDataProvider;
        }

        /// <summary>
        /// Creates the specified create device dto.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateDevice(int customerId, CreateDeviceRequestDto request, string token)
        {
            return await devicesDataProvider.CreateDevice(customerId, request, token);
        }

        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<IList<DeviceDto>> GetDevices(int customerId, Guid patientId, string token)
        {
            return await devicesDataProvider.GetDevices(customerId, patientId, token);
        }

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<DeviceDto> GetDevice(int customerId, Guid deviceId, string token)
        {
            return await devicesDataProvider.GetDevice(customerId, deviceId, token);
        }

        /// <summary>
        /// Removes the device.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task DeleteDevice(int customerId, Guid deviceId, string token)
        {
            await devicesDataProvider.DeleteDevice(customerId, deviceId, token);
        }

        public async Task UpdateDevice(string token, int customerId, DeviceDto device)
        {
            await devicesDataProvider.UpdateDevice(token, customerId, device);
        }

        public async Task RequestDeviceDecomission(string token, int customerId, UpdateDeviceDecomissionStatusDto deviceDecomissionDto)
        {
            await devicesDataProvider.RequestDeviceDecomission(token, customerId, deviceDecomissionDto);            
        }

        /// <summary>
        /// Clears the devices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="authToken">The authentication token.</param>
        /// <returns></returns>
        public async Task ClearDevices(int customerId, Guid patientId, string authToken)
        {
            await devicesDataProvider.ClearDevices(customerId, patientId, authToken);
        }
    }
}