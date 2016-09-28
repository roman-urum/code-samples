using System.Linq;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DomainLogic.Extensions
{
    /// <summary>
    /// Extension methods for SelecitonAnswerSetEntity.
    /// </summary>
    public static class SelectionAnswerChoiceExtensions
    {
        /// <summary>
        /// Updates answer choice with new data.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        public static void UpdateWith(this SelectionAnswerChoice destination, SelectionAnswerChoice source)
        {
            destination.Description = source.Description;
            destination.IsOpenEnded = source.IsOpenEnded;
            destination.Sort = source.Sort;

            var localizedString = source.LocalizedStrings.FirstOrDefault();

            if (localizedString != null)
            {
                destination.AddOrUpdateLocalizedString(localizedString);
            }
        }

        /// <summary>
        /// Updates fields of answer choice in destination with values from source.
        /// </summary>
        /// <param name="answerChoice"></param>
        /// <param name="localizedString"></param>
        public static void AddOrUpdateLocalizedString(this SelectionAnswerChoice answerChoice, SelectionAnswerChoiceString localizedString)
        {
            var localizedStringEntity =
                    answerChoice.LocalizedStrings.FirstOrDefault(
                        s => s.Language.Equals(localizedString.Language));

            if (localizedStringEntity == null)
            {
                answerChoice.LocalizedStrings.Add(localizedString);
            }
            else
            {
                localizedStringEntity.UpdateWith(localizedString);
            }
        }
    }
}
