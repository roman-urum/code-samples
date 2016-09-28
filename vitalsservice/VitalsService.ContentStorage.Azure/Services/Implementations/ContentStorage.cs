using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using VitalsService.ContentStorage.Azure.Models;
using VitalsService.ContentStorage.Azure.Services.Interfaces;

namespace VitalsService.ContentStorage.Azure.Services.Implementations
{
    /// <summary>
    /// ContentStorage.
    /// </summary>
    public class ContentStorage : IContentStorage
    {
        private readonly ICustomerContext customerContext;
        private readonly CloudStorageAccount storageAccount;
        private readonly string containerName;
        private readonly CloudBlobClient blobClient;

        private readonly DateTime SharedPolicyUrlExpirationTime =
#if !DEBUG
            DateTime.UtcNow.AddMonths(1);
#else
            DateTime.UtcNow.AddMinutes(1);
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentStorage" /> class.
        /// </summary>
        /// <param name="customerContext">The care element context.</param>
        public ContentStorage(
            ICustomerContext customerContext
        )
        {
            this.customerContext = customerContext;
            this.storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("BlobStorageConnectionString"));
            this.containerName = this.customerContext.GetMediaContainerName();
            this.blobClient = this.storageAccount.CreateCloudBlobClient();
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
            if (!string.IsNullOrEmpty(content.Base64String))
            {
                throw new InvalidOperationException("Content and SourceContentUrl can't be used at the same time");
            }

            var result = new StorageUploadResultModel();
            result.OriginalFileName = content.Name;
            result.OriginalStorageKey =
                await UploadContent(content.Base64String, content.ContentType);
            result.OriginalContentLength = Convert.FromBase64String(content.Base64String).LongLength;

            return result;
        }
    }
}