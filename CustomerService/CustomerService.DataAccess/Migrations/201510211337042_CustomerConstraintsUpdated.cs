using System.Data.Entity.Migrations;

namespace CustomerService.DataAccess.Migrations
{
    public partial class CustomerConstraintsUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "LogoPath", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "LogoPath", c => c.String(maxLength: 999));
        }
    }
}