using System;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DomainLogic.Extensions
{
    /// <summary>
    /// Extension methods for Media instances.
    /// </summary>
    public static class MediaExtensions
    {
        /// <summary>
        /// Updates destination entity with info provided in source entity.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        public static void UpdateWith(this Media destination, Media source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            destination.Description = source.Description;
            destination.Name = source.Name;
            destination.ContentLength = source.ContentLength;
            destination.ContentType = source.ContentType;

            if (!string.IsNullOrEmpty(source.OriginalStorageKey))
            {
                destination.OriginalStorageKey = source.OriginalStorageKey;
            }
        }
    }
}
