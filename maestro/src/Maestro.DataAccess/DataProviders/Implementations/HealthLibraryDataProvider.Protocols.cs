using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.HealthLibraryService.Protocols;
using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// HealthLibraryDataProvider.Protocols
    /// </summary>
    public partial class HealthLibraryDataProvider
    {
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="language">The language.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<ProtocolResponseDto> GetProtocol(Guid protocolId, int customerId, string language, bool isBrief, string token)
        {
            var url = string.Format("/api/{0}/protocols/{1}/{2}?isBrief={3}", customerId, protocolId, language, isBrief);

            return await apiClient.SendRequestAsync<ProtocolResponseDto>(url, null, Method.GET, null, token);
        }

        /// <summary>
        /// Searches the protocols.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchProtocolsDto">The search protocols dto.</param>
        /// <returns></returns>
        public async Task<IList<ProtocolResponseDto>> SearchProtocols(
            string token, 
            int customerId,
            SearchProtocolsRequestDto searchProtocolsDto
        )
        {
            var url = string.Format("/api/{0}/protocols", customerId);

            var pagedResult = await apiClient.SendRequestAsync<PagedResult<ProtocolResponseDto>>(url, searchProtocolsDto, Method.GET, null, token);

            return pagedResult.Results;
        }

        /// <summary>
        /// Creates the protocol.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocol">The protocol.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateProtocol(string token, int customerId, CreateProtocolRequestDto protocol)
        {
            var url = string.Format("/api/{0}/protocols", customerId);

            return await apiClient.SendRequestAsync<PostResponseDto<Guid>>(url, protocol, Method.POST, null, token);
        }

        /// <summary>
        /// Updates the protocol.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocol">The protocol.</param>
        /// <returns></returns>
        public Task UpdateProtocol(string token, int customerId, UpdateProtocolRequestDto protocol)
        {
            var url = string.Format("/api/{0}/protocols/{1}", customerId, protocol.Id);

            return apiClient.SendRequestAsync(url, protocol, Method.PUT, null, token);
        }
    }
}