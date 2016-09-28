using HealthLibrary.Domain.Entities;

namespace HealthLibrary.DomainLogic.Extensions
{
    /// <summary>
    /// Extension methods for IAnalyticsEntity instances.
    /// </summary>
    public static class AnalyticsEntityExtensions
    {
        /// <summary>
        /// Sets internal values to null.
        /// </summary>
        /// <param name="analyticsEntity"></param>
        public static void ResetInternalValues(this IAnalyticsEntity analyticsEntity)
        {
            analyticsEntity.InternalId = null;
            analyticsEntity.InternalScore = null;
        }

        /// <summary>
        /// Sets internal values to null.
        /// </summary>
        /// <param name="analyticsEntity"></param>
        public static void ResetInternalValues(this IAnalyticsEntity analyticsEntity, IAnalyticsEntity existedValues)
        {
            if (existedValues == null)
            {
                analyticsEntity.ResetInternalValues();

                return;
            }

            analyticsEntity.InternalId = existedValues.InternalId;
            analyticsEntity.InternalScore = existedValues.InternalScore;
        }
    }
}
