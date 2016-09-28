namespace Maestro.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSchemaUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerUserSites", "CustomerUserId", "dbo.CustomerUsers");
            DropForeignKey("dbo.CustomerUsers", "UserId", "dbo.Users");
            RenameColumn(table: "dbo.CustomerUsers", name: "UserId", newName: "Id");
            RenameIndex(table: "dbo.CustomerUsers", name: "IX_UserId", newName: "IX_Id");
            AddForeignKey("dbo.CustomerUserSites", "CustomerUserId", "dbo.CustomerUsers", "Id");
            AddForeignKey("dbo.CustomerUsers", "Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerUsers", "Id", "dbo.Users");
            DropForeignKey("dbo.CustomerUserSites", "CustomerUserId", "dbo.CustomerUsers");
            RenameIndex(table: "dbo.CustomerUsers", name: "IX_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.CustomerUsers", name: "Id", newName: "UserId");
            AddForeignKey("dbo.CustomerUsers", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerUserSites", "CustomerUserId", "dbo.CustomerUsers", "UserId", cascadeDelete: true);
        }
    }
}
