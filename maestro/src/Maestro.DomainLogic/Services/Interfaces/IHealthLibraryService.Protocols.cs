using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.HealthLibraryService.Protocols;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IHealthLibraryService.Protocols
    /// </summary>
    public partial interface IHealthLibraryService
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
        Task<ProtocolResponseDto> GetProtocol(Guid protocolId, int customerId, string language, bool isBrief, string token);

        /// <summary>
        /// Searches the protocols.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchProtocolsDto">The search protocols dto.</param>
        /// <returns></returns>
        Task<IList<ProtocolResponseDto>> SearchProtocols(string token, int customerId, SearchProtocolsRequestDto searchProtocolsDto);

        /// <summary>
        /// Creates the protocol.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocol">The protocol.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateProtocol(string token, int customerId, CreateProtocolRequestDto protocol);

        /// <summary>
        /// Updates the protocol.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocol">The protocol.</param>
        /// <returns></returns>
        Task UpdateProtocol(string token, int customerId, UpdateProtocolRequestDto protocol);
    }
}