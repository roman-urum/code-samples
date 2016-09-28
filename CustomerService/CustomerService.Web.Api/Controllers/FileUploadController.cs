using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CustomerService.Common.Extensions;
using CustomerService.Web.Api.Helpers.Interfaces;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.Enums;
using CustomerService.Web.Api.Models.Dtos.FileUpload;

namespace CustomerService.Web.Api.Controllers
{
    /// <summary>
    /// FileUploadController.
    /// </summary>
    public class FileUploadController : ApiController
    {
        private IFileUploadControllerHelper fileUploaderHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileUploadController"/> class.
        /// </summary>
        /// <param name="fileUploaderHelper">The file uploader helper.</param>
        public FileUploadController(IFileUploadControllerHelper fileUploaderHelper)
        {
            this.fileUploaderHelper = fileUploaderHelper;
        }
        
        /// <summary>
        /// Uploads the file.
        /// </summary>
        [HttpPost]
        [ResponseType(typeof(UploadFileResponseDto))]
        public IHttpActionResult UploadFile(UploadFileDto file)
        {
            if (file != null && !string.IsNullOrEmpty(file.FileData))
            {
                var fileResponse = fileUploaderHelper.SaveFile(file);

                if (fileResponse != null && !string.IsNullOrEmpty(fileResponse.FilePath))
                {
                    return Ok(fileResponse);
                }
            }

            return Content(
                HttpStatusCode.BadRequest,
                new ErrorResponseDto()
                {
                    Error = ErrorCode.InvalidRequest,
                    Message = ErrorCode.InvalidRequest.Description(),
                    Details = ErrorCode.IncorrectFile.Description()
                }
            );
        }
    }
}