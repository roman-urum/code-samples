using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;
using HealthLibrary.Web.Api.Models.Elements.TextMediaElements;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// TextMediaElementsControllerHelper.
    /// </summary>
    public class TextMediaElementsControllerHelper : ITextMediaElementsControllerHelper
    {
        private ITextMediaElementsService textMediaElementsService;
        private ITagsService tagsService;
        private IMediaService mediaService;
        private readonly IGlobalSearchCacheHelper globalSearchCacheHelper;
        private readonly ITagsSearchCacheHelper tagsSearchCacheHelper;
        private IMediaFileHelper mediaFileHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextMediaElementsControllerHelper" /> class.
        /// </summary>
        /// <param name="textMediaElementsService">The text media elements service.</param>
        /// <param name="tagsService">The tags service.</param>
        /// <param name="mediaService">The media service.</param>
        /// <param name="globalSearchCacheHelper">The global search cache helper.</param>
        /// <param name="tagsSearchCacheHelper">The tags search cache helper.</param>
        /// <param name="mediaFileHelper">The audio file helper.</param>
        public TextMediaElementsControllerHelper(
            ITextMediaElementsService textMediaElementsService,
            ITagsService tagsService,
            IMediaService mediaService,
            IGlobalSearchCacheHelper globalSearchCacheHelper,
            ITagsSearchCacheHelper tagsSearchCacheHelper,
            IMediaFileHelper mediaFileHelper
        )
        {
            this.textMediaElementsService = textMediaElementsService;
            this.tagsService = tagsService;
            this.mediaService = mediaService;
            this.globalSearchCacheHelper = globalSearchCacheHelper;
            this.tagsSearchCacheHelper = tagsSearchCacheHelper;
            this.mediaFileHelper = mediaFileHelper;
        }

        /// <summary>
        /// Creates the specified new text media element dto.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="newTextMediaElementDto">The new text media element dto.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, CreateTextMediaElementStatus>> Create(
            int customerId, 
            CreateTextMediaElementRequestDto newTextMediaElementDto
        )
        {
            var validationResult = await ValidateCreateTextMediaElement(customerId, newTextMediaElementDto);

            if (!validationResult.HasFlag(CreateTextMediaElementStatus.Success))
            {
                return new OperationResultDto<Guid, CreateTextMediaElementStatus>()
                {
                    Status = validationResult
                };
            }

            var elementTags = await tagsService.BuildTagsList(customerId, newTextMediaElementDto.Tags);
            var mappedTextMediaElement = Mapper.Map<CreateTextMediaElementRequestDto, TextMediaElement>(newTextMediaElementDto);

            mappedTextMediaElement.CustomerId = customerId;
            mappedTextMediaElement.Tags = elementTags;

            var elementString = Mapper.Map<CreateLocalizedStringRequestDto, TextMediaElementString>(newTextMediaElementDto.Text);

            if (elementString != null)
            {
                elementString.Language = CareElementRequestContext.Current.DefaultLanguage;

                mappedTextMediaElement.TextLocalizedStrings = new List<TextMediaElementString>()
                {
                    elementString
                };

                if (newTextMediaElementDto.Text.AudioFileMedia != null)
                {
                    elementString.AudioFileMedia = await mediaFileHelper.CreateMediaFile(newTextMediaElementDto.Text.AudioFileMedia);                    
                }
            }

            if (newTextMediaElementDto.MediaId.HasValue)
            {
                mappedTextMediaElement.TextMediaElementsToMedias = new List<TextMediaElementToMedia>()
                {
                    new TextMediaElementToMedia()
                    {
                        MediaId = newTextMediaElementDto.MediaId.Value,
                        Language = CareElementRequestContext.Current.DefaultLanguage
                    }
                };
            } else if (newTextMediaElementDto.Media != null)
            {
                var media = await mediaFileHelper.CreateMediaFile(newTextMediaElementDto.Media);

                if (media != null)
                {
                    mappedTextMediaElement.TextMediaElementsToMedias = new List<TextMediaElementToMedia>()
                    {
                        new TextMediaElementToMedia()
                        {
                            Media = media,
                            Language = CareElementRequestContext.Current.DefaultLanguage
                        }
                    };
                }
            }

            var result = await textMediaElementsService.Create(mappedTextMediaElement);

            await globalSearchCacheHelper.AddOrUpdateEntry(customerId, Mapper.Map<TextMediaElement, SearchTextAndMediaDto>(result));

            await tagsSearchCacheHelper.AddOrUpdateTags(customerId, result.Tags.Select(t => t.Name).ToList());

            var unusedTags = await tagsService.RemoveUnusedTags(customerId);
            await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);

            return new OperationResultDto<Guid, CreateTextMediaElementStatus>()
            {
                Status = CreateTextMediaElementStatus.Success,
                Content = mappedTextMediaElement.Id
            };
        }

        /// <summary>
        /// Gets the elements.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchCriteriaDto">The search criteria dto.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<TextMediaElementResponseDto>> GetElements(
            int customerId,
            TextMediaElementSearchDto searchCriteriaDto
        )
        {
            var result = await textMediaElementsService.GetElements(customerId, searchCriteriaDto);

            return Mapper.Map<PagedResult<TextMediaElement>, PagedResultDto<TextMediaElementResponseDto>>(result);
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="textMediaElementId">The identifier.</param>
        /// <param name="textMediaElementDto">The text media element dto.</param>
        /// <returns></returns>
        public async Task<UpdateTextMediaElementStatus> Update(
            int customerId,
            Guid textMediaElementId,
            UpdateTextMediaElementRequestDto textMediaElementDto
        )
        {
            var validationResult = ValidateUpdateTextMediaElement(textMediaElementDto);

            if (validationResult != UpdateTextMediaElementStatus.Success)
            {
                return validationResult;
            }

            var element = await textMediaElementsService.GetElement(customerId, textMediaElementId);

            if (element == null)
            {
                return UpdateTextMediaElementStatus.NotFound;
            }

            element.Name = textMediaElementDto.Name;
            element.MediaType = textMediaElementDto.Type;

            element.Tags.RemoveRange(element.Tags.ToList());

            var newElementTags = await this.tagsService.BuildTagsList(customerId, textMediaElementDto.Tags);
            element.Tags.AddRange(newElementTags);

            string language = CareElementRequestContext.Current.DefaultLanguage;

            if (textMediaElementDto.Text != null)
            {
                var localizedString = element.TextLocalizedStrings.FirstOrDefault(s => s.Language == language);

                if (localizedString == null)
                {
                    localizedString = new TextMediaElementString()
                    {
                        TextMediaElement = element
                    };

                    element.TextLocalizedStrings.Add(localizedString);
                    localizedString.Language = language;
                }

                localizedString.Value = textMediaElementDto.Text.Value;
                localizedString.Description = textMediaElementDto.Text.Description;
                localizedString.Pronunciation = textMediaElementDto.Text.Pronunciation;

                if (textMediaElementDto.Text.AudioFileMedia != null)
                {
                    if (textMediaElementDto.Text.AudioFileMediaId.HasValue)
                    {
                        localizedString.AudioFileMediaId = textMediaElementDto.Text.AudioFileMediaId.Value;
                    }
                    else
                    {
                        localizedString.AudioFileMedia =
                            await mediaFileHelper.CreateMediaFile(textMediaElementDto.Text.AudioFileMedia);
                    }
                }
                else
                {
                    localizedString.AudioFileMediaId = null;
                }
            }
            else
            {
                element.TextLocalizedStrings.RemoveRange(
                    element.TextLocalizedStrings.Where(s => s.Language == language).ToList()
                );
            }

            #region Media

            if (element.TextMediaElementsToMedias.Any(m => m.Language == language))
            {
                element.TextMediaElementsToMedias.RemoveRange(
                    element.TextMediaElementsToMedias.Where(m => m.Language == language).ToList()
                );
            }

            if (textMediaElementDto.MediaId.HasValue)
            {
                var existingMedia = await mediaService.Get(customerId, textMediaElementDto.MediaId.Value);

                if (existingMedia == null)
                {
                    return UpdateTextMediaElementStatus.MediaFileNotFound;
                }

                var media = new TextMediaElementToMedia()
                {
                    MediaId = textMediaElementDto.MediaId.Value,
                    Language = language
                };

                element.TextMediaElementsToMedias.Add(media);
            }
            else if (textMediaElementDto.Media != null)
            {
                var media = await mediaFileHelper.CreateMediaFile(textMediaElementDto.Media);

                if (media != null)
                {
                    element.TextMediaElementsToMedias.Add(
                        new TextMediaElementToMedia()
                        {
                            Media = media,
                            Language = language
                        }
                    );
                }
            }

            #endregion

            await textMediaElementsService.Update(element);

            await globalSearchCacheHelper.AddOrUpdateEntry(customerId, Mapper.Map<TextMediaElement, SearchTextAndMediaDto>(element));

            await tagsSearchCacheHelper.AddOrUpdateTags(customerId, element.Tags.Select(t => t.Name).ToList());

            var unusedTags = await tagsService.RemoveUnusedTags(customerId);
            await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);

            return UpdateTextMediaElementStatus.Success;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="textMediaElementId">The text media element identifier.</param>
        /// <returns></returns>
        public async Task<DeteleTextMediaElementStatus> Delete(int customerId, Guid textMediaElementId)
        {
            var language = CareElementRequestContext.Current.Language;

            var result = await textMediaElementsService.Delete(customerId, textMediaElementId, language);

            if (result == DeteleTextMediaElementStatus.Success)
            {
                await globalSearchCacheHelper.RemoveEntry(customerId, textMediaElementId);

                var unusedTags = await tagsService.RemoveUnusedTags(customerId);
                await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);
            }

            return result;
        }

        /// <summary>
        /// Updates the localization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="textMediaElementId">The text media element identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<UpdateTextMediaElementStatus> UpdateLocalization(
            int customerId, 
            Guid textMediaElementId,
            UpdateTextMediaElementLocalizedRequestDto model
        )
        {
            if (model.Type != null && model.Media == null && !model.MediaId.HasValue)
            {
                return UpdateTextMediaElementStatus.MediaFileMissed;
            }

            if (model.Media != null && !model.MediaId.HasValue && model.Type == null)
            {
                return UpdateTextMediaElementStatus.MediaMediaType;
            }

            if (model.Text == null && model.Media == null && !model.MediaId.HasValue)
            {
                return UpdateTextMediaElementStatus.TextOrMediaMissed;
            }

            var element = await this.textMediaElementsService.GetElement(customerId, textMediaElementId);

            if (element == null)
            {
                return UpdateTextMediaElementStatus.NotFound;
            }

            var language = CareElementRequestContext.Current.Language;

            if (model.Text != null)
            {
                var elementLocalizedText = element.TextLocalizedStrings.FirstOrDefault(ls => ls.Language == language);

                if (elementLocalizedText == null)
                {
                    elementLocalizedText = Mapper.Map<TextMediaElementString>(model.Text);
                    elementLocalizedText.Language = language;
                    element.TextLocalizedStrings.Add(elementLocalizedText);
                }
                else
                {
                    elementLocalizedText.Value = model.Text.Value;
                    elementLocalizedText.Description = model.Text.Description;
                    elementLocalizedText.Pronunciation = model.Text.Pronunciation;
                }

                if (model.Text.AudioFileMedia != null)
                {
                    elementLocalizedText.AudioFileMedia =
                        await mediaFileHelper.CreateMediaFile(model.Text.AudioFileMedia);
                }
            }
            else
            {
                element.TextLocalizedStrings.RemoveRange(
                    element.TextLocalizedStrings.Where(s => s.Language == language).ToList()
                );
            }

            #region Media

            if (element.TextMediaElementsToMedias.Any(m => m.Language == language))
            {
                element.TextMediaElementsToMedias.RemoveRange(
                    element.TextMediaElementsToMedias.Where(m => m.Language == language).ToList()
                );
            }

            if (model.MediaId.HasValue)
            {
                var existingMedia = await mediaService.Get(customerId, model.MediaId.Value);

                if (existingMedia == null)
                {
                    return UpdateTextMediaElementStatus.MediaFileNotFound;
                }

                var media = new TextMediaElementToMedia()
                {
                    MediaId = model.MediaId.Value,
                    Language = language
                };

                element.TextMediaElementsToMedias.Add(media);
            }
            else if (model.Media != null)
            {
                var media = await mediaFileHelper.CreateMediaFile(model.Media);

                if (media != null)
                {
                    element.TextMediaElementsToMedias.Add(
                        new TextMediaElementToMedia()
                        {
                            Media = media,
                            Language = language
                        }
                    );
                }
            }

            #endregion

            await this.textMediaElementsService.Update(element);

            return UpdateTextMediaElementStatus.Success;
        }

        /// <summary>
        /// Get text and mediaelement by id
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="textMediaElementId">The text media element identifier.</param>
        /// <returns></returns>
        public async Task<TextMediaElementResponseDto> GetElement(int customerId, Guid textMediaElementId)
        {
            var element = await this.textMediaElementsService.GetElement(customerId, textMediaElementId);

            return Mapper.Map<TextMediaElement, TextMediaElementResponseDto>(element);
        }

        private async Task<CreateTextMediaElementStatus> ValidateCreateTextMediaElement(
            int customerId,
            CreateTextMediaElementRequestDto newTextMediaElementDto
        )
        {
            if (newTextMediaElementDto.Type != null && newTextMediaElementDto.Media == null &&
                !newTextMediaElementDto.MediaId.HasValue)
            {
                return CreateTextMediaElementStatus.MediaFileMissed;
            }

            if (newTextMediaElementDto.Media != null && !newTextMediaElementDto.MediaId.HasValue &&
                newTextMediaElementDto.Type == null)
            {
                return CreateTextMediaElementStatus.MediaMediaType;
            }

            if (newTextMediaElementDto.MediaId.HasValue)
            {
                var media = await this.mediaService.Get(customerId, newTextMediaElementDto.MediaId.Value);

                if (media == null)
                {
                    return CreateTextMediaElementStatus.MediaFileNotFound;
                }
            }

            if (newTextMediaElementDto.Text == null && newTextMediaElementDto.Media == null && !newTextMediaElementDto.MediaId.HasValue)
            {
                return CreateTextMediaElementStatus.TextOrMediaMissed;
            }

            if (newTextMediaElementDto.Tags != null &&
                newTextMediaElementDto.Tags.Count != newTextMediaElementDto.Tags.Distinct().Count())
            {
                return CreateTextMediaElementStatus.DupplicateTags;
            }

            if (newTextMediaElementDto.Tags != null && newTextMediaElementDto.Tags.Any() && newTextMediaElementDto.Tags.Max(t => t.Length) > 30)
            {
                return CreateTextMediaElementStatus.TagLengthMustBeLessThan;
            }

            return CreateTextMediaElementStatus.Success;
        }

        private UpdateTextMediaElementStatus ValidateUpdateTextMediaElement(UpdateTextMediaElementRequestDto textMediaElementDto)
        {
            if (textMediaElementDto.Type != null && textMediaElementDto.Media == null &&
                !textMediaElementDto.MediaId.HasValue)
            {
                return UpdateTextMediaElementStatus.MediaFileMissed;
            }

            if (textMediaElementDto.Media != null && !textMediaElementDto.MediaId.HasValue &&
                textMediaElementDto.Type == null)
            {
                return UpdateTextMediaElementStatus.MediaMediaType;
            }

            if (textMediaElementDto.Text == null && textMediaElementDto.Media == null && !textMediaElementDto.MediaId.HasValue)
            {
                return UpdateTextMediaElementStatus.TextOrMediaMissed;
            }

            if (textMediaElementDto.Tags != null &&
                textMediaElementDto.Tags.Count != textMediaElementDto.Tags.Distinct().Count())
            {
                return UpdateTextMediaElementStatus.DupplicateTags;
            }

            if (textMediaElementDto.Tags != null && textMediaElementDto.Tags.Any() && textMediaElementDto.Tags.Max(t => t.Length) > 30)
            {
                return UpdateTextMediaElementStatus.TagLengthMustBeLessThan;
            }

            return UpdateTextMediaElementStatus.Success;
        }
    }
}