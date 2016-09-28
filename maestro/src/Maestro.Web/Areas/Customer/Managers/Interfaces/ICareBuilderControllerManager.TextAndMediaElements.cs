using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias;
using Maestro.Web.Areas.Customer.Models.CareBuilder.Medias;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SearchCareElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.TextMediaElements;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    /// <summary>
    /// ICareBuilderControllerManager.TextAndMediaElements.
    /// </summary>
    public partial interface ICareBuilderControllerManager
    {
        /// <summary>
        /// Creates the text media element.
        /// </summary>
        /// <param name="createTextMediaElementModel">The create text media element model.</param>
        /// <returns></returns>
        Task CreateTextMediaElement(CreateTextMediaElementViewModel createTextMediaElementModel);

        /// <summary>
        /// Updates the text media element.
        /// </summary>
        /// <param name="updateTextMediaElementModel">The update text media element model.</param>
        /// <returns></returns>
        Task UpdateTextMediaElement(UpdateTextMediaElementViewModel updateTextMediaElementModel);

        /// <summary>
        /// Creates the media element.
        /// </summary>
        /// <param name="mediaDto">The media dto.</param>
        /// <returns></returns>
        Task<Guid> CreateMediaElement(CreateMediaRequestDto mediaDto);

        /// <summary>
        /// Saves the file to temporary folder.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        Task SaveFileToTempFolder(byte[] bytes, string fileName);

        /// <summary>
        /// Gets the file from temporary folder.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        byte[] GetFileFromTempFolder(string fileName);

        /// <summary>
        /// Downloads the file from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        Task<MediaFileInfoViewModel> DownloadFileFromUrl(string url);

        /// <summary>
        /// Gets the text media element.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<TextMediaResponseViewModel> GetTextMediaElement(Guid id);

        /// <summary>
        /// Returns list of text elements from API.
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<IEnumerable<TextMediaResponseViewModel>> FindTextMediaElements(SearchCareElementsViewModel searchModel);

        /// <summary>
        /// Finds the media elements.
        /// </summary>
        /// <param name="searchRequestDto">The search request dto.</param>
        /// <returns></returns>
        Task<IList<MediaResponseDto>> FindMediaElements(SearchMediaDto searchRequestDto);
    }
}