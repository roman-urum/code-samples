namespace HealthLibrary.ContentStorage.Azure.Models
{
    /// <summary>
    /// ContentModel.
    /// </summary>
    public class ContentModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the base64 string.
        /// </summary>
        /// <value>
        /// The base64 string.
        /// </value>
        public string Base64String { get; set; }

        /// <summary>
        /// Gets or sets the source content URL.
        /// </summary>
        /// <value>
        /// The source content URL.
        /// </value>
        public string SourceContentUrl { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }
    }
}