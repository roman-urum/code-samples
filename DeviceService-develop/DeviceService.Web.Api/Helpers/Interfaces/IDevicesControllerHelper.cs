using System;
using System.Net.Http;
using System.Threading.Tasks;
using DeviceService.Domain.Dtos;
using DeviceService.Domain.Dtos.Enums;
using DeviceService.Web.Api.Models.Dtos.Entities;
using DeviceService.Web.Api.Models.Dtos.Enums;
using DeviceService.Web.Api.Models.Dtos.RequestsResponses;

namespace DeviceService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IDevicesControllerHelper.
    /// </summary>
    public interface IDevicesControllerHelper
    {
        /// <summary>
        /// Creates the device.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        Task<OperationResultDto<CreateDeviceResponseDto, CreateDeviceStatus>> CreateDevice(int customerId, CreateDeviceRequestDto device);

        /// <summary>
        /// Deletes the device.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        Task<DeleteDeviceStatus> DeleteDevice(int customerId, Guid deviceId);

        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<OperationResultDto<PagedResultDto<DeviceDto>, GetDeviceStatus>> GetDevices(int customerId, DevicesSearchDto request);

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<OperationResultDto<DeviceDto, GetDeviceStatus>> GetDevice(int customerId, Guid id);

        /// <summary>
        /// Updates the device.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<UpdateDeviceStatus> UpdateDevice(int customerId, Guid deviceId, BaseDeviceRequestDto request);

        /// <summary>
        /// Activate device
        /// </summary>
        /// <param name="activationData"></param>
        /// <returns></returns>
        Task<OperationResultDto<ActivationResponseDto, ActivateDeviceStatus>> ActivateDevice(ActivationDto activationData);

        /// <summary>
        /// Set decommission status of device
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<SetDecomissionStatusOperationStatus> SetDeviceDecommissionStatus(int customerId, Guid id,
            DecommissionStatusDto status);

        /// <summary>
        /// Clears the devices. Decommission activated devices and delete decommissioned devices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task ClearDevices(int customerId, Guid patientId, HttpRequestMessage request);
    }
}