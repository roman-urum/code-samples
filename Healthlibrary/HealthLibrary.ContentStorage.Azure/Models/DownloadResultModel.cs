namespace HealthLibrary.ContentStorage.Azure.Models
{
    /// <summary>
    /// DownloadResultModel.
    /// </summary>
    public class DownloadResultModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the temporary file path.
        /// </summary>
        /// <value>
        /// The temporary file path.
        /// </value>
        public string TemporaryFilePath { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }
    }
}