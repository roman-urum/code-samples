using System;
using Maestro.Web.Resources;

namespace Maestro.Web.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotificationMessageAttribute : Attribute
    {
        public string ValidationMessage { get; private set; }

        public NotificationMessageAttribute(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
            {
                throw new ArgumentNullException("messageId");
            }

            ValidationMessage = GlobalStrings.ResourceManager.GetString(messageId);
        }
    }
}