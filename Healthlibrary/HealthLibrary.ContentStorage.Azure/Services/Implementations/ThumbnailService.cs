using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using HealthLibrary.ContentStorage.Azure.Exceptions;
using HealthLibrary.ContentStorage.Azure.Services.Interfaces;
using NReco.VideoConverter;

namespace HealthLibrary.ContentStorage.Azure.Services.Implementations
{
    /// <summary>
    /// ThumbnailService.
    /// </summary>
    public class ThumbnailService : IThumbnailService
    {
        private const int ThumbnailImageWidth = 400;
        private const int ThumbnailImageHeight = 300;

        /// <summary>
        /// Generates the JPEG thumbnail from base64.
        /// </summary>
        /// <param name="sourceBase64String">The source base64.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        public string GenerateJpegThumbnailFromBase64String(string sourceBase64String, string contentType)
        {
            switch (contentType)
            {
                case "video/mp4":
                {
                    var temporaryFileName = Path.GetTempFileName();

                    File.WriteAllBytes(temporaryFileName, Convert.FromBase64String(sourceBase64String));
                        
                    var result = this.GetVideoThumbnail(temporaryFileName);

                    File.Delete(temporaryFileName);

                    return result;
                }

                case "image/jpeg":
                case "image/png":
                {
                    using (var outputStream = new MemoryStream(Convert.FromBase64String(sourceBase64String)))
                    {
                        ResizeImage(outputStream, outputStream, ThumbnailImageWidth, ThumbnailImageHeight);

                        return Convert.ToBase64String(outputStream.ToArray());
                    }
                }

                default:
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Generates the JPEG thumbnail from file.
        /// </summary>
        /// <param name="temporaryFilePath">The temporary file path.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        public string GenerateJpegThumbnailFromFile(string temporaryFilePath, string contentType)
        {
            switch (contentType)
            {
                case "video/mp4":
                {
                    return GetVideoThumbnail(temporaryFilePath);
                }

                case "image/jpeg":
                case "image/png":
                {
                    using (var inputStream = new FileStream(temporaryFilePath, FileMode.Open))
                    {
                        using (var outputStream = new MemoryStream())
                        {
                            ResizeImage(inputStream, outputStream, ThumbnailImageWidth, ThumbnailImageHeight);

                            return Convert.ToBase64String(outputStream.ToArray());
                        }
                    }
                }

                default:
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Generates thumbnail for video file.
        /// </summary>
        /// <param name="videoFilePath"></param>
        /// <returns>Base64 string with thumbnail.</returns>
        private string GetVideoThumbnail(string videoFilePath)
        {
            using (var outputStream = new MemoryStream())
            {
                var ffMpeg = new FFMpegConverter();

                try
                {
                    ffMpeg.GetVideoThumbnail(videoFilePath, outputStream);
                }
                catch (FFMpegException ex)
                {
                    throw new InvalidFileContentException("File is not a video.", ex);
                }
                
                ResizeImage(outputStream, outputStream, ThumbnailImageWidth, ThumbnailImageHeight);

                return Convert.ToBase64String(outputStream.ToArray());
            }
        }

        private void ResizeImage(Stream sourceStream, Stream outputStream, int fitWidth, int fitHeight)
        {
            Image imgToResize;

            try
            {
                imgToResize = Image.FromStream(sourceStream);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidFileContentException("File is not an image.", ex);
            }

            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            if (sourceWidth > sourceHeight)
            {
                float scale = ((float)fitWidth / (float)sourceWidth);
                fitHeight = (int)(sourceHeight * scale);
            }
            else
            {
                float scale = ((float)fitHeight / (float)sourceHeight);
                fitWidth = (int)(sourceWidth * scale);
            }

            using (var newImg = new Bitmap(fitWidth, fitHeight))
            {
                using (var graphics = Graphics.FromImage(newImg))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.DrawImage(imgToResize, 0, 0, fitWidth, fitHeight);
                }

                outputStream.Seek(0, SeekOrigin.Begin);
                newImg.Save(outputStream, ImageFormat.Jpeg);
            }
        }
    }
}