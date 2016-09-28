using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using MessagingHub.Data;
using MessagingHub.Data.Enums;
using NLog;
using PushSharp;
using PushSharp.Android;
using PushSharp.Apple;
using PushSharp.Core;

namespace MessagingHub.Web.Application
{
    public class MobilePusher
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static MobilePusher instance = null;
        private static MobilePusher Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MobilePusher();
                }

                return instance;
            }
        }

        private readonly PushBroker broker = new PushBroker();

        public MobilePusher()
        {
            logger.Log(NLog.LogLevel.Debug, "Initiating MobilePusher");
            broker.OnChannelCreated += Broker_OnChannelCreated;
            broker.OnChannelDestroyed += Broker_OnChannelDestroyed;
            broker.OnChannelException += Broker_OnChannelException;
            broker.OnDeviceSubscriptionChanged += Broker_OnDeviceSubscriptionChanged;
            broker.OnDeviceSubscriptionExpired += Broker_OnDeviceSubscriptionExpired;
            broker.OnNotificationFailed += Broker_OnNotificationFailed;
            broker.OnNotificationRequeue += Broker_OnNotificationRequeue;
            broker.OnNotificationSent += Broker_OnNotificationSent;
            broker.OnServiceException += Broker_OnServiceException;

            logger.Log(NLog.LogLevel.Debug, "Start registering applications");

            MessagingHubDbContext dbContext = new MessagingHubDbContext();

            foreach (var application in dbContext.Applications)
            {
                if (application.NotificationType == NotificationTypes.ApplePushNotificationService)
                {
                    TryRegisterApplePush(application);
                }

                if (application.NotificationType == NotificationTypes.GoogleCloudMessaging)
                {
                    TryRegisterGcmPush(application);
                }
            }

            logger.Log(NLog.LogLevel.Debug, "Registering applications finished");

            //logger.Log(NLog.LogLevel.Debug, "Call TryRegisterGcmPush");
            //TryRegisterGcmPush();

            //logger.Log(NLog.LogLevel.Debug, "Call TryRegisterApplePush");
            //TryRegisterApplePush();
        }

        private void TryRegisterApplePush(Data.Models.Application application)
        {
            try
            {
                if (application == null)
                {
                    logger.Log(
                        NLog.LogLevel.Debug,
                        "TryRegisterApplePush: Attempting to register ApplePushChannelSettings for null application name");
                    return;
                }

                logger.Log(
                    NLog.LogLevel.Debug,
                    "TryRegisterApplePush: Attempting to register ApplePushChannelSettings for application name {0}",
                    application.Name);

                if (string.IsNullOrEmpty(application.AppleCertificateBase64))
                {
                    logger.Log(
                        NLog.LogLevel.Debug,
                        "TryRegisterApplePush: Cannot register ApplePushChannelSettings for application name {0} because AppleCertificateBase64 is empty",
                        application.Name);
                    return;
                }

                if (string.IsNullOrEmpty(application.AppleCertificatePassword))
                {
                    logger.Log(
                        NLog.LogLevel.Debug,
                        "TryRegisterApplePush: Cannot register ApplePushChannelSettings for application name {0} because AppleCertificatePassword is empty",
                        application.Name);
                    return;
                }

                var settings = new ApplePushChannelSettings(
                    Convert.FromBase64String(application.AppleCertificateBase64),
                    application.AppleCertificatePassword);

                if (!string.IsNullOrEmpty(application.NotificationUrl) && application.NotificationPort.HasValue)
                {
                    settings.OverrideServer(application.NotificationUrl, application.NotificationPort.Value);
                    settings.OverrideFeedbackServer(application.NotificationUrl, application.NotificationPort.Value);
                }

                broker.RegisterAppleService(settings, application.Name);

                logger.Log(
                    NLog.LogLevel.Debug,
                    "TryRegisterApplePush: ApplePushChannelSettings for application name {0} was successfully registered",
                    application.Name);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "TryRegisterApplePush: An error occured when try to register Apple push service, application name = {0}", application == null ? "unknown" : application.Name);
            }
        }

        private void TryRegisterGcmPush(Data.Models.Application application)
        {
            try
            {
                if (application == null)
                {
                    logger.Log(
                        NLog.LogLevel.Debug,
                        "TryRegisterGcmPush: Attempting to register GcmPushChannelSettings for null application");
                    return;
                }

                if (string.IsNullOrWhiteSpace(application.GoogleCloudMessagingKey))
                {
                    logger.Log(
                        NLog.LogLevel.Debug,
                        "TryRegisterGcmPush: Attempting to register GcmPushChannelSettings for application name {0} with empty auth token",
                        application.Name);
                    return;
                }

                var settings = new GcmPushChannelSettings(application.GoogleCloudMessagingKey);

                if (!string.IsNullOrEmpty(application.NotificationUrl))
                {
                    settings.OverrideUrl(application.NotificationUrl);
                }

                broker.RegisterGcmService(settings, application.Name);
                logger.Log(
                    NLog.LogLevel.Debug,
                    "TryRegisterGcmPush: Successfully registered GcmPushChannelSettings for application {0}",
                    application.Name);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "TryRegisterGcmPush: An error occured when try to register google push service, application name = {0}", application == null ? "unknown" : application.Name);
            }
        }

        public void Queue<T>(T notification, string applicationName)
            where T : Notification
        {
            try
            {
                broker.QueueNotification(notification, applicationName);
            }
            catch (Exception e)
            {
                logger.Log(NLog.LogLevel.Error, e, "Exception queueing notification.");
            }
        }

        private void Broker_OnServiceException(object sender, Exception error)
        {
            logger.Log(NLog.LogLevel.Error, error, "OnServiceException");
        }

        private void Broker_OnNotificationSent(object sender, PushSharp.Core.INotification notification)
        {
            logger.Log(NLog.LogLevel.Info, "OnNotificationSent: type={0}, tag={1}", notification.GetType(), notification.Tag);
        }

        private void Broker_OnNotificationRequeue(object sender, PushSharp.Core.NotificationRequeueEventArgs e)
        {
            logger.Log(NLog.LogLevel.Error, e.RequeueCause, "OnNotificationRequeue: tag={0}", e.Notification.Tag);
        }

        private void Broker_OnNotificationFailed(object sender, PushSharp.Core.INotification notification, Exception error)
        {
            logger.Log(NLog.LogLevel.Error, error, "OnNotificationFailed: {0}", notification.GetType());
        }

        private void Broker_OnDeviceSubscriptionExpired(object sender, string expiredSubscriptionId, DateTime expirationDateUtc, PushSharp.Core.INotification notification)
        {
            logger.Log(NLog.LogLevel.Warn, "OnDeviceSubscriptionExpired: expiredSubscriptionId={0}, expirationDateUtc={1}, tag={2}", expiredSubscriptionId, expirationDateUtc, notification.Tag);
#warning TODO: Update token?
        }

        private void Broker_OnDeviceSubscriptionChanged(object sender, string oldSubscriptionId, string newSubscriptionId, PushSharp.Core.INotification notification)
        {
            logger.Log(NLog.LogLevel.Info, "OnDeviceSubscriptionChanged: old={0}, new={1}, tag={2}", oldSubscriptionId, newSubscriptionId, notification.Tag);

        }

        private void Broker_OnChannelException(object sender, PushSharp.Core.IPushChannel pushChannel, Exception error)
        {
            logger.Log(NLog.LogLevel.Error, error, "OnChannelException");
        }

        private void Broker_OnChannelDestroyed(object sender)
        {
            logger.Log(NLog.LogLevel.Debug, "OnChannelDestroyed");
        }

        private void Broker_OnChannelCreated(object sender, PushSharp.Core.IPushChannel pushChannel)
        {
            logger.Log(NLog.LogLevel.Info, "OnChannelCreated: {0}", pushChannel.GetType());
        }
    }
}