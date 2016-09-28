using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Common;
using Maestro.DataAccess.Api.ApiClient;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.DevicesService;
using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// DevicesDataProvider.
    /// </summary>
    public class DevicesDataProvider: IDevicesDataProvider
    {
        private readonly IRestApiClient apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesDataProvider"/> class.
        /// </summary>
        /// <param name="apiClientFactory">The API client factory.</param>
        public DevicesDataProvider(IRestApiClientFactory apiClientFactory)
        {
            this.apiClient = apiClientFactory.Create(Settings.DeviceServiceUrl);    
        }

        /// <summary>
        /// Creates the specified create device dto.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateDevice(int customerId, CreateDeviceRequestDto request, string bearerToken)
        {
            return await apiClient.SendRequestAsync<PostResponseDto<Guid>>(string.Format("/api/{0}/devices", customerId), request, Method.POST, null, bearerToken);
        }

        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<DeviceDto>> GetDevices(int customerId, Guid patientId, string bearerToken)
        {
            string url = string.Format("/api/{0}/devices?patientId={1}", customerId, patientId);

            var pagedResult = await apiClient.SendRequestAsync<PagedResult<DeviceDto>>(url, null, Method.GET, null, bearerToken);

            return pagedResult.Results;
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
            string url = string.Format("/api/{0}/devices/{1}", customerId, deviceId);

            return await apiClient.SendRequestAsync<DeviceDto>(url, null, Method.GET, null, token);
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
            string url = string.Format("/api/{0}/devices/{1}", customerId, deviceId);

            await apiClient.SendRequestAsync<DeviceDto>(url, null, Method.DELETE, null, token);            
        }

        /// <summary>
        /// Updates list of devices
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        public async Task UpdateDevice(string token, int customerId, DeviceDto device)
        {
            string url = string.Format("/api/{0}/devices/{1}", customerId, device.Id);

            await this.apiClient.SendRequestAsync(url, device, Method.PUT, null, token);     
        }

        /// <summary>
        /// Requests the device decomission.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="deviceDecomissionDto">The device decomission dto.</param>
        /// <returns></returns>
        public async Task RequestDeviceDecomission(string token, int customerId, UpdateDeviceDecomissionStatusDto deviceDecomissionDto)
        {
            string url = string.Format("/api/{0}/decommission/{1}", customerId, deviceDecomissionDto.DeviceId);

            await this.apiClient.SendRequestAsync(url, deviceDecomissionDto, Method.POST, null, token);     
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
            string url = string.Format("/api/{0}/devices/{1}/clear", customerId, patientId);

            await this.apiClient.SendRequestAsync(url, null, Method.POST, null, authToken);
        }
    }
}
