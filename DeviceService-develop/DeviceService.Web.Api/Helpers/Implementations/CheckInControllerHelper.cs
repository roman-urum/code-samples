using System;
using System.Threading.Tasks;
using DeviceService.DomainLogic.Services.Interfaces;
using DeviceService.Web.Api.Helpers.Interfaces;

namespace DeviceService.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// Helper methods for check in controller.
    /// </summary>
    public class CheckInControllerHelper : ICheckInControllerHelper
    {
        private readonly IDevicesService deviceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckInControllerHelper" /> class.
        /// </summary>
        /// <param name="deviceService">The device service.</param>
        public CheckInControllerHelper(IDevicesService deviceService)
        {
            this.deviceService = deviceService;
        }

        /// <summary>
        /// Updates time of last device connection to current.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="deviceTz">The device timezone.</param>
        /// <returns>
        /// False if active device with such id is not found.
        /// </returns>
        public async Task<bool> UpdateLastConnectedUtc(
            int customerId,
            Guid deviceId, 
            string deviceTz = null
        )
        {
            var updateLastConnectedUtcResult = 
                await deviceService.UpdateLastConnectedUtc(customerId, deviceId, deviceTz);

            return updateLastConnectedUtcResult;
        }
    }
}