using AutoMapper;
using HealthLibrary.ContentStorage.Azure.Services.Interfaces;
using HealthLibrary.Domain.Entities.Element;
using Microsoft.Practices.ServiceLocation;

namespace HealthLibrary.Web.Api.Models.Mappings.Resolvers
{
    /// <summary>
    /// MediaUrlResolver.
    /// </summary>
    public class MediaUrlResolver : ValueResolver<Media, string>
    {
        /// <summary>
        /// Resolves the core.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        protected override string ResolveCore(Media source)
        {
            if (source == null || string.IsNullOrEmpty(source.OriginalStorageKey))
            {
                return string.Empty;
            }

            var contentStorage = ServiceLocator.Current.GetInstance<IContentStorage>();

            return contentStorage.GenerateContentSASUrl(source.OriginalStorageKey, source.OriginalFileName);
        }
    }
}