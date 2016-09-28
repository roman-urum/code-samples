using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.HealthLibraryService.Protocols;
using Maestro.DomainLogic.Services.Interfaces;
using Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    /// <summary>
    /// CareBuilderControllerManager.
    /// </summary>
    public partial class CareBuilderControllerManager
    {
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="language">The language.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        public async Task<ProtocolResponseViewModel> GetProtocol(
            Guid protocolId,
            string language,
            bool isBrief
        )
        {
            var token = authDataStorage.GetToken();

            var result = await healthLibraryService.GetProtocol(protocolId,
                CustomerContext.Current.Customer.Id, language, isBrief, token);

            var protocol = Mapper.Map<ProtocolResponseDto, ProtocolResponseViewModel>(result, o => o.Items.Add("isBrief", isBrief));

            SetUpEndBranchFlag(protocol);

            return protocol;
        }

        private void SetUpEndBranchFlag(ProtocolResponseViewModel protocol)
        {
            var cacheElementsList = new List<ProtocolElementResponseViewModel>();

            var nextElementId = protocol.FirstProtocolElementId;
            LoopElements(nextElementId, protocol.ProtocolElements, cacheElementsList);
        }

        private void LoopElements(Guid nextElementId, IList<ProtocolElementResponseViewModel> protocolElements,
            List<ProtocolElementResponseViewModel> cacheElementsList)
        {
            var currentBranchElementsList = new List<ProtocolElementResponseViewModel>();

            var element = protocolElements.FirstOrDefault(p => p.Id == nextElementId);
            ProtocolElementResponseViewModel nextElement = null;
            if (element != null && element.NextProtocolElementId != null &&
                protocolElements.Any(p => p.Id == element.NextProtocolElementId))
            {
                nextElement = protocolElements.FirstOrDefault(p => p.Id == element.NextProtocolElementId);
            }
            if (!cacheElementsList.Contains(element))
            {
                cacheElementsList.Add(element);
                currentBranchElementsList.Add(element);
            }
            if (nextElement == null)
            {
                element.IsEnded = true;
            }

            while (nextElement != null && element != null)
            {
                if (!cacheElementsList.Contains(nextElement))
                {
                    cacheElementsList.Add(nextElement);
                    currentBranchElementsList.Add(nextElement);
                }
                else
                {
                    element.IsEnded = true;
                    break;
                }

                if (nextElement.NextProtocolElementId == null)
                {
                    nextElement.IsEnded = true;
                    break;
                }
                element = nextElement;

                nextElement = protocolElements.FirstOrDefault(p => p.Id == nextElement.NextProtocolElementId);
            }

            // can be optimized to use just index in cacheElemensList instead of currentBranchElementsList
            LoopBranches(currentBranchElementsList, protocolElements, cacheElementsList);
        }

        private void LoopBranches(IList<ProtocolElementResponseViewModel> currentBranchElementsList,
            IList<ProtocolElementResponseViewModel> protocolElements, List<ProtocolElementResponseViewModel> cacheElementsList)
        {
            foreach (var element in currentBranchElementsList)
            {
                if (element.Branches != null && element.Branches.Any())
                {
                    foreach (var branch in element.Branches)
                    {
                        if (branch.NextProtocolElementId != null)
                        {
                            LoopElements(branch.NextProtocolElementId.Value, protocolElements, cacheElementsList);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Searches the protocols.
        /// </summary>
        /// <param name="searchProtocolsDto">The search protocols dto.</param>
        /// <returns></returns>
        public async Task<IList<ProtocolResponseDto>> SearchProtocols(
            SearchProtocolsRequestDto searchProtocolsDto)
        {
            var token = authDataStorage.GetToken();

            return await healthLibraryService.SearchProtocols(token, CustomerContext.Current.Customer.Id, searchProtocolsDto);
        }

        /// <summary>
        /// Creates the protocol.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateProtocol(CreateProtocolRequestDto protocol)
        {
            var token = authDataStorage.GetToken();

            var result = await healthLibraryService
                .CreateProtocol(token, CustomerContext.Current.Customer.Id, protocol);

            return result;
        }

        /// <summary>
        /// Updates the protocol.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <returns></returns>
        public Task UpdateProtocol(UpdateProtocolRequestDto protocol)
        {
            var token = authDataStorage.GetToken();

            return healthLibraryService.UpdateProtocol(token, CustomerContext.Current.Customer.Id, protocol);
        }
    }
}