using System.Collections.Generic;
using System.Linq;
using HealthLibrary.Domain.Entities;

namespace HealthLibrary.DomainLogic.Extensions
{
    /// <summary>
    /// Extension methods for tag entities.
    /// </summary>
    internal static class TagExtensions
    {
        /// <summary>
        /// Modifies list of tag entities with new provided list.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        public static void UpdateWith(this ICollection<Tag> destination, IEnumerable<Tag> source)
        {
            if (source == null || !source.Any())
            {
                destination.Clear();

                return;
            }

            var newTags = source.Where(tag => !destination.Contains(tag)).ToList();
            var tagsToDelete = destination.Where(tag => !source.Contains(tag)).ToList();

            foreach (var tag in newTags)
            {
                destination.Add(tag);
            }

            foreach (var tag in tagsToDelete)
            {
                destination.Remove(tag);
            }
        }
    }
}
