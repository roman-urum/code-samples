using CustomerService.Web.Api.Models.Dtos.FileUpload;

namespace CustomerService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IFileUploadHelper.
    /// </summary>
    public interface IFileUploadControllerHelper
    {
        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="model">The HTTP posted file.</param>
        /// <returns></returns>
        UploadFileResponseDto SaveFile(UploadFileDto model);
    }
}
