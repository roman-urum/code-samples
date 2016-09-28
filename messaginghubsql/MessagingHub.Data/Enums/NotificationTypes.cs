namespace MessagingHub.Data.Enums
{
    public enum NotificationTypes
    {
        /// <summary>
        /// Don't notify anyone.
        /// </summary>
        None = 0,

        /// <summary>
        /// Notify via APNs using the specified Apple Certificate / Password.
        /// </summary>
        ApplePushNotificationService = 1,

        /// <summary>
        /// Notify via GCM using the specified key.
        /// </summary>
        GoogleCloudMessaging = 2,

        /// <summary>
        /// Notify via an HTTP POST to the specified Notification URL.
        /// </summary>
        NotifyUrl = 3
    }
}