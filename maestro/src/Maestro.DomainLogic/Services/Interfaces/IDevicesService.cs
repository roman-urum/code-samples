using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.DevicesService;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IDevicesService.
    /// </summary>
    public interface IDevicesService
    {
        /// <summary>
        /// Creates the specified create device dto.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateDevice(int customerId, CreateDeviceRequestDto request, string token);

        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IList<DeviceDto>> GetDevices(int customerId, Guid patientId, string token);

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<DeviceDto> GetDevice(int customerId, Guid deviceId, string token);

        /// <summary>
        /// Removes the device.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task DeleteDevice(int customerId, Guid deviceId, string token);

        /// <summary>
        /// Updates the list of devices
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customerId"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        Task UpdateDevice(string token, int customerId, DeviceDto device);

        /// <summary>
        /// Requests the device decomission.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceDecomissionDto">The device decomission dto.</param>
        /// <returns>Task.</returns>
        Task RequestDeviceDecomission(string token, int customerId, UpdateDeviceDecomissionStatusDto deviceDecomissionDto);

        /// <summary>
        /// Clears the devices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The identifier.</param>
        /// <param name="authToken">The authentication token.</param>
        /// <returns></returns>
        Task ClearDevices(int customerId, Guid patientId, string authToken);
    }
}