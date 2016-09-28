using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Domain.Dtos.HealthLibraryService.Elements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.TextMediaElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings;
using Maestro.Web.Areas.Customer.Models.CareBuilder.Medias;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SearchCareElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.TextMediaElements;
using Maestro.Web.Exceptions;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    /// <summary>
    /// CareBuilderControllerManager.TextAndMediaElements.
    /// </summary>
    public partial class CareBuilderControllerManager
    {
        /// <summary>
        /// Creates and send request to API to create new text and media element using 
        /// </summary>
        /// <param name="createTextMediaElementModel"></param>
        /// <returns></returns>
        public async Task CreateTextMediaElement(CreateTextMediaElementViewModel createTextMediaElementModel)
        {
            var token = this.authDataStorage.GetToken();
            var createTextMediaElementDto = Mapper.Map<CreateTextMediaElementRequestDto>(createTextMediaElementModel);

            createTextMediaElementDto.Text = this.InitCreateLocalizedStringRequest(createTextMediaElementModel.Text);
            createTextMediaElementDto.MediaId = await this.CreateMedia(createTextMediaElementModel.Media);

            await this.healthLibraryService.CreateTextAndMediaElement(createTextMediaElementDto, this.customerContext.Customer.Id, token);
        }
        
        /// <summary>
        /// Updates the text media element.
        /// </summary>
        /// <param name="updateTextMediaElementModel">The update text media element model.</param>
        /// <returns></returns>
        public async Task UpdateTextMediaElement(UpdateTextMediaElementViewModel updateTextMediaElementModel)
        {
            Guid? mediaId = null;

            if (updateTextMediaElementModel.Media != null)
            {
                if (updateTextMediaElementModel.Media.Id == Guid.Empty)
                {
                    mediaId = await this.CreateMedia(updateTextMediaElementModel.Media);
                }
                else
                {
                    mediaId = await this.UpdateMedia(updateTextMediaElementModel.Media);
                }
            }

            var updateTextMediaElementDto = Mapper.Map<UpdateTextMediaElementRequestDto>(updateTextMediaElementModel);
            updateTextMediaElementDto.MediaId = mediaId;
            updateTextMediaElementDto.Text = this.InitUpdateLocalizedStringRequest(updateTextMediaElementModel.Text);

            var token = authDataStorage.GetToken();
            await healthLibraryService.UpdateTextMediaElement(
                updateTextMediaElementDto,
                customerContext.Customer.Id,
                token
            );
        }

        /// <summary>
        /// Creates the media element.
        /// </summary>
        /// <param name="mediaDto">The media dto.</param>
        /// <returns></returns>
        public async Task<Guid> CreateMediaElement(CreateMediaRequestDto mediaDto)
        {
            var token = this.authDataStorage.GetToken();
            var mediaId = await this.healthLibraryService.CreateMediaElement(mediaDto, this.customerContext.Customer.Id, token);

            return mediaId;
        }

        /// <summary>
        /// Saves the file to temporary folder.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public async Task SaveFileToTempFolder(byte[] bytes, string fileName)
        {
            string tempLocation = string.Format(@"{0}{1}", Path.GetTempPath(), Common.Settings.CITempFolderName);

            if (!Directory.Exists(tempLocation))
            {
                Directory.CreateDirectory(tempLocation);
            }

            this.ClearOldTempFiles(tempLocation);

            string filePath = string.Format(@"{0}\{1}", tempLocation, fileName);

            using (var fileStream = File.Create(filePath))
            {
                await fileStream.WriteAsync(bytes, 0, bytes.Length);
            }
        }

        /// <summary>
        /// Gets the file from temporary folder.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public byte[] GetFileFromTempFolder(string fileName)
        {
            string tempFolder = Path.Combine(Path.GetTempPath(), Common.Settings.CITempFolderName);
            var tempFilename = Path.GetFileName(fileName);

            string filePath = Path.Combine(tempFolder, tempFilename);

            byte[] bytes = File.ReadAllBytes(filePath);

            return bytes;
        }

        /// <summary>
        /// Downloads the file from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Invalid url</exception>
        public async Task<MediaFileInfoViewModel> DownloadFileFromUrl(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                var uri = new Uri(url);
                var bytes = webClient.DownloadData(uri);

                var supportedMimeTypes = Common.Settings.SupportedMediaExtensionsAndMimeTypes.Values.SelectMany(v => v);

                if (supportedMimeTypes.Contains(webClient.ResponseHeaders["content-type"]))
                {
                    string tempFileName = Path.GetFileName(uri.LocalPath);
                    await this.SaveFileToTempFolder(bytes, tempFileName);

                    return new MediaFileInfoViewModel()
                    {
                        ContentType = webClient.ResponseHeaders["content-type"],
                        FileName = tempFileName
                    };
                }
                
                throw new LogicException("Please download file in correct format: mp4, m4a, webm, jpg, jpeg, png, pdf");
            }
        }

        /// <summary>
        /// Returns view model for text and media element with specified identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TextMediaResponseViewModel> GetTextMediaElement(Guid id)
        {
            var token = authDataStorage.GetToken();
            var textMediaElementDto = await healthLibraryService.GetTextMediaElement(token, CustomerContext.Current.Customer.Id, id);
            var textMediaElementModel = Mapper.Map<TextMediaResponseViewModel>(textMediaElementDto);

            return textMediaElementModel;
        }

        /// <summary>
        /// Returns list of text elements from API.
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TextMediaResponseViewModel>> FindTextMediaElements(SearchCareElementsViewModel searchModel)
        {
            var searchRequest = new SearchRequestDto
            {
                Q = searchModel.Keyword,
                CustomerId = CustomerContext.Current.Customer.Id
            };

            var token = authDataStorage.GetToken();
            var result = await healthLibraryService.FindTextMediaElements(searchRequest, token);

            return result.Select(textMediaElementDto => {
                var textMediaElementModel = Mapper.Map<TextMediaResponseViewModel>(textMediaElementDto);

                if (textMediaElementDto.Media == null)
                {
                    textMediaElementModel.Media = null;
                }
                else
                {
                    //var mediaElementDto = this.healthLibraryService.GetMediaElement(CustomerContext.Current.Customer.Id, textMediaElementDto.Media.MediaId, token);

                    //textMediaElementModel.Media = new MediaElementModel() { Name = "dummy name", ContentType = "dummy content type", FileName = "dummy file name" };
                    textMediaElementModel.Media = null;
                }

                /*
                textMediaElementDto.Media.IfNotNull(
                    dto =>
                        {
                            var mediaElementDto = this.healthLibraryService.GetMediaElement(CustomerContext.Current.Customer.Id, dto.MediaId, token);
                            textMediaElementModel.Media = Mapper.Map<MediaElementModel>(mediaElementDto);
                        });
                */
                return textMediaElementModel;
            }).ToList();
        }

        /// <summary>
        /// Finds the media elements.
        /// </summary>
        /// <param name="searchRequestDto">The search request dto.</param>
        /// <returns></returns>
        public async Task<IList<MediaResponseDto>> FindMediaElements(SearchMediaDto searchRequestDto)
        {
            var token = authDataStorage.GetToken();

            var searchMediaResultsDto = await healthLibraryService.FindMediaElements(searchRequestDto, CustomerContext.Current.Customer.Id, token);

            var result = searchMediaResultsDto.Select(Mapper.Map<MediaResponseDto>).ToList();

            return result;
        }

        #region Private methods

        /// <summary>
        /// Creates request dto for new localized string using model data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private CreateLocalizedStringRequestDto InitCreateLocalizedStringRequest(
            CreateLocalizedStringViewModel model
        )
        {
            if (string.IsNullOrEmpty(model.Value))
            {
                return null;
            }

            var result = Mapper.Map<CreateLocalizedStringRequestDto>(model);

            if (model.AudioFileMedia != null)
            {
                var file = this.GetFileFromTempFolder(model.AudioFileMedia.FileName);

                result.AudioFileMedia = new CreateMediaRequestDto
                {
                    Content = Convert.ToBase64String(file),
                    ContentType = model.AudioFileMedia.ContentType,
                    OriginalFileName = model.AudioFileMedia.FileName,
                    Name = model.Value
                };
            }

            return result;
        }

        /// <summary>
        /// Creates request dto for modified localized string using model data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private UpdateLocalizedStringRequestDto InitUpdateLocalizedStringRequest(
            CreateLocalizedStringViewModel model
        )
        {
            if (model.AudioFileMedia == null && model.Value == null && model.Pronunciation == null) return null;

            var result = Mapper.Map<UpdateLocalizedStringRequestDto>(model);

            if (model.AudioFileMedia != null)
            {
                if (model.AudioFileMedia.Id.HasValue)
                {
                    result.AudioFileMediaId = model.AudioFileMedia.Id;
                }
                else
                {
                    var file = this.GetFileFromTempFolder(model.AudioFileMedia.FileName);

                    result.AudioFileMedia = new UpdateMediaRequestDto
                    {
                        Content = Convert.ToBase64String(file),
                        ContentType = model.AudioFileMedia.ContentType,
                        OriginalFileName = model.AudioFileMedia.FileName,
                        Name = model.AudioFileMedia.FileName
                    };
                }
            }

            return result;
        }

        private async Task<Guid?> CreateMedia(MediaViewModel mediaElementModel)
        {
            if (mediaElementModel == null)
            {
                return null;
            }

            if (mediaElementModel.Id.HasValue)
            {
                return mediaElementModel.Id.Value;
            }

            if (string.IsNullOrEmpty(mediaElementModel.FileName))
            {
                return null;
            }

            var fileContent = this.GetFileFromTempFolder(mediaElementModel.FileName);

            if (fileContent.Length <= 0)
            {
                return null;
            }

            var fileContentBase64 = Convert.ToBase64String(fileContent);

            var createMediaDto = new CreateMediaRequestDto()
            {
                Name = mediaElementModel.Name,
                OriginalFileName = mediaElementModel.FileName,
                Content = fileContentBase64,
                ContentType = mediaElementModel.ContentType,
                Tags = mediaElementModel.Tags
            };

            return await this.CreateMediaElement(createMediaDto);
        }

        private async Task<Guid?> UpdateMedia(UpdateMediaModel mediaElementModel)
        {
            if (mediaElementModel == null)
            {
                return null;
            }

            var updateMediaDto = new UpdateMediaRequestDto()
            {
                Id = mediaElementModel.Id,
                Name = mediaElementModel.Name,
                ContentType = mediaElementModel.ContentType,
                Tags = mediaElementModel.Tags,
                OriginalFileName = mediaElementModel.OriginalFileName
            };

            if (!string.IsNullOrEmpty(mediaElementModel.FileName))
            {
                var fileContent = this.GetFileFromTempFolder(mediaElementModel.FileName);
                if (fileContent != null && fileContent.Length > 0)
                {
                    var fileContentBase64 = Convert.ToBase64String(fileContent);
                    updateMediaDto.Content = fileContentBase64;
                    updateMediaDto.OriginalFileName = mediaElementModel.FileName;
                }
            }

            var token = this.authDataStorage.GetToken();
            await this.healthLibraryService.UpdateMediaElement(updateMediaDto, this.customerContext.Customer.Id, token);

            return mediaElementModel.Id;
        }

        private void ClearOldTempFiles(string tempLocation)
        {
            var maxAgeHours = Convert.ToInt32(ConfigurationManager.AppSettings["TempFiles.MaxAgeHours"] ?? "48");

            var dir = new DirectoryInfo(tempLocation);

            foreach (var file in dir.GetFiles())
            {
                var age = DateTime.UtcNow - file.CreationTimeUtc;

                if (age.TotalHours > maxAgeHours)
                {
                    try
                    {
                        file.Delete();
                        // Warning because this shouldn't happen
                        this.logger.Warn("Deleted old ({0} hours) temp file: {1}", age.TotalHours, file.FullName);
                    }
                    catch (Exception ex)
                    {
                        // Permissions? File locked?
                        this.logger.Error("Error deleting old ({0} hours) temp file ({1}): {2}", age.TotalHours, file.FullName, ex.Message);
                    }
                }
            }
        }

        #endregion
    }
}