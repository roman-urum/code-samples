using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeviceService.Domain.Dtos;
using DeviceService.Domain.Dtos.Enums;
using DeviceService.Domain.Entities;
using DeviceService.Domain.Entities.Enums;

namespace DeviceService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IDeviceService.
    /// </summary>
    public interface IDevicesService
    {
        /// <summary>
        /// Creates the device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        Task<OperationResultDto<Device, CreateDeviceStatus>> CreateDevice(Device device);

        /// <summary>
        /// Updates the device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        Task<UpdateDeviceStatus> UpdateDevice(Device device);

        /// <summary>
        /// Deletes the device.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">Device not found</exception>
        /// <exception cref="System.InvalidOperationException">Invalid device status.</exception>
        Task<DeleteDeviceStatus> DeleteDevice(int customerId, Guid deviceId);

        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResult<Device>> GetDevices(int customerId, DevicesSearchDto request);

        /// <summary>
        /// Gets the device.
        /// Creates iHealth account for the device if it not exists yet.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Device> GetDevice(int customerId, Guid id);

        /// <summary>
        /// Returns device by certificate subject.
        /// </summary>
        /// <returns></returns>
        Device GetDevice(string certificateSubject);

        /// <summary>
        /// Returns device by certificate subject.
        /// </summary>
        /// <returns></returns>
        Task<Device> GetDevice(string activationCode, DateTime birthDate);

        /// <summary>
        /// Sets the decomission status.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">
        /// Invalid decomission status
        /// or
        /// Invalid device status.
        /// </exception>
        /// <exception cref="System.NullReferenceException">Device not found</exception>
        Task<OperationResultDto<Device, SetDecomissionStatusOperationStatus>> SetDecomissionStatus(
            int customerId, Guid deviceId, Status status);

        /// <summary>
        /// Activates the device.
        /// </summary>
        /// <param name="activation">The activation.</param>
        /// <returns></returns>
        Task<OperationResultDto<Device, ActivateDeviceStatus>> ActivateDevice(Activation activation);

        /// <summary>
        /// Updates time of last device connection to current.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="deviceTz">The device tz.</param>
        /// <returns>
        /// False if active device with such id is not found.
        /// </returns>
        Task<bool> UpdateLastConnectedUtc(
            int customerId,
            Guid deviceId,
            string deviceTz = null
        );

        /// <summary>
        /// Clears the devices. Decommission activated devices and delete decommissioned devices.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <returns>List of decomissioned/deleted devices.</returns>
        Task<IList<Device>> ClearDevices(int customerId, Guid patientId);
    }
}