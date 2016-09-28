using Maestro.Common.ApiClient;

namespace Maestro.Domain
{
    /// <summary>
    /// Model to transfer files to external service.
    /// </summary>
    public class FileDto
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        [RequestParameter(RequestParameterType.RequestBody)]
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        [RequestParameter(RequestParameterType.RequestBody)]
        public string ContentType { get; set; }
        /// <summary>
        /// Gets or sets the file data.
        /// </summary>
        /// <value>
        /// The file data.
        /// </value>
        [RequestParameter(RequestParameterType.RequestBody)]
        public string FileData { get; set; }

    }
}
