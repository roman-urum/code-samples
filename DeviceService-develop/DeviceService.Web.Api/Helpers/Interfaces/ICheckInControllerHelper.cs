using System;
using System.Threading.Tasks;

namespace DeviceService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// Helper methods for check in controller.
    /// </summary>
    public interface ICheckInControllerHelper
    {
        /// <summary>
        /// Updates time of last device connection to current.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="deviceTz">The device timezone.</param>
        /// <returns>
        /// False if active device with such id is not found.
        /// </returns>
        Task<bool> UpdateLastConnectedUtc(
            int customerId,
            Guid deviceId,
            string deviceTz = null
        );
    }
}