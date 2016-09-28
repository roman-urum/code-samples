using System;
using System.Data.Entity;
using System.Web.Http;
using MessagingHub.Data;
using MessagingHub.Data.Migrations;
using MessagingHub.Web.Application;

namespace MessagingHub.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public readonly static DateTime ApplicationStartedUtc = DateTime.UtcNow;

        public readonly static MobilePusher MobilePusher = new MobilePusher();

        protected void Application_Start()
        {
            AutoMapperConfig.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var context = new MessagingHubDbContext())
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<MessagingHubDbContext, Configuration>());
                context.Database.Initialize(true);
            }
        }
    }}