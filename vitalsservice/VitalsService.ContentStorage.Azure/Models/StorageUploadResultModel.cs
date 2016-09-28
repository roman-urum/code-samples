namespace VitalsService.ContentStorage.Azure.Models
{
    /// <summary>
    /// StorageUploadResultModel.
    /// </summary>
    public class StorageUploadResultModel
    {
        /// <summary>
        /// Gets or sets the original storage key.
        /// </summary>
        /// <value>
        /// The original storage key.
        /// </value>
        public string OriginalStorageKey { get; set; }

        /// <summary>
        /// Gets or sets the length of the original content.
        /// </summary>
        /// <value>
        /// The length of the original content.
        /// </value>
        public long OriginalContentLength { get; set; }

        /// <summary>
        /// Gets or sets the name of the original file.
        /// </summary>
        /// <value>
        /// The name of the original file.
        /// </value>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail storage key.
        /// </summary>
        /// <value>
        /// The thumbnail storage key.
        /// </value>
        public string ThumbnailStorageKey { get; set; }
    }
}