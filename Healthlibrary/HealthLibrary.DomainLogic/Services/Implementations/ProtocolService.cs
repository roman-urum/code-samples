using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HealthLibrary.Common;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Common.Helpers;
using HealthLibrary.DataAccess;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Protocol;
using HealthLibrary.DomainLogic.Services.Interfaces;

namespace HealthLibrary.DomainLogic.Services.Implementations
{
    /// <summary>
    /// ProtocolService.
    /// </summary>
    public class ProtocolService : IProtocolService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Element> elementRepository;
        private readonly IRepository<Protocol> protocolRepository;
        private readonly IRepository<ProtocolElement> protocolElementRepository;
        private readonly IRepository<Branch> branchRepository;
        private readonly IRepository<Alert> alertRepository;
        private readonly IRepository<Condition> conditionRepository;
        private readonly ICareElementContext careElementContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="careElementContext">The care element context.</param>
        public ProtocolService(
            IUnitOfWork unitOfWork,
            ICareElementContext careElementContext
        )
        {
            this.unitOfWork = unitOfWork;
            this.elementRepository = this.unitOfWork.CreateGenericRepository<Element>();
            this.protocolRepository = this.unitOfWork.CreateGenericRepository<Protocol>();
            this.protocolElementRepository = this.unitOfWork.CreateGenericRepository<ProtocolElement>();
            this.branchRepository = this.unitOfWork.CreateGenericRepository<Branch>();
            this.alertRepository = this.unitOfWork.CreateGenericRepository<Alert>();
            this.conditionRepository = this.unitOfWork.CreateGenericRepository<Condition>();
            this.careElementContext = careElementContext;
        }

        /// <summary>
        /// Creates the protocol.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <returns></returns>
        public async Task<Protocol> CreateProtocol(Protocol protocol)
        {
            protocolRepository.Insert(protocol);
            await unitOfWork.SaveAsync();

            return protocol;
        }

        /// <summary>
        /// Gets the elements by ids.
        /// </summary>
        /// <param name="elementsIds">The elements ids.</param>
        /// <returns></returns>
        public async Task<IList<Element>> GetElementsByIds(IList<Guid> elementsIds)
        {
            var elements = await elementRepository.FindAsync(e => elementsIds.Contains(e.Id));

            return elements;
        }

        /// <summary>
        /// Determines whether [is element in use] [the specified element identifier].
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="elementId">The element identifier.</param>
        /// <returns></returns>
        public async Task<bool> IsElementInUse(int customerId, Guid elementId)
        {
            return (await this.protocolRepository.FindAsync(p => p.ProtocolElements.Any(pe => pe.ElementId == elementId && pe.Protocol.CustomerId == customerId))).Any();
        }

        /// <summary>
        /// Updates the protocol.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="existingProtocolId">The existing protocol identifier.</param>
        /// <param name="newProtocol">The new protocol.</param>
        /// <returns></returns>
        public async Task<CreateUpdateProtocolStatus> UpdateProtocol(
            int customerId, 
            Guid existingProtocolId, 
            Protocol newProtocol
        )
        {
            var existingProtocol = await GetProtocol(customerId, existingProtocolId);

            if (existingProtocol == null)
            {
                return CreateUpdateProtocolStatus.ProtocolWithSuchIdDoesNotExist;
            }

            #region Updating default localized name

            var existingDefaultNameLocalizedString =
                existingProtocol.NameLocalizedStrings.Single(s => s.Language == careElementContext.DefaultLanguage);
            var updatedDefaultNameLocalizedString =
                newProtocol.NameLocalizedStrings.Single(s => s.Language == careElementContext.DefaultLanguage);

            existingDefaultNameLocalizedString.Value = updatedDefaultNameLocalizedString.Value;

            #endregion

            existingProtocol.IsPrivate = newProtocol.IsPrivate;

            #region Updating protocol's Tags

            existingProtocol.Tags.RemoveRange(existingProtocol.Tags.ToList());
            existingProtocol.Tags.AddRange(newProtocol.Tags);

            #endregion

            #region Updating protocol's ProtocolElements

            // Сonditions should be deleted explicitly due to they do not support cascade delete
            conditionRepository.DeleteRange(existingProtocol.ProtocolElements.SelectMany(pe => pe.Branches.SelectMany(a => a.Conditions)).ToList());
            conditionRepository.DeleteRange(existingProtocol.ProtocolElements.SelectMany(pe => pe.Alerts.SelectMany(a => a.Conditions)).ToList());
            branchRepository.DeleteRange(existingProtocol.ProtocolElements.SelectMany(pe => pe.Branches).ToList());
            alertRepository.DeleteRange(existingProtocol.ProtocolElements.SelectMany(pe => pe.Alerts).ToList());
            protocolElementRepository.DeleteRange(existingProtocol.ProtocolElements.ToList());
            existingProtocol.ProtocolElements.AddRange(newProtocol.ProtocolElements);

            #endregion

            protocolRepository.Update(existingProtocol, true);
            await unitOfWork.SaveAsync();

            return CreateUpdateProtocolStatus.Success;
        }

        /// <summary>
        /// Gets the protocol by identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <returns></returns>
        public async Task<Protocol> GetProtocol(int customerId, Guid protocolId)
        {
            var result = await protocolRepository.FindAsync(
                a => a.CustomerId == customerId && a.Id == protocolId && !a.IsDeleted,
                includeProperties: new List<Expression<Func<Protocol, object>>>
                {
                    e => e.Tags,
                    e => e.NameLocalizedStrings,
                    e => e.ProtocolElements,
                    e => e.ProtocolElements.Select(pe => pe.Element),
                    e => e.ProtocolElements.Select(pe => pe.Branches),
                    e => e.ProtocolElements.Select(pe => pe.NextProtocolElement),
                    e => e.ProtocolElements.Select(pe => pe.Element.Tags)
                }
            );

            return result.SingleOrDefault();
        }

        /// <summary>
        /// Deletes the protocol.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <returns></returns>
        public async Task<DeleteProtocolStatus> DeleteProtocol(int customerId, Guid protocolId)
        {
            var existingProtocol = await GetProtocol(customerId, protocolId);

            if (existingProtocol != null)
            {
                if (!existingProtocol.ProgramElements.Any())
                {
                    protocolRepository.Delete(existingProtocol);

                    await unitOfWork.SaveAsync();

                    return await Task.FromResult(DeleteProtocolStatus.Success);
                }

                return await Task.FromResult(DeleteProtocolStatus.InUse);
            }

            return await Task.FromResult(DeleteProtocolStatus.NotFound);
        }

        /// <summary>
        /// Gets the protocols.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResult<Protocol>> GetProtocols(int customerId, SearchProtocolDto request = null)
        {
            Expression<Func<Protocol, bool>> expression = p => !p.IsDeleted && p.CustomerId == customerId;

            if (request != null)
            {
                if (request.CreatedAfter.HasValue)
                {
                    expression = expression.And(p => p.CreatedUtc >= request.CreatedAfter.Value);
                }

                if (request.UpdatedAfter.HasValue)
                {
                    expression = expression.And(p => p.UpdatedUtc >= request.UpdatedAfter.Value);
                }

                if (request.CreatedBefore.HasValue)
                {
                    expression = expression.And(p => p.CreatedUtc < request.CreatedBefore.Value);
                }

                if (request.UpdatedBefore.HasValue)
                {
                    expression = expression.And(p => p.UpdatedUtc < request.UpdatedBefore.Value);
                }

                if (request.Tags != null && request.Tags.Any())
                {
                    Expression<Func<Protocol, bool>> tagsExpression = PredicateBuilder.False<Protocol>(); ;

                    foreach (var tag in request.Tags)
                    {
                        tagsExpression = tagsExpression.Or(se => se.Tags.Any(t => t.Name.ToLower() == tag.ToLower()));
                    }

                    expression = expression.And(tagsExpression);
                }

                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(p => p.NameLocalizedStrings.Any(ls => ls.Value.Contains(term)));
                    }
                }
            }
            
            return await protocolRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderBy(e => e.Id),
                    new List<Expression<Func<Protocol, object>>>
                    {
                        e => e.Tags,
                        e => e.NameLocalizedStrings,
                        e => e.ProtocolElements,
                        e => e.ProtocolElements.Select(pe => pe.Element),
                        e => e.ProtocolElements.Select(pe => pe.Branches),
                        e => e.ProtocolElements.Select(pe => pe.NextProtocolElement),
                        e => e.ProtocolElements.Select(pe => pe.Element.Tags)
                    },
                    request != null ? request.Skip : (int?)null,
                    request != null ? request.Take : (int?)null
                );
        }

        /// <summary>
        /// Updates the name of the protocol localized.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="protocolId">The protocol identifier.</param>
        /// <param name="localizedName">Name of the localized.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">careElementContext.Language is invalid</exception>
        public async Task<CreateUpdateProtocolStatus> UpdateProtocolLocalizedName(
            int customerId,
            Guid protocolId,
            string localizedName
        )
        {
            var existingProtocol = await GetProtocol(customerId, protocolId);

            if (existingProtocol == null)
            {
                return CreateUpdateProtocolStatus.ProtocolWithSuchIdDoesNotExist;
            }

            if (string.IsNullOrEmpty(careElementContext.Language))
            {
                throw new ArgumentException("careElementContext.Language is invalid");
            }
            
            var existingLocalizedString =
                existingProtocol.NameLocalizedStrings.SingleOrDefault(s => s.Language == careElementContext.Language);

            if (existingLocalizedString == null)
            {
                existingProtocol.NameLocalizedStrings.Add(
                    new ProtocolString()
                    {
                        Language = careElementContext.Language,
                        Value = localizedName
                    }
                );
            }
            else
            {
                existingLocalizedString.Value = localizedName;
            }

            protocolRepository.Update(existingProtocol);
            await unitOfWork.SaveAsync();

            return CreateUpdateProtocolStatus.Success;
        }
    }
}