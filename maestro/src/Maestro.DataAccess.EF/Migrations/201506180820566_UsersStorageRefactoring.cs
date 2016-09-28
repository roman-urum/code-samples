using System.Data.Entity.Migrations;

namespace Maestro.DataAccess.EF.Migrations
{
    /// <summary>
    /// UsersStorageRefactoring.
    /// </summary>
    public partial class UsersStorageRefactoring : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            RenameTable(name: "dbo.CustomerRoles", newName: "CustomerUserRoles");
            RenameTable(name: "dbo.CustomerRolesPermissionsMappings", newName: "CustomerUserRolesPermissionsMappings");
            DropForeignKey("dbo.UserSites", "UserId", "dbo.Users");
            DropIndex("dbo.UserSites", new[] { "UserId" });
            RenameColumn(table: "dbo.CustomerUsers", name: "CustomerRoleId", newName: "CustomerUserRoleId");
            RenameColumn(table: "dbo.CustomerUserRolesPermissionsMappings", name: "CustomerRoleId", newName: "CustomerUserRoleId");
            RenameIndex(table: "dbo.CustomerUsers", name: "IX_CustomerRoleId", newName: "IX_CustomerUserRoleId");
            RenameIndex(table: "dbo.CustomerUserRolesPermissionsMappings", name: "IX_CustomerRoleId", newName: "IX_CustomerUserRoleId");
            CreateTable(
                "dbo.CustomerUserSites",
                c => new
                    {
                        CustomerUserId = c.Guid(nullable: false),
                        SiteId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerUserId, t.SiteId })
                .ForeignKey("dbo.CustomerUsers", t => t.CustomerUserId, cascadeDelete: true)
                .Index(t => t.CustomerUserId);
            
            DropTable("dbo.UserSites");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            CreateTable(
                "dbo.UserSites",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        SiteId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.SiteId });
            
            DropForeignKey("dbo.CustomerUserSites", "CustomerUserId", "dbo.CustomerUsers");
            DropIndex("dbo.CustomerUserSites", new[] { "CustomerUserId" });
            DropTable("dbo.CustomerUserSites");
            RenameIndex(table: "dbo.CustomerUserRolesPermissionsMappings", name: "IX_CustomerUserRoleId", newName: "IX_CustomerRoleId");
            RenameIndex(table: "dbo.CustomerUsers", name: "IX_CustomerUserRoleId", newName: "IX_CustomerRoleId");
            RenameColumn(table: "dbo.CustomerUserRolesPermissionsMappings", name: "CustomerUserRoleId", newName: "CustomerRoleId");
            RenameColumn(table: "dbo.CustomerUsers", name: "CustomerUserRoleId", newName: "CustomerRoleId");
            CreateIndex("dbo.UserSites", "UserId");
            AddForeignKey("dbo.UserSites", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.CustomerUserRolesPermissionsMappings", newName: "CustomerRolesPermissionsMappings");
            RenameTable(name: "dbo.CustomerUserRoles", newName: "CustomerRoles");
        }
    }
}