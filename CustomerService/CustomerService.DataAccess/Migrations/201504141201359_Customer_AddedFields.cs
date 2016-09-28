namespace CustomerService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customer_AddedFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "LogoPath", c => c.String());
            AddColumn("dbo.Customers", "PasswordExpirationDays", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "IddleSessionTimeout", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "IddleSessionTimeout");
            DropColumn("dbo.Customers", "PasswordExpirationDays");
            DropColumn("dbo.Customers", "LogoPath");
        }
    }
}
