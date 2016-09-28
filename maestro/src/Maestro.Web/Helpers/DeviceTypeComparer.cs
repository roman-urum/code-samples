using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.DevicesService;

namespace Maestro.Web.Helpers
{
    /// <summary>
    /// DeviceTypeComparer.
    /// </summary>
    public class DeviceTypeComparer : IComparer<DeviceDto>
    {
        /// <summary>
        /// Compares the specified device1.
        /// </summary>
        /// <param name="device1">The device1.</param>
        /// <param name="device2">The device2.</param>
        /// <returns></returns>
        public int Compare(DeviceDto device1, DeviceDto device2)
        {
            if (device1.DeviceType == null && device2.DeviceType == null)
            {
                return 0;
            }

            if (device1.DeviceType == null && device2.DeviceType != null)
            {
                return 1;
            }

            if (device1.DeviceType != null && device2.DeviceType == null)
            {
                return -1;
            }

            return string.Compare(device1.DeviceType, device2.DeviceType, StringComparison.InvariantCulture);
        }
    }
}