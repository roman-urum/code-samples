using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthLibrary.Common;
using HealthLibrary.ContentStorage.Azure.Services.Interfaces;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.DomainLogic.Services.Results;
using HealthLibrary.Web.Api.Extensions;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;
using HealthLibrary.Web.Api.Models.Elements.SelectionAnswerChoices;
using HealthLibrary.Web.Api.Models.Elements.SelectionAnswerSets;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// Contains help methods to handle selection answer-set requests.
    /// </summary>
    public class SelectionAnswerSetsControllerHelper : ISelectionAnswerSetControllerHelper
    {
        private readonly ISelectionAnswerSetService selectionAnswerSetService;
        private readonly ICareElementContext careElementContext;
        private readonly IContentStorage contentStorage;
        private readonly IGlobalSearchCacheHelper globalSearchCacheHelper;
        private readonly ITagsSearchCacheHelper tagsSearchCacheHelper;
        private readonly ITagsService tagsService;
        private readonly IMediaFileHelper mediaFileHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionAnswerSetsControllerHelper" /> class.
        /// </summary>
        /// <param name="selectionAnswerSetService">The selection answer set service.</param>
        /// <param name="careElementContext">The care element context.</param>
        /// <param name="contentStorage">The content storage.</param>
        /// <param name="globalSearchCacheHelper">The global search cache helper.</param>
        /// <param name="tagsSearchCacheHelper">The tags search cache helper.</param>
        /// <param name="tagsService">The tags service.</param>
        /// <param name="mediaFileHelper">The audio file helper.</param>
        public SelectionAnswerSetsControllerHelper(
            ISelectionAnswerSetService selectionAnswerSetService,
            ICareElementContext careElementContext,
            IContentStorage contentStorage,
            IGlobalSearchCacheHelper globalSearchCacheHelper,
            ITagsSearchCacheHelper tagsSearchCacheHelper,
            ITagsService tagsService,
            IMediaFileHelper mediaFileHelper
        )
        {
            this.selectionAnswerSetService = selectionAnswerSetService;
            this.careElementContext = careElementContext;
            this.contentStorage = contentStorage;
            this.globalSearchCacheHelper = globalSearchCacheHelper;
            this.tagsSearchCacheHelper = tagsSearchCacheHelper;
            this.tagsService = tagsService;
            this.mediaFileHelper = mediaFileHelper;
        }

        /// <summary>
        /// Creates entity and saves data in datasource.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Id of created entity
        /// </returns>
        public async Task<Guid> Create(int customerId, CreateSelectionAnswerSetRequestDto model)
        {
            var answerSet = Mapper.Map<SelectionAnswerSet>(model);

            answerSet.CustomerId = customerId;
            answerSet.Tags = await tagsService.BuildTagsList(customerId, model.Tags);

            var selectionAnswerChoices = model.SelectionAnswerChoices.ToList();

            foreach (var answerDto in selectionAnswerChoices)
            {
                var answer = await this.MapSelectionAnswerChoice(answerDto, selectionAnswerChoices);

                answerSet.SelectionAnswerChoices.Add(answer);
            }

            var result = await this.selectionAnswerSetService.Create(answerSet);
            await UpdateCachedLists(customerId, result);

            return result.Id;
        }

        /// <summary>
        /// Updates existed answer set with new data.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<ServiceActionResult<SelectionAnswerSetUpdateResult, SelectionAnswerSetResponseDto>> Update(
            int customerId,
            Guid selectionAnswerSetId, 
            UpdateSelectionAnswerSetRequestDto model
        )
        {
            var answerSet = Mapper.Map<SelectionAnswerSet>(model);
            answerSet.Id = selectionAnswerSetId;

            answerSet.Tags = await tagsService.BuildTagsList(customerId, model.Tags);

            var selectionAnswerChoices = model.SelectionAnswerChoices.ToList();

            foreach (var answerDto in selectionAnswerChoices)
            {
                var answer = await this.MapSelectionAnswerChoice(answerDto, selectionAnswerChoices);

                answerSet.SelectionAnswerChoices.Add(answer);
            }

            var result = await this.selectionAnswerSetService.Update(customerId, selectionAnswerSetId, answerSet);

            if (result.Status != SelectionAnswerSetUpdateResult.Success)
            {
                return result.Clone<SelectionAnswerSetResponseDto>();
            }

            await UpdateCachedLists(customerId, result.Content);

            return result.CloneWithMapping(new SelectionAnswerSetResponseDto());
        }

        /// <summary>
        /// Updates existed answer set with new localized strings.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<ServiceActionResult<SelectionAnswerSetResponseDto>> UpdateLocalizedStrings(
            int customerId,
            Guid selectionAnswerSetId,
            UpdateSelectionAnswerSetLocalizedRequestDto model
        )
        {
            var answerChoicesTranslations = new Dictionary<Guid, SelectionAnswerChoiceString>();

            foreach (var answerChoiceDto in model.SelectionAnswerChoices)
            {
                var answerString = await this.MapSelectionAnswerChoiceString(answerChoiceDto.AnswerString);

                answerChoicesTranslations.Add(answerChoiceDto.Id, answerString);
            }

            var result = await this.selectionAnswerSetService.UpdateLocalizedStrings(customerId, selectionAnswerSetId, answerChoicesTranslations);

            return new ServiceActionResult<SelectionAnswerSetResponseDto>(result.Status);
        }

        /// <summary>
        /// Deletes Answerset with specified id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <returns></returns>
        public async Task<DeleteStatus> Delete(int customerId, Guid selectionAnswerSetId)
        {
            var result = await selectionAnswerSetService.Delete(customerId, selectionAnswerSetId);

            if (result == DeleteStatus.Success)
            {
                await globalSearchCacheHelper.RemoveEntry(customerId, selectionAnswerSetId);

                var unusedTags = await tagsService.RemoveUnusedTags(customerId);
                await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);
            }

            return result;
        }

        /// <summary>
        /// Returns answerset with answers for specified culture.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public async Task<SelectionAnswerSetResponseDto> Get(int customerId, Guid selectionAnswerSetId, string language)
        {
            var answerSet = await selectionAnswerSetService.Get(customerId, selectionAnswerSetId);

            return Mapper.Map<SelectionAnswerSet, SelectionAnswerSetResponseDto>(answerSet);
        }

        /// <summary>
        /// Returns model with search results.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<SelectionAnswerSetResponseDto>> Find(
            int customerId,
            SelectionAnswerSetSearchDto criteria
        )
        {
            var result = await selectionAnswerSetService.Find(customerId, criteria);

            return Mapper.Map<PagedResult<SelectionAnswerSet>, PagedResultDto<SelectionAnswerSetResponseDto>>(result);
        }

        #region Private methods

        /// <summary>
        /// Creates instance of SelectionAnswerChoice using dto data.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="selectionAnswerChoices"></param>
        /// <returns></returns>
        private async Task<SelectionAnswerChoice> MapSelectionAnswerChoice(
            CreateSelectionAnswerChoiceRequestDto dto,
            IList<CreateSelectionAnswerChoiceRequestDto> selectionAnswerChoices
        )
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto");
            }

            var answer = Mapper.Map<SelectionAnswerChoice>(dto);

            if (dto.AnswerString != null)
            {
                answer.LocalizedStrings.Add(await this.MapSelectionAnswerChoiceString(dto.AnswerString));
            }

            answer.Sort = selectionAnswerChoices.IndexOf(dto);

            return answer;
        }

        /// <summary>
        /// Creates instance of SelectionAnswerChoice using dto data.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="selectionAnswerChoices">The selection answer choices.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">dto</exception>
        private async Task<SelectionAnswerChoice> MapSelectionAnswerChoice(
            UpdateSelectionAnswerChoiceRequestDto dto,
            IList<UpdateSelectionAnswerChoiceRequestDto> selectionAnswerChoices
        )
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto");
            }

            var result = Mapper.Map<SelectionAnswerChoice>(dto);

            if (dto.AnswerString != null)
            {
                result.LocalizedStrings.Add(await this.MapSelectionAnswerChoiceString(dto.AnswerString));
            }

            if (dto.Id.HasValue)
            {
                result.Id = dto.Id.Value;
            }

            result.Sort = selectionAnswerChoices.IndexOf(dto);

            return result;
        }

        /// <summary>
        /// Creates instance of SelectionAnswerChoiceString using dto data.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task<SelectionAnswerChoiceString> MapSelectionAnswerChoiceString(CreateAnswerStringRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto");
            }

            var result = Mapper.Map<CreateAnswerStringRequestDto, SelectionAnswerChoiceString>(dto);
            result.Language = careElementContext.DefaultLanguage;

            if (dto.AudioFileMedia != null)
            {
                result.AudioFileMedia = await mediaFileHelper.CreateMediaFile(dto.AudioFileMedia);
            }

            return result;
        }

        /// <summary>
        /// Creates instance of SelectionAnswerChoiceString using dto data.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task<SelectionAnswerChoiceString> MapSelectionAnswerChoiceString(UpdateAnswerStringRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto");
            }

            var result = Mapper.Map<UpdateAnswerStringRequestDto, SelectionAnswerChoiceString>(dto);
            result.Language = string.IsNullOrEmpty(careElementContext.Language)
                ? careElementContext.DefaultLanguage
                : careElementContext.Language;

            if (dto.AudioFileMedia == null)
            {
                return result;
            }

            result.AudioFileMedia = await mediaFileHelper.CreateMediaFile(dto.AudioFileMedia);

            return result;
        }

        /// <summary>
        /// Updates cached tags and selection answer set and removes unused tags from db.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSet">The selection answer set.</param>
        /// <returns></returns>
        private async Task UpdateCachedLists(int customerId, SelectionAnswerSet selectionAnswerSet)
        {
            await globalSearchCacheHelper.AddOrUpdateEntry(customerId, Mapper.Map<SelectionAnswerSet, SearchEntryDto>(selectionAnswerSet));
            await tagsSearchCacheHelper.AddOrUpdateTags(customerId, selectionAnswerSet.Tags.Select(t => t.Name).ToList());

            foreach (var selectionAnswerChoice in selectionAnswerSet.SelectionAnswerChoices)
            {
                foreach (var localizedString in selectionAnswerChoice.LocalizedStrings)
                {
                    if (localizedString.AudioFileMedia != null && localizedString.AudioFileMedia.Tags != null)
                    {
                        var tagsToUpdate = localizedString.AudioFileMedia.Tags.Select(t => t.Name).ToList();
                        await tagsSearchCacheHelper.AddOrUpdateTags(customerId, tagsToUpdate);
                    }
                }
            }

            var unusedTags = await tagsService.RemoveUnusedTags(customerId);
            await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);
        }

        #endregion
    }
}