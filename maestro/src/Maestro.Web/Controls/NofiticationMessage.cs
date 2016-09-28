using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Maestro.Web.DataAnnotations;

namespace Maestro.Web.Controls
{
    /// <summary>
    /// Contains extension methods to generate notification message for input.
    /// </summary>
    public static class NofiticationMessage
    {
        private const string NotificationMessageAttribute = "data-val-notification";

        /// <summary>
        /// Generates html for standard validation message and includes
        /// default message which will be displayed before validation applied.
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString NotificationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> property)
        {
            var message = GetNotificationMessage(property);

            var attributes = new Dictionary<string, object>
            {
                {NotificationMessageAttribute, message}
            };

            return htmlHelper.ValidationMessageFor(property, null, attributes);
        }

        private static string GetNotificationMessage<TModel, TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            var memberExpression = property.Body as MemberExpression;

            if (memberExpression == null)
            {
                throw new CustomAttributeFormatException("NotificationMessageAttribute not specified.");
            }

            var attribute = memberExpression.Member.GetCustomAttribute<NotificationMessageAttribute>();

            if (attribute == null)
            {
                throw new CustomAttributeFormatException("NotificationMessageAttribute not specified.");
            }

            return attribute.ValidationMessage;
        }
    }
}