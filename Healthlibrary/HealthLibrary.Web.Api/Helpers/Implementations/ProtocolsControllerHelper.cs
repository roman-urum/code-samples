using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthLibrary.Common;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Enums;
using HealthLibrary.Domain.Entities.Protocol;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Protocols;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// ProtocolsControllerHelper.
    /// </summary>
    public class ProtocolsControllerHelper : IProtocolsControllerHelper
    {
        private readonly IProtocolService protocolService;
        private readonly ITagsService tagsService;
        private readonly ICareElementContext careElementContext;
        private readonly IGlobalSearchCacheHelper globalSearchCacheHelper;
        private readonly ITagsSearchCacheHelper tagsSearchCacheHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolsControllerHelper" /> class.
        /// </summary>
        /// <param name="protocolService">The protocol service.</param>
        /// <param name="tagsService">The tags service.</param>
        /// <param name="careElementContext">The care element context.</param>
        /// <param name="globalSearchCacheHelper">The global search cache helper.</param>
        /// <param name="tagsSearchCacheHelper">The tags search cache helper.</param>
        public ProtocolsControllerHelper(
            IProtocolService protocolService,
            ITagsService tagsService,
            ICareElementContext careElementContext,
            IGlobalSearchCacheHelper globalSearchCacheHelper,
            ITagsSearchCacheHelper tagsSearchCacheHelper
        )
        {
            this.protocolService = protocolService;
            this.tagsService = tagsService;
            this.careElementContext = careElementContext;
            this.globalSearchCacheHelper = globalSearchCacheHelper;
            this.tagsSearchCacheHelper = tagsSearchCacheHelper;
        }

        /// <summary>
        /// Creates the protocol.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, CreateUpdateProtocolStatus>> CreateProtocol(
            int customerId,
            CreateProtocolRequestDto request
        )
        {
            #region Step 1 - Client IDs validation

            var validationResult = PerformValidationStep1(request);

            if (!validationResult.HasFlag(CreateUpdateProtocolStatus.Success))
            {
                return new OperationResultDto<Guid, CreateUpdateProtocolStatus>()
                {
                    Status = validationResult
                };
            }

            #endregion

            #region Step 2 - Remaining Validation which can be made only after successfuly passed step 1

            validationResult = await PerformValidationStep2(request);

            if (!validationResult.HasFlag(CreateUpdateProtocolStatus.Success))
            {
                return new OperationResultDto<Guid, CreateUpdateProtocolStatus>()
                {
                    Status = validationResult
                };
            }

            #endregion

            var protocol = await BuildProtocol(customerId, request);

            var createdProtocol = await protocolService.CreateProtocol(protocol);

            await globalSearchCacheHelper.AddOrUpdateEntry(customerId, Mapper.Map<Protocol, SearchEntryDto>(protocol));

            await tagsSearchCacheHelper.AddOrUpdateTags(customerId, createdProtocol.Tags.Select(t => t.Name).ToList());

            var unusedTags = await tagsService.RemoveUnusedTags(customerId);
            await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);

            return new OperationResultDto<Guid, CreateUpdateProtocolStatus>()
            {
                Content = createdProtocol.Id,
                Status = validationResult
            };
        }

        /// <summary>
        /// Updates the protocol.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<CreateUpdateProtocolStatus> UpdateProtocol(
            int customerId, 
            Guid protocolId,
            UpdateProtocolRequestDto request
        )
        {
            #region Step 1 - Client IDs validation

            var validationResult = PerformValidationStep1(request);

            if (!validationResult.HasFlag(CreateUpdateProtocolStatus.Success))
            {
                return validationResult;
            }

            #endregion

            #region Step 2 - Remaining Validation which can be made only after successfuly passed step 1

            validationResult = await PerformValidationStep2(request);

            if (!validationResult.HasFlag(CreateUpdateProtocolStatus.Success))
            {
                return validationResult;
            }

            #endregion

            var updatedProtocol = await BuildProtocol(customerId, request);

            var result = await protocolService.UpdateProtocol(customerId, protocolId, updatedProtocol);

            updatedProtocol.Id = protocolId;
            await globalSearchCacheHelper.AddOrUpdateEntry(customerId, Mapper.Map<Protocol, SearchEntryDto>(updatedProtocol));

            await tagsSearchCacheHelper.AddOrUpdateTags(customerId, updatedProtocol.Tags.Select(t => t.Name).ToList());

            var unusedTags = await tagsService.RemoveUnusedTags(customerId);
            await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);

            return result;
        }

        /// <summary>
        /// Deletes the protocol.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <returns></returns>
        public async Task<DeleteProtocolStatus> DeleteProtocol(int customerId, Guid protocolId)
        {
            var result = await protocolService.DeleteProtocol(customerId, protocolId);

            if (result == DeleteProtocolStatus.Success)
            {
                await globalSearchCacheHelper.RemoveEntry(customerId, protocolId);

                var unusedTags = await tagsService.RemoveUnusedTags(customerId);
                await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);
            }

            return result;
        }

        /// <summary>
        /// Gets the protocol.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        public async Task<OperationResultDto<ProtocolResponseDto, GetProtocolStatus>> GetProtocol(
            int customerId,
            Guid protocolId, 
            bool isBrief
        )
        {
            var protocol = await protocolService.GetProtocol(customerId, protocolId);

            if (protocol == null)
            {
                return await Task.FromResult(
                    new OperationResultDto<ProtocolResponseDto, GetProtocolStatus>()
                    {
                        Status = GetProtocolStatus.NotFound
                    }
                );
            }

            return await Task.FromResult(
                new OperationResultDto<ProtocolResponseDto, GetProtocolStatus>()
                {
                    Status = GetProtocolStatus.Success,
                    Content = Mapper.Map<Protocol, ProtocolResponseDto>(protocol, o => o.Items.Add("isBrief", isBrief))
                }
            );
        }

        /// <summary>
        /// Gets the protocols.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request">The request.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        public async Task<PagedResultDto<ProtocolResponseDto>> GetProtocols(
            int customerId,
            SearchProtocolDto request,
            bool isBrief
        )
        {
            var protocols = await protocolService.GetProtocols(customerId, request);

            var result = Mapper.Map<PagedResult<Protocol>, PagedResultDto<ProtocolResponseDto>>(
                protocols,
                o => o.Items.Add("isBrief", isBrief)
            );

            result.Results = result.Results.OrderBy(e => e.Name.Value).ToList();

            return result;
        }

        /// <summary>
        /// Updates the name of the protocol localized.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="localizedName">Name of the localized.</param>
        /// <returns></returns>
        public Task<CreateUpdateProtocolStatus> UpdateProtocolLocalizedName(
            int customerId,
            Guid protocolId,
            string localizedName
        )
        {
            return protocolService.UpdateProtocolLocalizedName(customerId, protocolId, localizedName);
        }

        #region Private methods

        /// <summary>
        /// Validation, which doesn't depend on data retrieved from the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private CreateUpdateProtocolStatus PerformValidationStep1(CreateProtocolRequestDto request)
        {
            CreateUpdateProtocolStatus validationResult = 0;
            var validProtocolElementClientIds = request.ProtocolElements.Select(e => e.Id).ToList();

            // Validating FirstProtocolElementClientId
            if (validProtocolElementClientIds.All(e => e != request.FirstProtocolElementId))
            {
                validationResult |= CreateUpdateProtocolStatus.FirstProtocolElementClientIdInvalid;
            }

            // Validating that ProtocolElements list contains only unique ProtocolElements
            if (request.ProtocolElements.Count != request.ProtocolElements.Select(pe => pe.Id).Distinct().Count())
            {
                validationResult |= CreateUpdateProtocolStatus.ProtocolElementsListShouldBeDistinct;
            }

            // Validating NextProtocolElementClientId inside ProtocolElements
            foreach (var protocolElement in request.ProtocolElements)
            {
                if (protocolElement.NextProtocolElementId != null &&
                    validProtocolElementClientIds.All(e => e != protocolElement.NextProtocolElementId))
                {
                    validationResult |= CreateUpdateProtocolStatus.OneOfNextProtocolElementClientIdsInProtocolElementsInvalid;
                }

                if (protocolElement.Branches != null)
                {
                    // Validating NextProtocolElementClientId inside Branches
                    if (protocolElement.Branches
                        .Where(b => b.NextProtocolElementId != null)
                        .Any(b => validProtocolElementClientIds.All(e => e != b.NextProtocolElementId)))
                    {
                        validationResult |= CreateUpdateProtocolStatus.OneOfNextProtocolElementClientIdsInBranchesInvalid;
                    }
                }

                // Validating that each ProtocolElement should be used within the protocol
                // It means that ProtocolElement should be referenced at least as FirstProtocolElement or NextProtocolElement
                var protocolElementClientId = protocolElement.Id;

                if (!(request.FirstProtocolElementId == protocolElementClientId ||
                    request.ProtocolElements.Any(pe => pe.NextProtocolElementId == protocolElementClientId) ||
                    request.ProtocolElements.Where(pe => pe.Branches != null).SelectMany(pe => pe.Branches).Any(b => b.NextProtocolElementId == protocolElementClientId)))
                {
                    validationResult |= CreateUpdateProtocolStatus.OneOfProtocolElementsIsNotUsedWithinProtocol;
                }
            }

            // Validating that ProtocolElement (or it's branches) shouldn't point to itself
            if (request.ProtocolElements.Any(pe => pe.NextProtocolElementId == pe.Id) ||
                request.ProtocolElements.Where(pe => pe.Branches != null).Any(pe => pe.Branches.Any(b => b.NextProtocolElementId == pe.Id)))
            {
                validationResult |= CreateUpdateProtocolStatus.ProtocolElementShouldNotPointToItself;
            }

            // Validating that branch should have at least one condition 
            // and also that branch should contain conditions with operands related with ONLY measurements OR ONLY answer sets OR ONLY answer choices
            var branches = request.ProtocolElements.Where(pe => pe.Branches != null).SelectMany(pe => pe.Branches).ToList();

            if (!branches.All(b => b.Conditions != null &&
                b.Conditions.Any() &&
                !(b.Conditions.Any(c => c.Operand == OperandType.ScaleAnswerSet) && b.Conditions.Any(c => c.Operand != OperandType.ScaleAnswerSet)) &&
                !(b.Conditions.Any(c => c.Operand == OperandType.SelectionAnswerChoice) && b.Conditions.Any(c => c.Operand != OperandType.SelectionAnswerChoice))))
            {
                validationResult |= CreateUpdateProtocolStatus.OneOfBranchesOrAlertsInproperlyFilledIn;
            }

            // Validating that Alert should have at least one Condition
            // and also that alert should contain conditions with operands related with ONLY measurements OR ONLY answer sets OR ONLY answer choices
            var alerts = request.ProtocolElements.Where(pe => pe.Alerts != null).SelectMany(pe => pe.Alerts).ToList();

            if (!alerts.All(a => a.Conditions != null &&
                a.Conditions.Any() &&
                !(a.Conditions.Any(c => c.Operand == OperandType.ScaleAnswerSet) && a.Conditions.Any(c => c.Operand != OperandType.ScaleAnswerSet)) &&
                !(a.Conditions.Any(c => c.Operand == OperandType.SelectionAnswerChoice) && a.Conditions.Any(c => c.Operand != OperandType.SelectionAnswerChoice))))
            {
                validationResult |= CreateUpdateProtocolStatus.OneOfBranchesOrAlertsInproperlyFilledIn;
            }

            // Validating that conditions with Operand == ScaleAnswerSet have decimal value
            // Conditions from Branches
            var scaleAnswerChoiceStrings =
                request
                .ProtocolElements
                .Where(pe => pe.Branches != null)
                .SelectMany(pe => pe.Branches)
                .Where(b => b.Conditions != null && b.Conditions.All(c => c.Operand == OperandType.ScaleAnswerSet))
                .SelectMany(b => b.Conditions)
                .Select(c => c.Value)
                .Distinct()
                .ToList();

            // Adding Conditions from Alerts
            scaleAnswerChoiceStrings.AddRange(
                request
                .ProtocolElements
                .Where(pe => pe.Alerts != null)
                .SelectMany(pe => pe.Alerts)
                .Where(a => a.Conditions != null && a.Conditions.All(c => c.Operand == OperandType.ScaleAnswerSet))
                .SelectMany(a => a.Conditions)
                .Select(c => c.Value)
                .Distinct()
                .ToList()
            );

            foreach (var scaleAnswerChoiceString in scaleAnswerChoiceStrings)
            {
                decimal scaleAnswerChoiceValue;

                if (!decimal.TryParse(scaleAnswerChoiceString, out scaleAnswerChoiceValue))
                {
                    validationResult |= CreateUpdateProtocolStatus.OneOfBranchesOrAlertsInproperlyFilledIn;

                    break;
                }
            }

            // Validating that conditions with Operand == "MeasurementType" have valid value
            // Conditions from Branches
            var measurementValueStrings =
                request
                .ProtocolElements
                .Where(pe => pe.Branches != null)
                .SelectMany(pe => pe.Branches)
                .Where(b => b.Conditions != null && b.Conditions.All(c => c.Operand != OperandType.SelectionAnswerChoice) && b.Conditions.All(c => c.Operand != OperandType.ScaleAnswerSet))
                .SelectMany(b => b.Conditions)
                .Select(c => c.Value)
                .Distinct()
                .ToList();

            // Adding Conditions from Alerts
            measurementValueStrings.AddRange(
                request
                .ProtocolElements
                .Where(pe => pe.Alerts != null)
                .SelectMany(pe => pe.Alerts)
                .Where(a => a.Conditions != null && a.Conditions.All(c => c.Operand != OperandType.SelectionAnswerChoice) && a.Conditions.All(c => c.Operand != OperandType.ScaleAnswerSet))
                .SelectMany(b => b.Conditions)
                .Select(c => c.Value)
                .Distinct()
                .ToList()
            );

            foreach (var measurementValueString in measurementValueStrings)
            {
                MeasurementLimitType parsedValue;

                if (!Enum.TryParse(measurementValueString, out parsedValue))
                {
                    validationResult |= CreateUpdateProtocolStatus.OneOfBranchesOrAlertsInproperlyFilledIn;

                    break;
                }
            }

            // Validating that conditions with Operand == SelectionAnswerChoice have Guid value
            // Conditions from Branches
            var branchesSelectionAnswerChoiceStringIds =
                request
                .ProtocolElements
                .Where(pe => pe.Branches != null)
                .SelectMany(pe => pe.Branches)
                .Where(b => b.Conditions != null && b.Conditions.All(c => c.Operand == OperandType.SelectionAnswerChoice))
                .SelectMany(b => b.Conditions)
                .Select(c => c.Value)
                .Distinct()
                .ToList();

            // Conditions from Alerts
            var alertsSelectionAnswerChoiceStringIds = 
                request
                .ProtocolElements
                .Where(pe => pe.Alerts != null)
                .SelectMany(pe => pe.Alerts)
                .Where(a => a.Conditions != null && a.Conditions.All(c => c.Operand == OperandType.SelectionAnswerChoice))
                .SelectMany(a => a.Conditions)
                .Select(c => c.Value)
                .Distinct()
                .ToList();

            var allSelectionAnswerChoiceStringIds = branchesSelectionAnswerChoiceStringIds
                .Concat(alertsSelectionAnswerChoiceStringIds)
                .ToList();
            
            var clientSelectionAnswerChoiceId = new Guid();
            var clientSelectionAnswerChoiceIds =
                allSelectionAnswerChoiceStringIds
                .Where(sid => Guid.TryParse(sid, out clientSelectionAnswerChoiceId))
                .Select(e => clientSelectionAnswerChoiceId)
                .ToList();

            if (clientSelectionAnswerChoiceIds.Count != allSelectionAnswerChoiceStringIds.Count)
            {
                validationResult |= CreateUpdateProtocolStatus.OneOfBranchesOrAlertsInproperlyFilledIn;
            }

            if (validationResult > 0)
            {
                return validationResult;
            }

            return CreateUpdateProtocolStatus.Success;
        }

        /// <summary>
        /// Validation, which depends on data retrieved from the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private async Task<CreateUpdateProtocolStatus> PerformValidationStep2(CreateProtocolRequestDto request)
        {
            // Validating that ElementIds provided in the request correspond to existing Elements in the database
            var elementIds =
                request
                .ProtocolElements
                .Select(pe => pe.ElementId)
                .Distinct()
                .ToList();

            var existingElements = await protocolService.GetElementsByIds(elementIds);

            if (existingElements.Count != elementIds.Count)
            {
                return CreateUpdateProtocolStatus.OneOfElementIdsInvalid;
            }

            // Validating that ProtocolElement associated with particular Element have conditions with appropriate Operand based on ElementType
            foreach (var protocolElement in request.ProtocolElements)
            {
                var alertConditions = protocolElement.Alerts != null && protocolElement.Alerts.Any()
                    ? protocolElement.Alerts.SelectMany(b => b.Conditions).ToList()
                    : new List<ConditionDto>();

                var branchConditions = protocolElement.Branches != null && protocolElement.Branches.Any()
                    ? protocolElement.Branches.SelectMany(b => b.Conditions).ToList()
                    : new List<ConditionDto>();

                var conditions = alertConditions.Concat(branchConditions).ToList();

                if (conditions.Any())
                {
                    var associatedElement = existingElements.SingleOrDefault(e => e.Id == protocolElement.ElementId);

                    if (associatedElement != null)
                    {
                        if (associatedElement is QuestionElement)
                        {
                            var questionElement = (QuestionElement)associatedElement;

                            if (questionElement.AnswerSet is SelectionAnswerSet)
                            {
                                Guid tempGuid;
                                var selectionAnswerSet = questionElement.AnswerSet as SelectionAnswerSet;

                                if (conditions.Any(c => c.Operand != OperandType.SelectionAnswerChoice ||
                                    !Guid.TryParse(c.Value, out tempGuid) ||
                                    !selectionAnswerSet.SelectionAnswerChoices.Select(ac => ac.Id).Contains(Guid.Parse(c.Value)))
                                )
                                {
                                    return CreateUpdateProtocolStatus.OneOfBranchesOrAlertsInproperlyFilledIn;
                                }
                            }

                            if (questionElement.AnswerSet is ScaleAnswerSet)
                            {
                                if (conditions.Any(c => c.Operand != OperandType.ScaleAnswerSet))
                                {
                                    return CreateUpdateProtocolStatus.OneOfBranchesOrAlertsInproperlyFilledIn;
                                }
                            }
                        }

                        if (associatedElement is MeasurementElement)
                        {
                            if (conditions.Any(c => c.Operand == OperandType.SelectionAnswerChoice || c.Operand == OperandType.ScaleAnswerSet))
                            {
                                return CreateUpdateProtocolStatus.OneOfBranchesOrAlertsInproperlyFilledIn;
                            }
                        }

                        if (associatedElement is TextMediaElement)
                        {
                            return CreateUpdateProtocolStatus.OneOfBranchesOrAlertsInproperlyFilledIn;
                        }
                    }
                }
            }

            return CreateUpdateProtocolStatus.Success;
        }

        /// <summary>
        /// Builds the protocol.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private async Task<Protocol> BuildProtocol(int customerId, CreateProtocolRequestDto request)
        {
            var protocol = new Protocol
            {
                CustomerId = customerId,
                NameLocalizedStrings = new List<ProtocolString>()
                {
                    new ProtocolString()
                    {
                        Value = request.Name,
                        Language = careElementContext.DefaultLanguage
                    }
                },
                IsPrivate = request.IsPrivate,
                Tags = await tagsService.BuildTagsList(customerId, request.Tags),
                ProtocolElements = new List<ProtocolElement>()
            };

            foreach (var protocolElementDto in request.ProtocolElements)
            {
                var protocolElement = new ProtocolElement
                {
                    Id = protocolElementDto.Id, // Temporary storing client id to build up relationships and dump it before saving to the database
                    ElementId = protocolElementDto.ElementId,
                    Sort = protocolElementDto.Sort,
                    Branches = new List<Branch>(),
                    Alerts = new List<Alert>()
                };

                if (protocolElementDto.Branches != null)
                {
                    foreach (var branchDto in protocolElementDto.Branches)
                    {
                        if (branchDto.Conditions != null)
                        {
                            var branch = new Branch()
                            {
                                NextProtocolElementId = branchDto.NextProtocolElementId,
                                ThresholdAlertSeverityId = branchDto.ThresholdAlertSeverityId,
                                Conditions = new List<Condition>()
                            };

                            foreach (var conditionDto in branchDto.Conditions)
                            {
                                var condition = new Condition()
                                {
                                    Operand = conditionDto.Operand,
                                    Operator = conditionDto.Operator,
                                    Value = conditionDto.Value
                                };

                                branch.Conditions.Add(condition);
                            }

                            protocolElement.Branches.Add(branch);
                        }
                    }
                }

                if (protocolElementDto.Alerts != null)
                {
                    foreach (var alertDto in protocolElementDto.Alerts)
                    {
                        if (alertDto.Conditions != null)
                        {
                            var alert = new Alert()
                            {
                                Conditions = new List<Condition>()
                            };

                            foreach (var conditionDto in alertDto.Conditions)
                            {
                                var condition = new Condition()
                                {
                                    Operand = conditionDto.Operand,
                                    Operator = conditionDto.Operator,
                                    Value = conditionDto.Value
                                };

                                alert.Conditions.Add(condition);
                            }

                            alert.AlertSeverityId = alertDto.AlertSeverityId;

                            protocolElement.Alerts.Add(alert);
                        }
                    }
                }

                protocol.ProtocolElements.Add(protocolElement);
            }

            // Assigning FirstProtocolElement
            var firstProtocolElement =
                protocol.ProtocolElements.Single(pe => pe.Id == request.FirstProtocolElementId);
            firstProtocolElement.IsFirstProtocolElement = true;

            // Assigning NextProtocolElements and removing client Ids
            foreach (var protocolElement in protocol.ProtocolElements)
            {
                var nextProtocolElementIdForProtocolElement =
                    request.ProtocolElements.Single(pe => pe.Id == protocolElement.Id).NextProtocolElementId;

                if (nextProtocolElementIdForProtocolElement != null)
                {
                    protocolElement.NextProtocolElement =
                        protocol.ProtocolElements.Single(pe => pe.Id == nextProtocolElementIdForProtocolElement);
                }

                foreach (var branch in protocolElement.Branches.Where(b => b.NextProtocolElementId != null))
                {
                    branch.NextProtocolElement = protocol.ProtocolElements.Single(pe => pe.Id == branch.NextProtocolElementId);
                    branch.NextProtocolElementId = null; // Removing client Id
                }
            }

            foreach (var protocolElement in protocol.ProtocolElements)
            {
                protocolElement.Id = default(Guid); // Removing client Id
            }

            return protocol;
        }

        #endregion
    }
}