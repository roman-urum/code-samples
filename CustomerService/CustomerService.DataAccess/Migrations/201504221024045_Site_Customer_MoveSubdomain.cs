namespace CustomerService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Site_Customer_MoveSubdomain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Subdomain", c => c.String());
            DropColumn("dbo.Sites", "Subdomain");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sites", "Subdomain", c => c.String());
            DropColumn("dbo.Customers", "Subdomain");
        }
    }
}
