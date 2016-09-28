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
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;
using HealthLibrary.Web.Api.Models.Elements.ScaleAnswerSets;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// ScaleAnswerSetHelper.
    /// </summary>
    public class ScaleAnswerSetControllerHelper : IScaleAnswerSetControllerHelper
    {
        private readonly IScaleAnswerSetService scaleAnswerSetService;
        private readonly ICareElementContext careElementContext;
        private readonly ITagsService tagsService;
        private readonly IGlobalSearchCacheHelper globalSearchCacheHelper;
        private readonly ITagsSearchCacheHelper tagsSearchCacheHelper;
        private readonly IMediaFileHelper mediaFileHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleAnswerSetControllerHelper" /> class.
        /// </summary>
        /// <param name="scaleAnswerSetService">The scale answer set service.</param>
        /// <param name="careElementContext">The care element context.</param>
        /// <param name="tagsService">The tags service.</param>
        /// <param name="globalSearchCacheHelper">The global search cache helper.</param>
        /// <param name="tagsSearchCacheHelper">The tags search cache helper.</param>
        /// <param name="mediaFileHelper">The audio file helper.</param>
        public ScaleAnswerSetControllerHelper(IScaleAnswerSetService scaleAnswerSetService,
            ICareElementContext careElementContext, ITagsService tagsService,
            IGlobalSearchCacheHelper globalSearchCacheHelper,
            ITagsSearchCacheHelper tagsSearchCacheHelper,
            IMediaFileHelper mediaFileHelper
        )
        {
            this.scaleAnswerSetService = scaleAnswerSetService;
            this.careElementContext = careElementContext;
            this.tagsService = tagsService;
            this.globalSearchCacheHelper = globalSearchCacheHelper;
            this.tagsSearchCacheHelper = tagsSearchCacheHelper;
            this.mediaFileHelper = mediaFileHelper;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, CreateScaleAnswerSetStatus>> Create(int customerId, CreateScaleAnswerSetRequestDto model)
        {
            var status = ValidateLabels(model);

            if (status > 0)
            {
                return new OperationResultDto<Guid, CreateScaleAnswerSetStatus>()
                {
                    Status = status
                };
            }

            var answerSet = Mapper.Map<ScaleAnswerSet>(model);

            answerSet.CustomerId = customerId;
            answerSet.Tags = await tagsService.BuildTagsList(customerId, model.Tags);

            var lowLabel = await MapLabelToLocalizedString<LowLabelScaleAnswerSetString>(
                model.Labels.LowLabel, careElementContext.DefaultLanguage);
            answerSet.LowLabelScaleAnswerSetStrings.Add(lowLabel);

            // Mid is optional field.
            if (model.Labels.MidLabel != null)
            {
                var midLabel = await MapLabelToLocalizedString<MidLabelScaleAnswerSetString>
                    (model.Labels.MidLabel, careElementContext.DefaultLanguage);
                answerSet.MidLabelScaleAnswerSetStrings.Add(midLabel);
            }

            var highLabel = await MapLabelToLocalizedString<
                HighLabelScaleAnswerSetString>(model.Labels.HighLabel,
                careElementContext.DefaultLanguage);
            answerSet.HighLabelScaleAnswerSetStrings.Add(highLabel);

            var result = await scaleAnswerSetService.Create(answerSet);

            await globalSearchCacheHelper.AddOrUpdateEntry(customerId, Mapper.Map<ScaleAnswerSet, SearchEntryDto>(result));

            await tagsSearchCacheHelper.AddOrUpdateTags(customerId, result.Tags.Select(t => t.Name).ToList());

            var unusedTags = await tagsService.RemoveUnusedTags(customerId);
            await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);

            return new OperationResultDto<Guid, CreateScaleAnswerSetStatus>()
            {
                Content = result.Id,
                Status = CreateScaleAnswerSetStatus.Success
            };
        }

        private CreateScaleAnswerSetStatus ValidateLabels(CreateScaleAnswerSetRequestDto model)
        {
            CreateScaleAnswerSetStatus status = 0;

            if (string.IsNullOrEmpty(model.Labels.HighLabel.Value))
            {
                status |= CreateScaleAnswerSetStatus.HighLabelIsRequired;
            }

            if (string.IsNullOrEmpty(model.Labels.LowLabel.Value))
            {
                status |= CreateScaleAnswerSetStatus.LowLabelIsRequired;
            }

            return status;
        }

        private async Task<T> MapLabelToLocalizedString<T>(CreateLocalizedStringRequestDto dto, string language)
            where T : LocalizedString
        {
            var label = Mapper.Map<T>(dto);
            label.Language = language;

            if (dto.AudioFileMedia != null)
            {
                label.AudioFileMedia = await mediaFileHelper.CreateMediaFile(dto.AudioFileMedia);
            }

            return label;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public async Task<ScaleAnswerSetResponseDto> Get(int customerId, Guid scaleAnswerSetId, string language)
        {
            var answerSet = await scaleAnswerSetService.Get(customerId, scaleAnswerSetId);
          
            return Mapper.Map<ScaleAnswerSet, ScaleAnswerSetResponseDto>(answerSet);
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<UpdateScaleAnswerSetStatus> Update(int customerId, Guid scaleAnswerSetId, UpdateScaleAnswerSetRequestDto model)
        {
            var status = ValidateUpdateLabels(model);

            if (status > 0)
            {
                return status;
            }

            var answerSet = Mapper.Map<ScaleAnswerSet>(model);
            answerSet.Id = scaleAnswerSetId;
            answerSet.CustomerId = customerId;
            answerSet.Tags = await tagsService.BuildTagsList(customerId, model.Tags);

            var lowLabel = Mapper.Map<LowLabelScaleAnswerSetString>(model.Labels.LowLabel);
            lowLabel.Language = careElementContext.DefaultLanguage;

            if (model.Labels.LowLabel.AudioFileMedia != null)
            {
                lowLabel.AudioFileMedia = await mediaFileHelper.CreateMediaFile(model.Labels.LowLabel.AudioFileMedia);
            }

            answerSet.LowLabelScaleAnswerSetStrings.Add(lowLabel);

            // Mid is optional
            if (model.Labels.MidLabel != null)
            {
                var midLabel = Mapper.Map<MidLabelScaleAnswerSetString>(model.Labels.MidLabel);
                midLabel.Language = careElementContext.DefaultLanguage;
                if (model.Labels.MidLabel.AudioFileMedia != null)
                {
                    midLabel.AudioFileMedia = await mediaFileHelper.CreateMediaFile(model.Labels.MidLabel.AudioFileMedia);
                }
                answerSet.MidLabelScaleAnswerSetStrings.Add(midLabel);
            }

            var highLabel = Mapper.Map<HighLabelScaleAnswerSetString>(model.Labels.HighLabel);
            highLabel.Language = careElementContext.DefaultLanguage;
            if (model.Labels.HighLabel.AudioFileMedia != null)
            {
                highLabel.AudioFileMedia = await mediaFileHelper.CreateMediaFile(model.Labels.HighLabel.AudioFileMedia);
            }
            answerSet.HighLabelScaleAnswerSetStrings.Add(highLabel);

            status = await scaleAnswerSetService.Update(customerId, scaleAnswerSetId, answerSet);

            await globalSearchCacheHelper.AddOrUpdateEntry(customerId, Mapper.Map<ScaleAnswerSet, SearchEntryDto>(answerSet));

            await tagsSearchCacheHelper.AddOrUpdateTags(customerId, answerSet.Tags.Select(t => t.Name).ToList());

            var unusedTags = await tagsService.RemoveUnusedTags(customerId);
            await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);

            return status;
        }

        private UpdateScaleAnswerSetStatus ValidateUpdateLabels(UpdateScaleAnswerSetRequestDto model)
        {
            UpdateScaleAnswerSetStatus status = 0;

            if (string.IsNullOrEmpty(model.Labels.HighLabel.Value))
            {
                status |= UpdateScaleAnswerSetStatus.HighLabelIsRequired;
            }

            if (string.IsNullOrEmpty(model.Labels.LowLabel.Value))
            {
                status |= UpdateScaleAnswerSetStatus.LowLabelIsRequired;
            }

            return status;
        }

        /// <summary>
        /// Deletes Answerset with specified id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <returns></returns>
        public async Task<DeleteStatus> Delete(int customerId, Guid scaleAnswerSetId)
        {
            var result = await scaleAnswerSetService.Delete(customerId, scaleAnswerSetId);

            if (result == DeleteStatus.Success)
            {
                await globalSearchCacheHelper.RemoveEntry(customerId, scaleAnswerSetId);

                var unusedTags = await tagsService.RemoveUnusedTags(customerId);
                await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);
            }

            return result;
        }

        /// <summary>
        /// Finds the specified model.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<ScaleAnswerSetResponseDto>> Find(int customerId, TagsSearchDto model = null)
        {
            var result = await scaleAnswerSetService.Find(customerId, model);

            return Mapper.Map<PagedResult<ScaleAnswerSet>, PagedResultDto<ScaleAnswerSetResponseDto>>(result);
        }

        /// <summary>
        /// Updates the localized strings.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public async Task<UpdateScaleAnswerSetLocalization> UpdateLocalizedStrings(
            int customerId,
            Guid scaleAnswerSetId,
            UpdateScaleAnswerSetLocalizedRequestDto model,
            string language
        )
        {
            var lowLabel = await MapUpdatingLabelToLocalizedString<LowLabelScaleAnswerSetString>(model.LowLabel, language);

            MidLabelScaleAnswerSetString midLabel = null;

            if (model.MidLabel != null && model.MidLabel.Value != null)
            {
                midLabel = await MapUpdatingLabelToLocalizedString<MidLabelScaleAnswerSetString>(model.MidLabel, language);
            }

            var highLabel = await MapUpdatingLabelToLocalizedString<HighLabelScaleAnswerSetString>(model.HighLabel, language);

            var status = await scaleAnswerSetService.UpdateLabels(customerId, scaleAnswerSetId, lowLabel, midLabel, highLabel);

            return status;
        }

        private async Task<TM> MapUpdatingLabelToLocalizedString<TM>(UpdateLocalizedStringRequestDto dto, string language) 
            where TM : LocalizedString
        {
            var label = Mapper.Map<TM>(dto);
            label.Language = language;

            if (dto.AudioFileMedia != null)
            {
                label.AudioFileMedia = await mediaFileHelper.CreateMediaFile(dto.AudioFileMedia);
            }

            return label;
        }
    }
}