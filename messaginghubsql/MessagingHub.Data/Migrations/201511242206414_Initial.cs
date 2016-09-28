namespace MessagingHub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                        Secret = c.String(maxLength: 100),
                        Type = c.Int(nullable: false),
                        Token = c.String(maxLength: 250),
                        Application = c.String(maxLength: 100),
                        Device = c.String(maxLength: 100),
                        Verified = c.Boolean(nullable: false),
                        VerificationCode = c.String(maxLength: 10),
                        Disabled = c.Boolean(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Type, t.Token }, unique: true, name: "UX_RegistrationTypeToken")
                .Index(t => t.Verified)
                .Index(t => t.Disabled);
            
            CreateTable(
                "dbo.RegistrationTags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RegistrationId = c.Guid(nullable: false),
                        Value = c.String(maxLength: 100),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Registrations", t => t.RegistrationId, cascadeDelete: true)
                .Index(t => new { t.RegistrationId, t.Value }, unique: true, name: "UX_RegistrationTagValues");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegistrationTags", "RegistrationId", "dbo.Registrations");
            DropIndex("dbo.RegistrationTags", "UX_RegistrationTagValues");
            DropIndex("dbo.Registrations", new[] { "Disabled" });
            DropIndex("dbo.Registrations", new[] { "Verified" });
            DropIndex("dbo.Registrations", "UX_RegistrationTypeToken");
            DropTable("dbo.RegistrationTags");
            DropTable("dbo.Registrations");
        }
    }
}
