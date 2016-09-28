namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDefaultStringInElements : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TextMediaElements", "DefaultStringId", "dbo.TextMediaElementStrings");
            DropIndex("dbo.TextMediaElements", new[] { "DefaultStringId" });
            DropColumn("dbo.TextMediaElements", "DefaultStringId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TextMediaElements", "DefaultStringId", c => c.Guid(nullable: false));
            CreateIndex("dbo.TextMediaElements", "DefaultStringId");
            AddForeignKey("dbo.TextMediaElements", "DefaultStringId", "dbo.TextMediaElementStrings", "Id");
        }
    }
}
