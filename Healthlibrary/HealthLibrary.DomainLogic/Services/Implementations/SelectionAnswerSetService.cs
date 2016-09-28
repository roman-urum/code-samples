using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HealthLibrary.Common.Helpers;
using HealthLibrary.DataAccess;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Enums;
using HealthLibrary.DomainLogic.Extensions;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.DomainLogic.Services.Results;

namespace HealthLibrary.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Contains business rules to work with selection answer-sets.
    /// </summary>
    public class SelectionAnswerSetService : ISelectionAnswerSetService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<SelectionAnswerSet> selectionAnswerSetRepository;
        private readonly IRepository<SelectionAnswerChoice> selectionAnswerChoiceRepository;

        public SelectionAnswerSetService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.selectionAnswerSetRepository = this.unitOfWork.CreateGenericRepository<SelectionAnswerSet>();
            this.selectionAnswerChoiceRepository = this.unitOfWork.CreateGenericRepository<SelectionAnswerChoice>();
        }

        /// <summary>
        /// Creates new answerset with answers.
        /// </summary>
        /// <param name="answerSet">Answer-set data</param>
        public async Task<SelectionAnswerSet> Create(SelectionAnswerSet answerSet)
        {
            answerSet.Type = AnswerSetType.Selection;

            this.selectionAnswerSetRepository.Insert(answerSet);

            await this.unitOfWork.SaveAsync();

            return answerSet;
        }

        /// <summary>
        /// Updates existed answerset with new default answer strings.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="answerSet">The answer set.</param>
        /// <returns></returns>
        public async Task<ServiceActionResult<SelectionAnswerSetUpdateResult, SelectionAnswerSet>> Update(
            int customerId,
            Guid selectionAnswerSetId,
            SelectionAnswerSet answerSet
        )
        {
            var dbEntity = await Get(customerId, selectionAnswerSetId);

            if (dbEntity == null)
            {
                return
                    new ServiceActionResult<SelectionAnswerSetUpdateResult, SelectionAnswerSet>(
                        SelectionAnswerSetUpdateResult.NotFound);
            }

            if (answerSet.SelectionAnswerChoices.Any(
                a => !a.IsNew && dbEntity.SelectionAnswerChoices.All(ac => ac.Id != a.Id)))
            {
                return
                    new ServiceActionResult<SelectionAnswerSetUpdateResult, SelectionAnswerSet>(
                        SelectionAnswerSetUpdateResult.IncorrectAnswerId);
            }

            foreach (var answerEntity in dbEntity.SelectionAnswerChoices.ToList())
            {
                if (answerSet.SelectionAnswerChoices.All(a => a.Id != answerEntity.Id))
                {
                    this.selectionAnswerChoiceRepository.Delete(answerEntity);
                }
            }

            foreach (var answer in answerSet.SelectionAnswerChoices)
            {
                dbEntity.AddOrUpdateAnswerChoice(answer);
            }

            dbEntity.IsMultipleChoice = answerSet.IsMultipleChoice;
            dbEntity.Name = answerSet.Name;
            dbEntity.Tags.UpdateWith(answerSet.Tags);

            this.selectionAnswerSetRepository.Update(dbEntity);
            await this.unitOfWork.SaveAsync();

            return
                new ServiceActionResult<SelectionAnswerSetUpdateResult, SelectionAnswerSet>(
                    SelectionAnswerSetUpdateResult.Success, dbEntity);
        }

        /// <summary>
        /// Updates answerset with additional translations for answer choices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="answerChoices">The answer choices.</param>
        /// <returns></returns>
        public async Task<ServiceActionResult<SelectionAnswerSet>> UpdateLocalizedStrings(
            int customerId,
            Guid selectionAnswerSetId,
            IDictionary<Guid, SelectionAnswerChoiceString> answerChoices
        )
        {
            var dbEntity = await Get(customerId, selectionAnswerSetId);

            if (dbEntity == null)
            {
                return new ServiceActionResult<SelectionAnswerSet>(ServiceActionResultStatus.DataNotFound);
            }

            foreach (var answerChoice in answerChoices)
            {
                if (!dbEntity.AddOrUpdateLocalizedAnswerString(answerChoice.Key, answerChoice.Value))
                {
                    return new ServiceActionResult<SelectionAnswerSet>(ServiceActionResultStatus.IncorrectData);
                }
            }

            selectionAnswerSetRepository.Update(dbEntity);

            await unitOfWork.SaveAsync();

            return new ServiceActionResult<SelectionAnswerSet>(ServiceActionResultStatus.Succeed, dbEntity);
        }

        /// <summary>
        /// Sets specified entity as deleted in db.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <returns></returns>
        public async Task<DeleteStatus> Delete(int customerId, Guid selectionAnswerSetId)
        {
            var entity = await Get(customerId, selectionAnswerSetId);

            if (entity == null)
            {
                return DeleteStatus.NotFound;
            }

            if (entity.QuestionElements.Any())
            {
                return DeleteStatus.InUse;
            }

            entity.IsDeleted = true;

            selectionAnswerSetRepository.Update(entity);
            await unitOfWork.SaveAsync();

            return DeleteStatus.Success;
        }

        /// <summary>
        /// Returns answer set by answerset id and customer id.
        /// Customer id should be null for generic answer sets.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <returns></returns>
        public async Task<SelectionAnswerSet> Get(int customerId, Guid selectionAnswerSetId)
        {
            var result = await this.selectionAnswerSetRepository.FindAsync(
                a => !a.IsDeleted &&
                    a.Id == selectionAnswerSetId &&
                    a.CustomerId == customerId,
                null,
                new List<Expression<Func<SelectionAnswerSet, object>>>
                {
                    e => e.Tags,
                    e => e.SelectionAnswerChoices.Select(c => c.LocalizedStrings.Select(s => s.AudioFileMedia.Tags)),
                    e => e.QuestionElements.Select(q => q.QuestionElementToSelectionAnswerChoices)
                }
            );

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Returns list of selection sets which matches to provided search criteria.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<PagedResult<SelectionAnswerSet>> Find(int customerId, SelectionAnswerSetSearchDto searchRequest = null)
        {
            Expression<Func<SelectionAnswerSet, bool>> expression = s => !s.IsDeleted && s.CustomerId == customerId;

            if (searchRequest != null)
            {
                if (searchRequest.Tags != null && searchRequest.Tags.Any())
                {
                    Expression<Func<SelectionAnswerSet, bool>> tagsExpression = PredicateBuilder.False<SelectionAnswerSet>();

                    foreach (var tag in searchRequest.Tags)
                    {
                        tagsExpression = tagsExpression.Or(se => se.Tags.Any(t => t.Name.ToLower() == tag.ToLower()));
                    }

                    expression = expression.And(tagsExpression);
                }

                if (!string.IsNullOrEmpty(searchRequest.Q))
                {
                    var terms = searchRequest.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(s => s.Name.Contains(term));
                    }
                }

                if (searchRequest.IsMultipleChoice.HasValue)
                {
                    expression = expression.And(s => s.IsMultipleChoice == searchRequest.IsMultipleChoice.Value);
                }
            }

            return await selectionAnswerSetRepository.FindPagedAsync(
                expression,
                o => o.OrderBy(e => e.Id),
                new List<Expression<Func<SelectionAnswerSet, object>>>
                {
                    e => e.Tags,
                    e => e.SelectionAnswerChoices.Select(c => c.LocalizedStrings.Select(s => s.AudioFileMedia.Tags)),
                    e => e.QuestionElements.Select(q => q.QuestionElementToSelectionAnswerChoices)
                },
                searchRequest != null ? searchRequest.Skip : (int?)null,
                searchRequest != null ? searchRequest.Take : (int?)null
            );
        }
    }
}