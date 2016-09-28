using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;

namespace Maestro.Web.Helpers
{
    /// <summary>
    /// Provides help methods to work with media types.
    /// </summary>
    public static class MediaTypeHelper
    {
        private static readonly IDictionary<string, MediaType> MediaTypesMap = new Dictionary<string, MediaType>
        {
            {"image/jpeg", MediaType.Image},
            {"image/png", MediaType.Image},
            {"image/gif", MediaType.Image},
            {"video/mp4", MediaType.Video},
            {"audio/mpeg", MediaType.Audio},
            {"audio/mp4", MediaType.Audio},
            {"audio/mp3", MediaType.Audio},
            {"application/pdf", MediaType.Document}
        };
        

        /// <summary>
        /// Returns type of media element based on content type string.
        /// </summary>
        /// <returns></returns>
        public static MediaType? GetByContentType(string contentType)
        {
            MediaType result;

            if (MediaTypesMap.TryGetValue(contentType, out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Generates the thumbnail URL.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        /// <param name="thumbnailUrl">The thumbnail URL.</param>
        /// <returns></returns>
        public static string GenerateThumbnailUrl(MediaType? mediaType, string thumbnailUrl)
        {
            if (!mediaType.HasValue)
            {
                return thumbnailUrl;
            }

            switch (mediaType)
            {
                case MediaType.Audio:
                    return UrlHelper.GenerateContentUrl("~/Content/img/audio-preview.gif",
                        new HttpContextWrapper(HttpContext.Current));

                case MediaType.Document:
                    return UrlHelper.GenerateContentUrl("~/Content/img/doc-preview.png",
                        new HttpContextWrapper(HttpContext.Current));

                default:
                    return thumbnailUrl;
            }
        }
    }
}