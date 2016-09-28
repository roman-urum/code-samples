using System.Linq;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DomainLogic.Extensions
{
    /// <summary>
    /// Extension methods for question elements.
    /// </summary>
    public static class QuestionElementExtensions
    {
        /// <summary>
        /// Includes new or updates existed localized string.
        /// </summary>
        /// <param name="?"></param>
        public static void AddOrUpdateString(this QuestionElement dbEntity, QuestionElementString localizedString)
        {
            var existedString =
                dbEntity.LocalizedStrings.FirstOrDefault(s => s.Language.Equals(localizedString.Language));

            if (existedString == null)
            {
                dbEntity.LocalizedStrings.Add(localizedString);
            }
            else
            {
                existedString.UpdateWith(localizedString);
            }
        }
    }
}
