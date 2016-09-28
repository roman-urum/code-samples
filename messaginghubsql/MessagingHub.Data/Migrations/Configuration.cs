using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using MessagingHub.Data.Enums;
using MessagingHub.Data.Models;

namespace MessagingHub.Data.Migrations
{
    /// <summary>
    /// Configuration.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigrationsConfiguration{MessagingHubDbContext}" />
    public sealed class Configuration : DbMigrationsConfiguration<MessagingHubDbContext>
    {
        private const string DefaultIOSApplicationName = "HH-Mobile-1-iOs";
        private const string DefaultAndroidApplicationName = "HH-Mobile-1-Android";

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// Runs after upgrading to the latest migration to allow seed data to be updated.
        /// </summary>
        /// <param name="context">Context to be used for updating seed data.</param>
        /// <remarks>
        /// Note that the database may already contain seed data when this method runs. This means that
        /// implementations of this method must check whether or not seed data is present and/or up-to-date
        /// and then only make changes if necessary and in a non-destructive way. The
        /// <see cref="M:System.Data.Entity.Migrations.DbSetMigrationsExtensions.AddOrUpdate``1(System.Data.Entity.IDbSet{``0},``0[])" />
        /// can be used to help with this, but for seeding large amounts of data it may be necessary to do less
        /// granular checks if performance is an issue.
        /// If the <see cref="T:System.Data.Entity.MigrateDatabaseToLatestVersion`2" /> database
        /// initializer is being used, then this method will be called each time that the initializer runs.
        /// If one of the <see cref="T:System.Data.Entity.DropCreateDatabaseAlways`1" />, <see cref="T:System.Data.Entity.DropCreateDatabaseIfModelChanges`1" />,
        /// or <see cref="T:System.Data.Entity.CreateDatabaseIfNotExists`1" /> initializers is being used, then this method will not be
        /// called and the Seed method defined in the initializer should be used instead.
        /// </remarks>
        protected override void Seed(MessagingHubDbContext context)
        {
            SeedApplications(context);

            UpdateRegistrationsWithoutApplicationWithDefault(context);
            
            context.SaveChanges();
        }

        private void SeedApplications(MessagingHubDbContext context)
        {

            var appMobile1IOs = new Application()
            {
                Name = DefaultIOSApplicationName,
                NotificationType = NotificationTypes.ApplePushNotificationService,
                NotificationUrl = "gateway.push.apple.com",
                NotificationPort = 2195,
                Platform = "iOS",
                AppleCertificatePassword = ConfigurationManager.AppSettings["AppleCertificatePasswordProd"],
                AppleCertificateBase64 = ConfigurationManager.AppSettings["AppleCertificateBase64Prod"]
            };

            var appMobile2IOs = new Application()
            {
                Name = "HH-Mobile-2-iOs",
                NotificationType = NotificationTypes.ApplePushNotificationService,
                NotificationUrl = "gateway.push.apple.com",
                NotificationPort = 2195,
                Platform = "iOS",
                AppleCertificatePassword = ConfigurationManager.AppSettings["AppleCertificatePasswordProd"],
                AppleCertificateBase64 = ConfigurationManager.AppSettings["AppleCertificateBase64Prod"]
            };

            var appMobile2IOsDebug = new Application()
            {
                Name = "HH-Mobile-2-iOs-Debug",
                NotificationType = NotificationTypes.ApplePushNotificationService,
                NotificationUrl = "gateway.sandbox.push.apple.com",
                NotificationPort = 2195,
                Platform = "iOS",
                AppleCertificatePassword = ConfigurationManager.AppSettings["AppleCertificatePasswordDebug"],
                AppleCertificateBase64 = ConfigurationManager.AppSettings["AppleCertificateBase64Debug"]
            };

            var appMobile1Android = new Application()
            {
                Name = DefaultAndroidApplicationName,
                NotificationType = NotificationTypes.GoogleCloudMessaging,
                Platform = "Android",
                GoogleCloudMessagingKey = ConfigurationManager.AppSettings["GoogleCloudMessagingKey"]
            };

            context.Applications.AddOrUpdate(
                a => a.Name,
                appMobile1IOs,
                appMobile2IOs,
                appMobile2IOsDebug,
                appMobile1Android
            );            
        }

        private void UpdateRegistrationsWithoutApplicationWithDefault(MessagingHubDbContext context)
        {
            var appMobile1IOs = context.Applications.FirstOrDefault(a => a.Name == DefaultIOSApplicationName);
            var appMobile1Android = context.Applications.FirstOrDefault(a => a.Name == DefaultAndroidApplicationName);

            foreach (var registration in context.Registrations.Where(r => !r.ApplicationId.HasValue).ToList())
            {
                if (registration.Type == RegistrationTypes.APN)
                {
                    registration.Application = appMobile1IOs;
                }

                if (registration.Type == RegistrationTypes.GCM)
                {
                    registration.Application = appMobile1Android;
                }
            }
        }
    }
}