using System;
using System.Collections.Generic;
using System.Linq;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Domain.Entities;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.DomainLogic.Services.Results;

namespace HealthLibrary.DomainLogic.Extensions
{
    /// <summary>
    /// Extension methods for SelecitonAnswerSetEntity.
    /// </summary>
    public static class SelectionAnswerSetExtensions
    {
        /// <summary>
        /// Includes new or updates existed answer in answer set.
        /// </summary>
        public static void AddOrUpdateAnswerChoice(this SelectionAnswerSet answerSet, SelectionAnswerChoice answerChoice)
        {
            if (answerChoice == null)
            {
                throw new ArgumentNullException("answerChoice");
            }

            var answerChoiceEntity =
                answerSet.SelectionAnswerChoices.FirstOrDefault(
                    a => !answerChoice.Id.IsEmpty() && a.Id == answerChoice.Id);

            if (answerChoiceEntity == null)
            {
                answerSet.SelectionAnswerChoices.Add(answerChoice);
            }
            else
            {
                answerChoiceEntity.UpdateWith(answerChoice);
            }
        }

        /// <summary>
        /// Includes new localized string to answer choice of answer set or updates existed.
        /// </summary>
        /// <param name="answerSet"></param>
        /// <param name="answerChoiceId"></param>
        /// <param name="answerChoiceString"></param>
        public static bool AddOrUpdateLocalizedAnswerString(this SelectionAnswerSet answerSet, Guid answerChoiceId,
            SelectionAnswerChoiceString answerChoiceString)
        {
            if (answerChoiceString == null)
            {
                throw new ArgumentNullException("answerChoiceString");
            }

            var answerChoiceEntity = answerSet.SelectionAnswerChoices.FirstOrDefault(a => a.Id == answerChoiceId);

            if (answerChoiceEntity == null)
            {
                return false;
            }

            answerChoiceEntity.AddOrUpdateLocalizedString(answerChoiceString);

            return true;
        }
    }
}
