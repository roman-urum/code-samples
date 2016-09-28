namespace CustomerService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Site_AddedFields1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "ContactPhone", c => c.String());
            DropColumn("dbo.Customers", "ContactPhone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "ContactPhone", c => c.String());
            DropColumn("dbo.Sites", "ContactPhone");
        }
    }
}
