namespace CustomerService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Site_AddedFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "IsPublished", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sites", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.Customers", "IsPublished");
            DropColumn("dbo.Customers", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "IsPublished", c => c.Boolean(nullable: false));
            DropColumn("dbo.Sites", "IsActive");
            DropColumn("dbo.Sites", "IsPublished");
        }
    }
}
