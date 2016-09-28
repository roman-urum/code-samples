namespace HealthLibrary.ContentStorage.Azure.Services.Interfaces
{
    /// <summary>
    /// IThumbnailService.
    /// </summary>
    public interface IThumbnailService
    {
        /// <summary>
        /// Generates the JPEG thumbnail from base64 string.
        /// </summary>
        /// <param name="sourceBase64String">The source base64 string.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        string GenerateJpegThumbnailFromBase64String(string sourceBase64String, string contentType);

        /// <summary>
        /// Generates the JPEG thumbnail from file.
        /// </summary>
        /// <param name="temporaryFilePath">The temporary file path.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        string GenerateJpegThumbnailFromFile(string temporaryFilePath, string contentType);
    }
}