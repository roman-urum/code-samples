namespace Maestro.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerUsers",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        State = c.String(maxLength: 100),
                        City = c.String(maxLength: 50),
                        Address1 = c.String(maxLength: 250),
                        Address2 = c.String(maxLength: 250),
                        Address3 = c.String(maxLength: 250),
                        ZipCode = c.String(maxLength: 10),
                        NPI = c.String(maxLength: 100),
                        CustomerUserId = c.String(maxLength: 100),
                        CustomerRoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.CustomerRoles", t => t.CustomerRoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.NPI)
                .Index(t => t.CustomerUserId)
                .Index(t => t.CustomerRoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TokenServiceUserId = c.String(maxLength: 100),
                        IsEnabled = c.Boolean(nullable: false),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 256),
                        Phone = c.String(maxLength: 20),
                        RoleId = c.Guid(nullable: false),
                        IsEmailVerified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserSites",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        SiteId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.SiteId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CustomerRolesPermissionsMappings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerRoleId = c.Guid(nullable: false),
                        PermissionCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerRoles", t => t.CustomerRoleId, cascadeDelete: true)
                .Index(t => t.CustomerRoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerRolesPermissionsMappings", "CustomerRoleId", "dbo.CustomerRoles");
            DropForeignKey("dbo.UserSites", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.CustomerUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.CustomerUsers", "CustomerRoleId", "dbo.CustomerRoles");
            DropIndex("dbo.CustomerRolesPermissionsMappings", new[] { "CustomerRoleId" });
            DropIndex("dbo.UserSites", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.CustomerUsers", new[] { "CustomerRoleId" });
            DropIndex("dbo.CustomerUsers", new[] { "CustomerUserId" });
            DropIndex("dbo.CustomerUsers", new[] { "NPI" });
            DropIndex("dbo.CustomerUsers", new[] { "UserId" });
            DropTable("dbo.CustomerRolesPermissionsMappings");
            DropTable("dbo.UserSites");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.CustomerUsers");
            DropTable("dbo.CustomerRoles");
        }
    }
}
