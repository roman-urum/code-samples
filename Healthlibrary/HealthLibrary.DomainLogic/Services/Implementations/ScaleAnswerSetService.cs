using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Common.Helpers;
using HealthLibrary.DataAccess;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Enums;
using HealthLibrary.DomainLogic.Services.Interfaces;

namespace HealthLibrary.DomainLogic.Services.Implementations
{
    /// <summary>
    /// ScaleAnswerSetService.
    /// </summary>
    public class ScaleAnswerSetService : IScaleAnswerSetService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<ScaleAnswerSet> scaleAnswerSetRepository;
        private readonly IRepository<LowLabelScaleAnswerSetString> lowLabelRepository;
        private readonly IRepository<MidLabelScaleAnswerSetString> midLabelRepository;
        private readonly IRepository<HighLabelScaleAnswerSetString> highLabelRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleAnswerSetService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public ScaleAnswerSetService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.scaleAnswerSetRepository = this.unitOfWork.CreateGenericRepository<ScaleAnswerSet>();
        }

        /// <summary>
        /// Creates the specified answer set.
        /// </summary>
        /// <param name="answerSet">The answer set.</param>
        /// <returns></returns>
        public async Task<ScaleAnswerSet> Create(ScaleAnswerSet answerSet)
        {
            answerSet.Type = AnswerSetType.Scale;

            scaleAnswerSetRepository.Insert(answerSet);

            await unitOfWork.SaveAsync();

            return answerSet;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <returns></returns>
        public async Task<ScaleAnswerSet> Get(int customerId, Guid scaleAnswerSetId)
        {
            var result = 
                await scaleAnswerSetRepository
                    .FindAsync(
                        a => a.Id == scaleAnswerSetId && !a.IsDeleted && a.CustomerId == customerId,
                        null,
                        new List<Expression<Func<ScaleAnswerSet, object>>>
                        {
                            e => e.Tags,
                            e => e.QuestionElements,
                            e => e.LowLabelScaleAnswerSetStrings.Select(l => l.AudioFileMedia.Tags),
                            e => e.LowLabelScaleAnswerSetStrings.Select(l => l.AudioFileMedia.LocalizedStrings),
                            e => e.MidLabelScaleAnswerSetStrings.Select(l => l.AudioFileMedia.Tags),
                            e => e.MidLabelScaleAnswerSetStrings.Select(l => l.AudioFileMedia.LocalizedStrings),
                            e => e.HighLabelScaleAnswerSetStrings.Select(l => l.AudioFileMedia.Tags),
                            e => e.HighLabelScaleAnswerSetStrings.Select(l => l.AudioFileMedia.LocalizedStrings)
                        }
                );

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Updates the specified answer set.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <param name="answerSet">The answer set.</param>
        /// <returns></returns>
        public async Task<UpdateScaleAnswerSetStatus> Update(int customerId, Guid scaleAnswerSetId, ScaleAnswerSet answerSet)
        {
            var dbEntity = await Get(customerId, scaleAnswerSetId);

            if (dbEntity == null)
            {
                return UpdateScaleAnswerSetStatus.NotFound;
            }

            dbEntity.CustomerId = answerSet.CustomerId;
            dbEntity.Tags.RemoveRange(dbEntity.Tags.ToList());
            dbEntity.Tags.AddRange(answerSet.Tags.ToList());

            dbEntity.Name = answerSet.Name;
            dbEntity.HighValue = answerSet.HighValue;
            dbEntity.LowValue = answerSet.LowValue;

            var lowLabel = answerSet.LowLabelScaleAnswerSetStrings.First();
            var midLabel = answerSet.MidLabelScaleAnswerSetStrings.FirstOrDefault();
            var highLabel = answerSet.HighLabelScaleAnswerSetStrings.First();

            var dbLowLabel = dbEntity.LowLabelScaleAnswerSetStrings.FirstOrDefault(
                l => l.Language == lowLabel.Language);
            var dbHighLabel = dbEntity.HighLabelScaleAnswerSetStrings.FirstOrDefault(
                l => l.Language == highLabel.Language);

            UpdateScaleAnswerSetStatus status = 0;

            if (dbLowLabel == null)
            {
                status = status | UpdateScaleAnswerSetStatus.LowLabelLanguageNotMatch;
            }
            if (dbHighLabel == null)
            {
                status = status | UpdateScaleAnswerSetStatus.HighLabelLanguageNotMatch;
            }

            if (status > 0)
            {
                return status;
            }

            if (midLabel != null)
            {
                var dbMidLabel = dbEntity.MidLabelScaleAnswerSetStrings
                    .FirstOrDefault(l => l.Language == midLabel.Language);

                if (dbMidLabel == null)
                {
                    dbMidLabel = new MidLabelScaleAnswerSetString();
                    UpdateLabelFields(midLabel, dbMidLabel);
                    dbEntity.MidLabelScaleAnswerSetStrings.Add(dbMidLabel);
                }
                else
                {
                    UpdateLabelFields(midLabel, dbMidLabel);
                }

            }

            UpdateLabelFields(lowLabel, dbLowLabel);
            UpdateLabelFields(highLabel, dbHighLabel);

            scaleAnswerSetRepository.Update(dbEntity);
            await unitOfWork.SaveAsync();

            return UpdateScaleAnswerSetStatus.Success;
        }

        private void UpdateLabelFields<T>(T source,
            T destination) where T : LocalizedString
        {
            destination.Language = source.Language;
            destination.Pronunciation = source.Pronunciation;
            destination.Value = source.Value;
            destination.Description = source.Description;

            if (destination.AudioFileMedia == null)
            {
                destination.AudioFileMedia = source.AudioFileMedia;
            }
            else
            {
                UpdateMediaFields(source.AudioFileMedia,
                    destination.AudioFileMedia);
            }
        }

        private void UpdateMediaFields(Media source, Media destination)
        {
            if (source != null)
            {
                destination.ContentLength = source.ContentLength;
                destination.ContentType = source.ContentType;
                destination.Description = source.Description;
                destination.CustomerId = source.CustomerId;
                destination.Name = source.Name;
                destination.OriginalStorageKey = source.OriginalStorageKey;
            }
        }

        /// <summary>
        /// Sets specified entity as deleted in db.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <returns></returns>
        public async Task<DeleteStatus> Delete(int customerId, Guid scaleAnswerSetId)
        {
            var entity = await Get(customerId, scaleAnswerSetId);

            if (entity == null)
            {
                return DeleteStatus.NotFound;
            }

            if (entity.QuestionElements != null && entity.QuestionElements.Any(q => !q.IsDeleted))
            {
                return DeleteStatus.InUse;
            }

            entity.IsDeleted = true;

            scaleAnswerSetRepository.Update(entity);

            await unitOfWork.SaveAsync();

            return DeleteStatus.Success;
        }

        /// <summary>
        /// Finds the specified keyword.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<PagedResult<ScaleAnswerSet>> Find(int customerId, TagsSearchDto searchRequest = null)
        {
            Expression<Func<ScaleAnswerSet, bool>> expression = s => !s.IsDeleted && s.CustomerId == customerId;

            if (searchRequest != null)
            {
                if (searchRequest.Tags != null && searchRequest.Tags.Any())
                {
                    Expression<Func<ScaleAnswerSet, bool>> tagsExpression = PredicateBuilder.False<ScaleAnswerSet>();

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
            }

            return await scaleAnswerSetRepository.FindPagedAsync(
                expression,
                o => o.OrderBy(e => e.Id),
                new List<Expression<Func<ScaleAnswerSet, object>>>
                {
                    e => e.Tags,
                    e => e.QuestionElements,
                    e => e.LowLabelScaleAnswerSetStrings.Select(l => l.AudioFileMedia.Tags),
                    e => e.LowLabelScaleAnswerSetStrings.Select(l => l.AudioFileMedia.LocalizedStrings),
                    e => e.MidLabelScaleAnswerSetStrings.Select(l => l.AudioFileMedia.Tags),
                    e => e.MidLabelScaleAnswerSetStrings.Select(l => l.AudioFileMedia.LocalizedStrings),
                    e => e.HighLabelScaleAnswerSetStrings.Select(l => l.AudioFileMedia.Tags),
                    e => e.HighLabelScaleAnswerSetStrings.Select(l => l.AudioFileMedia.LocalizedStrings)
                },
                searchRequest != null ? searchRequest.Skip : (int?)null,
                searchRequest != null ? searchRequest.Take : (int?)null
            );
        }

        /// <summary>
        /// Updates only labels for Answer set.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="answerSetId">The answer set identifier.</param>
        /// <param name="lowLabel">The low label.</param>
        /// <param name="midLabel">The mid label.</param>
        /// <param name="highLabel">The high label.</param>
        /// <returns></returns>
        public async Task<UpdateScaleAnswerSetLocalization> UpdateLabels(
            int customerId,
            Guid answerSetId,
            LowLabelScaleAnswerSetString lowLabel,
            MidLabelScaleAnswerSetString midLabel, 
            HighLabelScaleAnswerSetString highLabel
        )
        {
            var answerSet = await Get(customerId, answerSetId);

            if (answerSet == null)
            {
                return UpdateScaleAnswerSetLocalization.NotFound;
            }

            // Low label.
            if (answerSet.LowLabelScaleAnswerSetStrings.All(l => l.Language != lowLabel.Language))
            {
                //lowLabelRepository.Insert(lowLabel);
                answerSet.LowLabelScaleAnswerSetStrings.Add(lowLabel);
            }
            else
            {
                var dbLowLabel = answerSet.LowLabelScaleAnswerSetStrings
                    .First(l => l.Language == lowLabel.Language);
                UpdateLabelFields(lowLabel, dbLowLabel);
            }

            // Mid label.
            if (midLabel != null && answerSet.MidLabelScaleAnswerSetStrings.All(l => l.Language != midLabel.Language))
            {
                //midLabelRepository.Insert(midLabel);
                answerSet.MidLabelScaleAnswerSetStrings.Add(midLabel);
            }
            if (midLabel != null && answerSet.MidLabelScaleAnswerSetStrings.Any(l => l.Language == midLabel.Language))
            {
                var dbMidLabel = answerSet.MidLabelScaleAnswerSetStrings
                    .First(l => l.Language == midLabel.Language);
                UpdateLabelFields(midLabel, dbMidLabel);
            }

            // High label.
            if (answerSet.HighLabelScaleAnswerSetStrings.All(l => l.Language != highLabel.Language))
            {
                //highLabelRepository.Insert(highLabel);
                answerSet.HighLabelScaleAnswerSetStrings.Add(highLabel);
            }
            else
            {
                var dbHighLabel = answerSet.HighLabelScaleAnswerSetStrings
                    .First(l => l.Language == highLabel.Language);
                UpdateLabelFields(highLabel, dbHighLabel);
            }

            scaleAnswerSetRepository.Update(answerSet);
            unitOfWork.SaveAsync();

            return UpdateScaleAnswerSetLocalization.Success;
        }

        public async Task<ScaleAnswerSet> GetForCustomer(Guid id, int customerId)
        {
            var customer = await scaleAnswerSetRepository.FindAsync(a => a.CustomerId == customerId);

            return customer.FirstOrDefault();
        }
    }
}
