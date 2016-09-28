namespace CustomerService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customer_AddedField_ContactPhone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ContactPhone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "ContactPhone");
        }
    }
}
