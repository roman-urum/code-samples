using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthLibrary.Common;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Web.Api.Extensions;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.DomainLogic.Services.Results;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;
using HealthLibrary.Web.Api.Models.Elements.QuestionElements;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// Provides methods to manage question elements.
    /// </summary>
    public class QuestionElementControllerHelper : IQuestionElementControllerHelper
    {
        private readonly IQuestionElementService questionElementService;
        private readonly ICareElementContext careElementContext;
        private readonly ITagsService tagsService;
        private readonly IGlobalSearchCacheHelper globalSearchCacheHelper;
        private readonly ITagsSearchCacheHelper tagsSearchCacheHelper;
        private readonly IMediaFileHelper mediaFileHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionElementControllerHelper" /> class.
        /// </summary>
        /// <param name="questionElementService">The question element service.</param>
        /// <param name="careElementContext">The care element context.</param>
        /// <param name="tagsService">The tags service.</param>
        /// <param name="globalSearchCacheHelper">The global search cache helper.</param>
        /// <param name="tagsSearchCacheHelper">The tags search cache helper.</param>
        /// <param name="mediaFileHelper">The audio file helper.</param>
        public QuestionElementControllerHelper(
            IQuestionElementService questionElementService, 
            ICareElementContext careElementContext,
            ITagsService tagsService,
            IGlobalSearchCacheHelper globalSearchCacheHelper,
            ITagsSearchCacheHelper tagsSearchCacheHelper,
            IMediaFileHelper mediaFileHelper
        )
        {
            this.tagsService = tagsService;
            this.questionElementService = questionElementService;
            this.careElementContext = careElementContext;
            this.globalSearchCacheHelper = globalSearchCacheHelper;
            this.tagsSearchCacheHelper = tagsSearchCacheHelper;
            this.mediaFileHelper = mediaFileHelper;
        }

        /// <summary>
        /// Creates new question element.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceActionResult<QuestionElementActionStatus, Guid>> Create(
            int customerId, 
            CreateQuestionElementRequestDto dto
        )
        {
            var entity = Mapper.Map<QuestionElement>(dto);
            entity.CustomerId = customerId;
            entity.LocalizedStrings.Add(await this.MapAnswerString(dto.QuestionElementString));
            entity.Tags = await this.tagsService.BuildTagsList(customerId, dto.Tags);

            if (dto.AnswerChoiceIds != null)
            {
                MapAnswerChoiceIds(entity, dto.AnswerChoiceIds);
            }

            var result = await this.questionElementService.Create(entity);

            if (result.Status != QuestionElementActionStatus.Success)
            {
                return result.Clone<Guid>();
            }

            await globalSearchCacheHelper.AddOrUpdateEntry(customerId, Mapper.Map<QuestionElement, SearchEntryDto>(result.Content));

            await tagsSearchCacheHelper.AddOrUpdateTags(customerId, result.Content.Tags.Select(t => t.Name).ToList());

            var unusedTags = await tagsService.RemoveUnusedTags(customerId);
            await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);

            return result.Clone(result.Content.Id);
        }

        /// <summary>
        /// Updates question element with provided dto.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public async Task<ServiceActionResult<QuestionElementActionStatus, QuestionElementResponseDto>> Update(
            int customerId,
            Guid questionElementId,
            UpdateQuestionElementRequestDto dto
        )
        {
            var entity = Mapper.Map<QuestionElement>(dto);
            entity.LocalizedStrings.Add(await MapAnswerString(dto.QuestionElementString));

            entity.Tags = await this.tagsService.BuildTagsList(customerId, dto.Tags);

            if (dto.AnswerChoiceIds != null)
            {
                MapAnswerChoiceIds(entity, dto.AnswerChoiceIds);
            }

            var result = await this.questionElementService.Update(customerId, questionElementId, entity);

            if (result.Status != QuestionElementActionStatus.Success)
            {
                return result.Clone<QuestionElementResponseDto>();
            }

            await globalSearchCacheHelper.AddOrUpdateEntry(customerId, Mapper.Map<QuestionElement, SearchEntryDto>(result.Content));

            await tagsSearchCacheHelper.AddOrUpdateTags(customerId, result.Content.Tags.Select(t => t.Name).ToList());

            var unusedTags = await tagsService.RemoveUnusedTags(customerId);
            await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);

            return result.CloneWithMapping(new QuestionElementResponseDto());
        }

        /// <summary>
        /// Updates question element with provided dto.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public async Task<QuestionElementResponseDto> UpdateLocalizedString(int customerId, Guid questionElementId, UpdateQuestionElementLocalizedRequestDto dto)
        {
            var localizedString = await MapAnswerString(dto.QuestionElementString);
            var result = await this.questionElementService.UpdateLocalizedString(customerId, questionElementId, localizedString);

            return Mapper.Map<QuestionElementResponseDto>(result);
        }

        /// <summary>
        /// Deletes entity with specified id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <returns></returns>
        public async Task<DeleteStatus> Delete(int customerId, Guid questionElementId)
        {
            var result =  await this.questionElementService.Delete(customerId, questionElementId);

            if (result == DeleteStatus.Success)
            {
                await globalSearchCacheHelper.RemoveEntry(customerId, questionElementId);

                var unusedTags = await tagsService.RemoveUnusedTags(customerId);
                await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);
            }

            return result;
        }

        /// <summary>
        /// Returns required answer set by id or null.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        public async Task<QuestionElementResponseDto> Get(int customerId, Guid questionElementId, bool isBrief)
        {
            var result = await this.questionElementService.Get(customerId, questionElementId);

            return Mapper.Map<QuestionElementResponseDto>(result, o => o.Items.Add("isBrief", isBrief));
        }

        /// <summary>
        /// Returns model with search results.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="criteria">The criteria.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        public async Task<PagedResultDto<QuestionElementResponseDto>> Find(int customerId, TagsSearchDto criteria, bool isBrief)
        {
            var result = await questionElementService.Find(customerId, criteria);

            return Mapper.Map<PagedResult<QuestionElement>, PagedResultDto<QuestionElementResponseDto>>(
                result, 
                o => o.Items.Add("isBrief", isBrief)
            );
        }

        #region Private method

        /// <summary>
        /// Creates instance of SelectionAnswerChoiceString using dto data.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task<QuestionElementString> MapAnswerString(CreateLocalizedStringRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto");
            }

            var result = Mapper.Map<QuestionElementString>(dto);
            result.Language = string.IsNullOrEmpty(careElementContext.Language)
                ? careElementContext.DefaultLanguage
                : careElementContext.Language;

            if (dto.AudioFileMedia == null)
            {
                return result;
            }

            result.AudioFileMedia = await mediaFileHelper.CreateMediaFile(dto.AudioFileMedia);

            if (result.AudioFileMedia != null)
            {
                result.AudioFileMedia.Tags = await tagsService.BuildTagsList(careElementContext.CustomerId, dto.AudioFileMedia.Tags);
            }

            return result;
        }

        /// <summary>
        /// Creates instance of SelectionAnswerChoiceString using dto data.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task<QuestionElementString> MapAnswerString(UpdateLocalizedStringRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto");
            }

            var result = Mapper.Map<QuestionElementString>(dto);
            result.Language = careElementContext.DefaultLanguage;

            if (dto.AudioFileMediaId.HasValue)
            {
                result.AudioFileMediaId = dto.AudioFileMediaId;

                return result;
            }

            if (dto.AudioFileMedia == null)
            {
                return result;
            }

            result.AudioFileMedia = await mediaFileHelper.CreateMediaFile(dto.AudioFileMedia);

            if (result.AudioFileMedia != null)
            {
                result.AudioFileMedia.Tags = await tagsService.BuildTagsList(careElementContext.CustomerId, dto.AudioFileMedia.Tags);
            }

            return result;
        }

        /// <summary>
        /// Initializes ids of answer choices in question element entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="answerChoices"></param>
        private void MapAnswerChoiceIds(QuestionElement entity, IEnumerable<AnswerChoiceIdDto> answerChoices)
        {
            foreach (var answerChoiceIdDto in answerChoices)
            {
                if (answerChoiceIdDto.Id.HasValue)
                {
                    entity.QuestionElementToSelectionAnswerChoices.Add(
                        Mapper.Map<QuestionElementToSelectionAnswerChoice>(answerChoiceIdDto));
                }
                else
                {
                    entity.QuestionElementToScaleAnswerChoices.Add(
                        Mapper.Map<QuestionElementToScaleAnswerChoice>(answerChoiceIdDto));
                }
            }
        }

        #endregion
    }
}