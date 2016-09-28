using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HealthLibrary.Common;
using HealthLibrary.ContentStorage.Azure.Exceptions;
using HealthLibrary.ContentStorage.Azure.Models;
using HealthLibrary.ContentStorage.Azure.Services.Interfaces;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HealthLibrary.ContentStorage.Azure.Services.Implementations
{
    /// <summary>
    /// ContentStorage.
    /// </summary>
    public class ContentStorage : IContentStorage
    {
        private readonly ICareElementContext careElementContext;
        private readonly CloudStorageAccount storageAccount;
        private readonly string containerName;
        private readonly CloudBlobClient blobClient;
        private readonly IThumbnailService thumbnailService;

        private readonly DateTime SharedPolicyUrlExpirationTime =
#if !DEBUG
            DateTime.UtcNow.AddMonths(1);
#else
            DateTime.UtcNow.AddMinutes(1);
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentStorage" /> class.
        /// </summary>
        /// <param name="careElementContext">The care element context.</param>
        /// <param name="thumbnailService">The thumbnail service.</param>
        public ContentStorage(
            ICareElementContext careElementContext,
            IThumbnailService thumbnailService
        )
        {
            this.careElementContext = careElementContext;
            this.storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("BlobStorageConnectionString"));
            this.containerName = this.careElementContext.GetMediaContainerName();
            this.blobClient = this.storageAccount.CreateCloudBlobClient();
            this.thumbnailService = thumbnailService;
        }

        /// <summary>
        /// Uploads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        public async Task<string> UploadContent(byte[] content, string contentType)
        {
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
            await blobContainer.CreateIfNotExistsAsync();

            var storageKey = Guid.NewGuid().ToString();

            var blockBlob = blobContainer.GetBlockBlobReference(storageKey);

            blockBlob.Properties.ContentType = contentType;

            await blockBlob.UploadFromByteArrayAsync(content, 0, content.Length);

            return storageKey;
        }

        /// <summary>
        /// Uploads the content.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        public async Task<string> UploadContent(Stream stream, string contentType)
        {
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
            await blobContainer.CreateIfNotExistsAsync();

            var storageKey = Guid.NewGuid().ToString();

            var blockBlob = blobContainer.GetBlockBlobReference(storageKey);

            blockBlob.Properties.ContentType = contentType;

            await blockBlob.UploadFromStreamAsync(stream);

            return storageKey;
        }

        /// <summary>
        /// Uploads the content from base64 string.
        /// </summary>
        /// <param name="base64Content">Content of the base64.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        public async Task<string> UploadContent(string base64Content, string contentType)
        {
            var bytes = Convert.FromBase64String(base64Content);

            return await this.UploadContent(bytes, contentType);
        }

        /// <summary>
        /// Deletes the content.
        /// </summary>
        /// <param name="storageKey">The storage key.</param>
        /// <returns></returns>
        public async Task<bool> DeleteContent(string storageKey)
        {
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
            if (!await blobContainer.ExistsAsync())
            {
                return false;
            }

            var blockBlob = blobContainer.GetBlockBlobReference(storageKey);

            return await blockBlob.DeleteIfExistsAsync();
        }

        /// <summary>
        /// Generates the temporary content URL.
        /// </summary>
        /// <param name="storageKey">The storage key.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public string GenerateContentSASUrl(
            string storageKey,
            string fileName
        )
        {
            if (string.IsNullOrEmpty(storageKey))
            {
                return string.Empty;
            }

            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            var blob = container.GetBlockBlobReference(storageKey);

            string sasBlobToken = blob.GetSharedAccessSignature(
                new SharedAccessBlobPolicy()
                {
                    SharedAccessExpiryTime = SharedPolicyUrlExpirationTime,
                    Permissions = SharedAccessBlobPermissions.Read
                },
                new SharedAccessBlobHeaders()
                {
                    ContentDisposition = string.Format("attachment; filename={0}", fileName)
                }
            );

            return string.Format("{0}{1}", blob.Uri, sasBlobToken);
        }

        /// <summary>
        /// Uploads the content to storage.
        /// </summary>
        /// <param name="content">The file.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Content and SourceContentUrl cann't be used at the same time</exception>
        public async Task<StorageUploadResultModel> UploadContent(ContentModel content)
        {
            if (
                !string.IsNullOrEmpty(content.Base64String) &&
                !string.IsNullOrEmpty(content.SourceContentUrl)
            )
            {
                throw new InvalidOperationException("Content and SourceContentUrl can't be used at the same time");
            }

            var result = new StorageUploadResultModel();

            if (!string.IsNullOrEmpty(content.Base64String))
            {
                result.OriginalFileName = content.Name;

                var thumbnailBase64String = thumbnailService
                    .GenerateJpegThumbnailFromBase64String(content.Base64String, content.ContentType);

                if (!string.IsNullOrEmpty(thumbnailBase64String))
                {
                    result.ThumbnailStorageKey =
                        await UploadContent(thumbnailBase64String, "image/jpeg");
                }

                result.OriginalStorageKey =
                    await UploadContent(content.Base64String, content.ContentType);

                result.OriginalContentLength = Convert.FromBase64String(content.Base64String).LongLength;

                return result;
            }

            if (!string.IsNullOrEmpty(content.SourceContentUrl))
            {
                var downloadResult = await DownloadExternalContent(content.SourceContentUrl);

                if (downloadResult != null)
                {
                    result.OriginalFileName = downloadResult.Name;

                    var thumbnailBase64String = thumbnailService
                        .GenerateJpegThumbnailFromFile(downloadResult.TemporaryFilePath, downloadResult.ContentType);

                    if (!string.IsNullOrEmpty(thumbnailBase64String))
                    {
                        result.ThumbnailStorageKey =
                            await UploadContent(thumbnailBase64String, "image/jpeg");
                    }

                    using (var stream = new FileStream(downloadResult.TemporaryFilePath, FileMode.Open))
                    {
                        result.OriginalStorageKey =
                            await UploadContent(stream, downloadResult.ContentType);

                        result.OriginalContentLength = stream.Length;
                    }

                    File.Delete(downloadResult.TemporaryFilePath);

                    return result;
                }
            }

            return null;
        }

        private async Task<DownloadResultModel> DownloadExternalContent(string url)
        {
            var uri = new Uri(url);

#if DEBUG
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
#endif

            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri);

                    response.EnsureSuccessStatusCode();

                    var contentType = response.Content.Headers.ContentType;

                    if (Settings.SupportedExtensionsAndMimeTypes.Any(e => e.Value.Contains(contentType.MediaType)))
                    {
                        var temporaryFileFullPath = Path.GetTempFileName();

                        File.WriteAllBytes(temporaryFileFullPath, await response.Content.ReadAsByteArrayAsync());

                        return new DownloadResultModel()
                        {
                            Name = Path.GetFileName(uri.LocalPath),
                            ContentType = contentType.MediaType,
                            TemporaryFilePath = temporaryFileFullPath
                        };
                    }

                    return null;
                }
                catch (HttpRequestException ex)
                {
                    throw new InvalidExternalUrlException($"Couldn't download content using provided URL: {url}.", ex);
                }
            }
        }
    }
}