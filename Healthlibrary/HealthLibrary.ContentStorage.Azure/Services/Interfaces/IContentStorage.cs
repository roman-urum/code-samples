using System.IO;
using System.Threading.Tasks;
using HealthLibrary.ContentStorage.Azure.Models;

namespace HealthLibrary.ContentStorage.Azure.Services.Interfaces
{
    /// <summary>
    /// IContentStorage.
    /// </summary>
    public interface IContentStorage
    {
        /// <summary>
        /// Uploads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        Task<string> UploadContent(byte[] content, string contentType);

        /// <summary>
        /// Uploads the content.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        Task<string> UploadContent(Stream stream, string contentType);

        /// <summary>
        /// Uploads the content from base64 string.
        /// </summary>
        /// <param name="base64Content">Content of the base64.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        Task<string> UploadContent(string base64Content, string contentType);

        /// <summary>
        /// Deletes the content.
        /// </summary>
        /// <param name="storageKey">The storage key.</param>
        /// <returns></returns>
        Task<bool> DeleteContent(string storageKey);

        /// <summary>
        /// Generates the temporary content URL.
        /// </summary>
        /// <param name="storageKey">The storage key.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        string GenerateContentSASUrl(string storageKey, string fileName);

        /// <summary>
        /// Uploads the content to storage.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        Task<StorageUploadResultModel> UploadContent(ContentModel content);
    }
}