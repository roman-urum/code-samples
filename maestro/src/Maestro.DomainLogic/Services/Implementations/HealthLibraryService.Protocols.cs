using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.HealthLibraryService.Protocols;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// HealthLibraryService.Protocols
    /// </summary>
    public partial class HealthLibraryService
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
            var result = await healthLibraryDataProvider.GetProtocol(protocolId, 
                customerId, language, isBrief, token);

            return result;
        }

        /// <summary>
        /// Searches the protocols.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchProtocolsDto">The search protocols dto.</param>
        /// <returns></returns>
        public async Task<IList<ProtocolResponseDto>> SearchProtocols(string token, int customerId, SearchProtocolsRequestDto searchProtocolsDto)
        {
            var result = await healthLibraryDataProvider.SearchProtocols(token, customerId, searchProtocolsDto);

            return result;
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
            var result = await healthLibraryDataProvider.CreateProtocol(token, customerId, protocol);

            return result;
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
            return healthLibraryDataProvider.UpdateProtocol(token, customerId, protocol);
        }
    }
}