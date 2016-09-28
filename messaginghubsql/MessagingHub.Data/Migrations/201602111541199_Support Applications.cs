namespace MessagingHub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupportApplications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Platform = c.String(nullable: false, maxLength: 100),
                        NotificationType = c.Int(nullable: false),
                        AppleCertificateBase64 = c.String(),
                        AppleCertificatePassword = c.String(maxLength: 255),
                        GoogleCloudMessagingKey = c.String(maxLength: 255),
                        NotificationUrl = c.String(maxLength: 1024),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Name, t.Platform }, unique: true, name: "UX_Application");
            
            AddColumn("dbo.Registrations", "ApplicationId", c => c.Guid());
            AddColumn("dbo.Registrations", "Client", c => c.String(maxLength: 256));
            CreateIndex("dbo.Registrations", "ApplicationId");
            AddForeignKey("dbo.Registrations", "ApplicationId", "dbo.Applications", "Id");
            DropColumn("dbo.Registrations", "Application");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Registrations", "Application", c => c.String(maxLength: 100));
            DropForeignKey("dbo.Registrations", "ApplicationId", "dbo.Applications");
            DropIndex("dbo.Registrations", new[] { "ApplicationId" });
            DropIndex("dbo.Applications", "UX_Application");
            DropColumn("dbo.Registrations", "Client");
            DropColumn("dbo.Registrations", "ApplicationId");
            DropTable("dbo.Applications");
        }
    }
}
