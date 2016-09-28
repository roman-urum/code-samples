using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Maestro.Web.Areas.Customer.Models.CareBuilder.Medias;
using System.Linq;
using System.Net;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias;
using Maestro.Web.Exceptions;
using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Customer.Controllers
{
    /// <summary>
    /// CareBuilderController.
    /// </summary>
    public partial class CareBuilderController
    {
        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="mediaFile">The media file.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UploadFile(HttpPostedFileBase mediaFile)
        {
            var supportedMimeTypes = Common.Settings.SupportedExtensionsAndMimeTypes.Values.SelectMany(v => v);

            if (!supportedMimeTypes.Contains(mediaFile.ContentType))
            {                
                throw new LogicException(GlobalStrings.Media_UnsuportedMediaFiletype);
            }

            byte[] mediaBytes = new byte[mediaFile.ContentLength];

            mediaFile.InputStream.Read(mediaBytes, 0, mediaBytes.Length);

            await careBuilderManager.SaveFileToTempFolder(mediaBytes, mediaFile.FileName);

            var result = new MediaFileInfoViewModel()
            {
                FileName = mediaFile.FileName,
                ContentType = mediaFile.ContentType
            };

            return Json(result);
        }

        /// <summary>
        /// Downloads the file from URL.
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <returns></returns>
        public async Task<ActionResult> DownloadFileFromUrl(string fileUrl)
        {
            try
            {
                var downloadResult = await this.careBuilderManager.DownloadFileFromUrl(fileUrl);

                return Json(downloadResult);
            }
            catch (UriFormatException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new { ErrorMessage = "Invalid link to a file provided" });
            }
            catch (WebException)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;

                return Json(new { ErrorMessage = "Invalid link to a file provided" });
            }
            catch (LogicException ex)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;

                return Json(new
                {
                    ErrorMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DownloadFile(string fileName)
        {
            byte[] bytes = this.careBuilderManager.GetFileFromTempFolder(fileName);

            return new FileContentResult(bytes, "application/force-download");
        }

        [HttpGet]
        [ActionName("SearchMediaElements")]
        public async Task<ActionResult> SearchMediaElements(SearchMediaDto searchRequestDto)
        {
            var result = await careBuilderManager.FindMediaElements(searchRequestDto);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}