namespace CustomerService.Web.Api.Models.Dtos.FileUpload
{
    /// <summary>
    /// UploadFileDto.
    /// </summary>
    public class UploadFileDto
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }
        /// <summary>
        /// Gets or sets the file data.
        /// </summary>
        /// <value>
        /// The file data.
        /// </value>
        public string FileData { get; set; }
    }
}