using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Protocols;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IProtocolsControllerHelper.
    /// </summary>
    public interface IProtocolsControllerHelper
    {
        /// <summary>
        /// Creates the protocol.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, CreateUpdateProtocolStatus>> CreateProtocol(
            int customerId,
            CreateProtocolRequestDto request
        );

        /// <summary>
        /// Updates the protocol.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<CreateUpdateProtocolStatus> UpdateProtocol(
            int customerId,
            Guid protocolId, 
            UpdateProtocolRequestDto request
        );

        /// <summary>
        /// Deletes the protocol.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <returns></returns>
        Task<DeleteProtocolStatus> DeleteProtocol(int customerId, Guid protocolId);

        /// <summary>
        /// Gets the protocol.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        Task<OperationResultDto<ProtocolResponseDto, GetProtocolStatus>> GetProtocol(
            int customerId,
            Guid protocolId,
            bool isBrief
        );

        /// <summary>
        /// Gets the protocols.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        Task<PagedResultDto<ProtocolResponseDto>> GetProtocols(
            int customerId,
            SearchProtocolDto request,
            bool isBrief
        );

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