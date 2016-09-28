using System;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DomainLogic.Extensions
{
    /// <summary>
    /// Extensions methods for selection answer choice strings.
    /// </summary>
    public static class LocalizedStringExtensions
    {
        /// <summary>
        /// Updates fields of answer choice in destination with values from source.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        public static void UpdateWith(this LocalizedString destination, LocalizedString source)
        {
            destination.Language = source.Language;
            destination.Value = source.Value;
            destination.Description = source.Description;
            destination.Pronunciation = source.Pronunciation;

            if (source.AudioFileMediaId.HasValue)
            {
                destination.AudioFileMediaId = source.AudioFileMediaId;

                return;
            }

            if (source.AudioFileMedia == null)
            {
                destination.AudioFileMediaId = null;
                destination.AudioFileMedia = null;

                return;
            }

            if (destination.AudioFileMedia != null && destination.AudioFileMedia.Id == source.AudioFileMedia.Id)
            {
                destination.AudioFileMedia.UpdateWith(source.AudioFileMedia);
            }
            else
            {
                destination.AudioFileMedia = source.AudioFileMedia;
            }
        }
    }
}
