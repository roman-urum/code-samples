namespace CustomerService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCreatorId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "CreatorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "CreatorId", c => c.String());
        }
    }
}
