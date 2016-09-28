namespace CustomerService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Site_AddField_Subdomain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "Subdomain", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "Subdomain");
        }
    }
}
