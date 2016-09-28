using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Hosting;
using CustomerService.Common;
using CustomerService.Web.Api.Helpers.Interfaces;
using CustomerService.Web.Api.Models.Dtos.FileUpload;

namespace CustomerService.Web.Api.Helpers.Implemetations
{
    /// <summary>
    /// FileUploadHelper.
    /// </summary>
    public class FileUploadControllerHelper : IFileUploadControllerHelper
    {
        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="model">The HTTP posted file.</param>
        /// <returns></returns>
        public UploadFileResponseDto SaveFile(UploadFileDto model)
        {
            string relativePath = null;
            if (model != null && !string.IsNullOrEmpty(model.FileName))
            {
                // TODO: Validate file.
                var fileName = model.FileName;
                var indexOfExtension = fileName.LastIndexOf('.');
                var fileExtension = fileName.Substring(indexOfExtension);

                var newFileName = string.Format("{0}{1}", Guid.NewGuid(), fileExtension);

                string filePath = Path.Combine(
                    HostingEnvironment.MapPath(Settings.UploadImagesFolder),
                    newFileName);

                var stream = new MemoryStream(Convert.FromBase64String(model.FileData));

                var image = Image.FromStream(stream);

                image.Save(filePath);

                relativePath = VirtualPathUtility.ToAbsolute(string.Format("{0}/{1}", 
                    Settings.UploadImagesFolder, newFileName));
            }

            return new UploadFileResponseDto()
            {
                FilePath = string.Format("{0}{1}", Settings.ApiBaseUrl, relativePath),
            };
        }
    }
}