using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.Web.Api.Extensions
{
    /// <summary>
    /// Contains extension methods for localized strings.
    /// </summary>
    public static class LocalizedStringExtensions
    {
        /// <summary>
        /// Returns localized string for language specified in request or default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="localizedStrings"></param>
        /// <returns></returns>
        public static T GetForRequestedLanguage<T>(this IEnumerable<T> localizedStrings) where T : LocalizedString
        {
            if (!string.IsNullOrEmpty(CareElementRequestContext.Current.Language))
            {
                return localizedStrings.SingleOrDefault(s => s.Language == CareElementRequestContext.Current.Language);
            }

            return localizedStrings.SingleOrDefault(s => s.Language == CareElementRequestContext.Current.DefaultLanguage);
        }
    }
}