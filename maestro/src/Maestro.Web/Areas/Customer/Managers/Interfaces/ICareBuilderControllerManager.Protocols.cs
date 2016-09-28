using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.HealthLibraryService.Protocols;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    /// <summary>
    /// ICareBuilderControllerManager.
    /// </summary>
    public partial interface ICareBuilderControllerManager
    {
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="language">The language.</param>
        /// <param name="isBrief"></param>
        /// <returns></returns>
        Task<Models.CareBuilder.Protocols.ProtocolResponseViewModel> GetProtocol(Guid protocolId, string language, bool isBrief);
        /// <summary>
        /// Searches the protocols.
        /// </summary>
        /// <param name="searchProtocolsDto">The search protocols dto.</param>
        /// <returns></returns>
        Task<IList<ProtocolResponseDto>> SearchProtocols(SearchProtocolsRequestDto searchProtocolsDto);

        /// <summary>
        /// Creates the protocol.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateProtocol(CreateProtocolRequestDto protocol);

        /// <summary>
        /// Updates the protocol.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <returns></returns>
        Task UpdateProtocol(UpdateProtocolRequestDto protocol);
    }
}