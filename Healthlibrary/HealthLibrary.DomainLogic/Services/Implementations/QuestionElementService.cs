using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HealthLibrary.Common;
using HealthLibrary.Common.Helpers;
using HealthLibrary.DataAccess;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Enums;
using HealthLibrary.DomainLogic.Extensions;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.DomainLogic.Services.Results;

namespace HealthLibrary.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Contains business rules to manage question elements.
    /// </summary>
    public class QuestionElementService : IQuestionElementService
    {
        private readonly ICareElementContext careElementContext;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<QuestionElement> questionElementRepository;
        private readonly IRepository<AnswerSet> answerSetRepository;
        private readonly IRepository<QuestionElementToScaleAnswerChoice> questionElementScaleAnswerChoiceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionElementService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="careElementContext">The care element context.</param>
        public QuestionElementService(IUnitOfWork unitOfWork, ICareElementContext careElementContext)
        {
            this.careElementContext = careElementContext;
            this.unitOfWork = unitOfWork;
            this.questionElementRepository = this.unitOfWork.CreateGenericRepository<QuestionElement>();
            this.answerSetRepository = this.unitOfWork.CreateGenericRepository<AnswerSet>();
            this.questionElementScaleAnswerChoiceRepository =
                this.unitOfWork.CreateGenericRepository<QuestionElementToScaleAnswerChoice>();
        }

        /// <summary>
        /// Creates new instance of Question element in database.
        /// </summary>
        /// <param name="questionElement"></param>
        /// <returns></returns>
        public async Task<ServiceActionResult<QuestionElementActionStatus, QuestionElement>> Create(
            QuestionElement questionElement)
        {
            var answerSetValidationResult =
                await this.ValidateAnswerSet(questionElement.AnswerSetId, questionElement);

            if (answerSetValidationResult != QuestionElementActionStatus.Success)
            {
                return new ServiceActionResult<QuestionElementActionStatus, QuestionElement>(answerSetValidationResult);
            }

            questionElement.Type = ElementType.Question;

            if (!await this.careElementContext.IsCIUser())
            {
                questionElement.InternalId = null;
            }

            await this.UpdateInternalValues(questionElement.QuestionElementToSelectionAnswerChoices);
            await this.UpdateInternalValues(questionElement.QuestionElementToScaleAnswerChoices);

            this.questionElementRepository.Insert(questionElement);
            await this.unitOfWork.SaveAsync();

            return
                new ServiceActionResult<QuestionElementActionStatus, QuestionElement>(
                    QuestionElementActionStatus.Success, questionElement);
        }

        /// <summary>
        /// Updates data of question element.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <param name="questionElement">The question element.</param>
        /// <returns></returns>
        public async Task<ServiceActionResult<QuestionElementActionStatus, QuestionElement>> Update(
            int customerId,
            Guid questionElementId,
            QuestionElement questionElement
        )
        {
            var dbEntity = await Get(customerId, questionElementId);

            if (dbEntity == null)
            {
                return
                    new ServiceActionResult<QuestionElementActionStatus, QuestionElement>(
                        QuestionElementActionStatus.NotFound);
            }

            questionElement.CustomerId = customerId;
            questionElement.Id = questionElementId;

            var answerSetValidationResult =
                await this.ValidateAnswerSet(questionElement.AnswerSetId, questionElement);

            if (answerSetValidationResult != QuestionElementActionStatus.Success)
            {
                return new ServiceActionResult<QuestionElementActionStatus, QuestionElement>(answerSetValidationResult);
            }

            await this.UpdateInternalValues(dbEntity.QuestionElementToSelectionAnswerChoices,
                questionElement.QuestionElementToSelectionAnswerChoices);
            await this.UpdateInternalValues(dbEntity.QuestionElementToScaleAnswerChoices,
                questionElement.QuestionElementToScaleAnswerChoices);

            dbEntity.QuestionElementToSelectionAnswerChoices.Clear();
            this.questionElementScaleAnswerChoiceRepository.DeleteRange(dbEntity.QuestionElementToScaleAnswerChoices);

            dbEntity.QuestionElementToSelectionAnswerChoices = questionElement.QuestionElementToSelectionAnswerChoices;
            dbEntity.QuestionElementToScaleAnswerChoices = questionElement.QuestionElementToScaleAnswerChoices;
            dbEntity.AnswerSetId = questionElement.AnswerSetId;
            dbEntity.ExternalId = questionElement.ExternalId;
            dbEntity.Tags.UpdateWith(questionElement.Tags);
            dbEntity.AddOrUpdateString(questionElement.LocalizedStrings.First());

            if (await this.careElementContext.IsCIUser())
            {
                dbEntity.InternalId = questionElement.InternalId;
            }

            questionElementRepository.Update(dbEntity);
            await unitOfWork.SaveAsync();

            return
                new ServiceActionResult<QuestionElementActionStatus, QuestionElement>(
                    QuestionElementActionStatus.Success, dbEntity);
        }

        /// <summary>
        /// Updates data of question element.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <param name="localizedString">The localized string.</param>
        /// <returns></returns>
        public async Task<QuestionElement> UpdateLocalizedString(
            int customerId,
            Guid questionElementId, 
            QuestionElementString localizedString
        )
        {
            var dbEntity = await Get(customerId, questionElementId);

            if (dbEntity == null)
            {
                return null;
            }

            dbEntity.AddOrUpdateString(localizedString);

            questionElementRepository.Update(dbEntity);
            await unitOfWork.SaveAsync();

            return dbEntity;
        }

        /// <summary>
        /// Marks the item as deleted.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <returns></returns>
        public async Task<DeleteStatus> Delete(int customerId, Guid questionElementId)
        {
            var dbEntity = await Get(customerId, questionElementId);

            if (dbEntity == null)
            {
                return DeleteStatus.NotFound;
            }

            if (dbEntity.ProtocolElements.Any())
            {
                return DeleteStatus.InUse;
            }

            dbEntity.IsDeleted = true;
            
            questionElementRepository.Update(dbEntity);
            await unitOfWork.SaveAsync();

            return DeleteStatus.Success;
        }


        /// <summary>
        /// Returns required question element by id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <returns></returns>
        public async Task<QuestionElement> Get(int customerId, Guid questionElementId)
        {
            var result = await this.questionElementRepository.FindAsync(
                q => q.CustomerId == customerId && q.Id == questionElementId && !q.IsDeleted,
                null,
                new List<Expression<Func<QuestionElement, object>>>
                {
                    e => e.Tags,
                    e => e.AnswerSet,
                    e => e.LocalizedStrings,
                    e => e.QuestionElementToScaleAnswerChoices,
                    e => e.QuestionElementToSelectionAnswerChoices
                });

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Returns question elements by criteria.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<PagedResult<QuestionElement>> Find(int customerId, TagsSearchDto searchRequest = null)
        {
            Expression<Func<QuestionElement, bool>> expression = q => !q.IsDeleted && q.CustomerId == customerId;

            if (searchRequest != null)
            {
                if (searchRequest.Tags != null && searchRequest.Tags.Any())
                {
                    Expression<Func<QuestionElement, bool>> tagsExpression = PredicateBuilder.False<QuestionElement>();

                    foreach (var tag in searchRequest.Tags)
                    {
                        tagsExpression = tagsExpression.Or(se => se.Tags.Any(t => t.Name.ToLower() == tag.ToLower()));
                    }

                    expression = expression.And(tagsExpression);
                }

                if (!string.IsNullOrEmpty(searchRequest.Q))
                {
                    var currentLanguage =
                        string.IsNullOrEmpty(careElementContext.Language)
                        ? careElementContext.DefaultLanguage
                        : careElementContext.Language;

                    expression = expression
                        .And(q => q.LocalizedStrings.Any(ls => ls.Language.Equals(currentLanguage)));

                    var terms = searchRequest.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(q => q.LocalizedStrings.Any(ls => ls.Value.Contains(term)));
                    }
                }
            }

            return await questionElementRepository.FindPagedAsync(
                expression,
                o => o.OrderBy(e => e.Id),
                new List<Expression<Func<QuestionElement, object>>>
                {
                    e => e.Tags,
                    e => e.AnswerSet,
                    e => e.LocalizedStrings,
                    e => e.QuestionElementToScaleAnswerChoices,
                    e => e.QuestionElementToSelectionAnswerChoices
                },
                searchRequest != null ? searchRequest.Skip : (int?)null,
                searchRequest != null ? searchRequest.Take : (int?)null
            );
        }

        #region Private methods

        /// <summary>
        /// Returns answer set by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<AnswerSet> GetAnswerSet(Guid id)
        {
            var result = await this.answerSetRepository.FindAsync(a => a.Id == id);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Validates that answerset with specified id can be used for provided question.
        /// </summary>
        /// <returns></returns>
        private async Task<QuestionElementActionStatus> ValidateAnswerSet(Guid answerSetId,
            QuestionElement questionElement)
        {
            var answerSet = await this.GetAnswerSet(answerSetId);

            if (answerSet == null)
            {
                return QuestionElementActionStatus.AnswerSetNotExists;
            }

            if (answerSet.CustomerId != careElementContext.CustomerId)
            {
                return QuestionElementActionStatus.AnswerSetCannotBeUsed;
            }

            if (answerSet.Type == AnswerSetType.Scale)
            {
                var scaleAnswerSet = answerSet as ScaleAnswerSet;

                if (questionElement.QuestionElementToSelectionAnswerChoices.Any())
                {
                    return QuestionElementActionStatus.AnswerChoiceCannotBeUsed;
                }

                if (questionElement.QuestionElementToScaleAnswerChoices.Any(
                    c => c.Value > scaleAnswerSet.HighValue || c.Value < scaleAnswerSet.LowValue))
                {
                    return QuestionElementActionStatus.InvalidScaleValueRange;
                }
            }
            else if (answerSet.Type == AnswerSetType.Selection)
            {
                var selectionAnswerSet = answerSet as SelectionAnswerSet;

                if (questionElement.QuestionElementToScaleAnswerChoices.Any() ||
                    questionElement.QuestionElementToSelectionAnswerChoices.Any(
                        c => selectionAnswerSet.SelectionAnswerChoices.All(a => a.Id != c.SelectionAnswerChoiceId)))
                {
                    return QuestionElementActionStatus.AnswerChoiceCannotBeUsed;
                }
            }

            return QuestionElementActionStatus.Success;
        }

        /// <summary>
        /// Updates internal ids for new values in accordance with user access rights.
        /// </summary>
        /// <param name="dbEntities"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        private async Task UpdateInternalValues(
            IEnumerable<QuestionElementToSelectionAnswerChoice> dbEntities,
            IEnumerable<QuestionElementToSelectionAnswerChoice> newValues)
        {
            if (await this.careElementContext.IsCIUser())
            {
                return;
            }

            foreach (var questionElementToSelectionAnswerChoice in newValues)
            {
                var dbAnswerChoice =
                    dbEntities.FirstOrDefault(
                        a => a.SelectionAnswerChoiceId == questionElementToSelectionAnswerChoice.SelectionAnswerChoiceId);

                questionElementToSelectionAnswerChoice.ResetInternalValues(dbAnswerChoice);
            }
        }

        /// <summary>
        /// Updates internal ids for new values in accordance with user access rights.
        /// </summary>
        /// <param name="dbEntities"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        private async Task UpdateInternalValues(
            IEnumerable<QuestionElementToScaleAnswerChoice> dbEntities,
            IEnumerable<QuestionElementToScaleAnswerChoice> newValues)
        {
            if (await this.careElementContext.IsCIUser())
            {
                return;
            }

            foreach (var questionElementToScaleAnswerChoice in newValues)
            {
                var dbAnswerChoice =
                    dbEntities.FirstOrDefault(
                        a => a.Value == questionElementToScaleAnswerChoice.Value);

                questionElementToScaleAnswerChoice.ResetInternalValues(dbAnswerChoice);
            }
        }

        /// <summary>
        /// Updates internal ids for new values in accordance with user access rights.
        /// </summary>
        /// <param name="newValues"></param>
        /// <returns></returns>
        private async Task UpdateInternalValues(
            IEnumerable<IAnalyticsEntity> newValues)
        {
            if (await this.careElementContext.IsCIUser())
            {
                return;
            }

            foreach (var questionElementToSelectionAnswerChoice in newValues)
            {
                questionElementToSelectionAnswerChoice.ResetInternalValues();
            }
        }

        #endregion
    }
}