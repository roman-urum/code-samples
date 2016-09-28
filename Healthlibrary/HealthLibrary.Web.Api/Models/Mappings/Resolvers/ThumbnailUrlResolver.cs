using System.IO;
using AutoMapper;
using HealthLibrary.ContentStorage.Azure.Services.Interfaces;
using HealthLibrary.Domain.Entities.Element;
using Microsoft.Practices.ServiceLocation;

namespace HealthLibrary.Web.Api.Models.Mappings.Resolvers
{
    /// <summary>
    /// ThumbnailUrlResolver.
    /// </summary>
    public class ThumbnailUrlResolver : ValueResolver<Media, string>
    {
        /// <summary>
        /// Resolves the core.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        protected override string ResolveCore(Media source)
        {
            if (source == null || string.IsNullOrEmpty(source.ThumbnailStorageKey))
            {
                return string.Empty;
            }

            var contentStorage = ServiceLocator.Current.GetInstance<IContentStorage>();

            return contentStorage.GenerateContentSASUrl(
                source.ThumbnailStorageKey,
                string.Concat(
                    Path.GetFileNameWithoutExtension(source.OriginalFileName),
                    "-thumbnail.jpg"
                )
            );
        }
    }
}