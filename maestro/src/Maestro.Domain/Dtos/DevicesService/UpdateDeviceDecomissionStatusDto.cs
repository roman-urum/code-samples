using System;

using Maestro.Common.ApiClient;
using Maestro.Domain.Enums;

namespace Maestro.Domain.Dtos.DevicesService
{
    public class UpdateDeviceDecomissionStatusDto
    {
        public Guid DeviceId { get; set; }

        [RequestParameter(RequestParameterType.RequestBody)]
        public DecommissionStatusDto Status { get; set; }
    }
}