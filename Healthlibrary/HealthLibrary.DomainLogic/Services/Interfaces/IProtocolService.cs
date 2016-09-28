using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Protocol;

namespace HealthLibrary.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IProtocolService.
    /// </summary>
    public interface IProtocolService
    {
        /// <summary>
        /// Creates the protocol.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <returns></returns>
        Task<Protocol> CreateProtocol(Protocol protocol);

        /// <summary>
        /// Gets the elements by ids.
        /// </summary>
        /// <param name="elementsIds">The elements ids.</param>
        /// <returns></returns>
        Task<IList<Element>> GetElementsByIds(IList<Guid> elementsIds);

        /// <summary>
        /// Determines whether [is element in use] [the specified element identifier].
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="elementId">The element identifier.</param>
        /// <returns></returns>
        Task<bool> IsElementInUse(int customerId, Guid elementId);

        /// <summary>
        /// Updates the protocol.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="existingProtocolId">The existing protocol identifier.</param>
        /// <param name="newProtocol">The new protocol.</param>
        /// <returns></returns>
        Task<CreateUpdateProtocolStatus> UpdateProtocol(
            int customerId, 
            Guid existingProtocolId, 
            Protocol newProtocol
        );

        /// <summary>
        /// Gets the protocol by identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <returns></returns>
        Task<Protocol> GetProtocol(int customerId, Guid protocolId);

        /// <summary>
        /// Deletes the protocol.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <returns></returns>
        Task<DeleteProtocolStatus> DeleteProtocol(int customerId, Guid protocolId);

        /// <summary>
        /// Gets the protocols.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResult<Protocol>> GetProtocols(int customerId, SearchProtocolDto request = null);

        /// <summary>
        /// Updates the name of the protocol localized.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="localizedName">Name of the localized.</param>
        /// <returns></returns>
        Task<CreateUpdateProtocolStatus> UpdateProtocolLocalizedName(
            int customerId,
            Guid protocolId,
            string localizedName
        );
    }
}