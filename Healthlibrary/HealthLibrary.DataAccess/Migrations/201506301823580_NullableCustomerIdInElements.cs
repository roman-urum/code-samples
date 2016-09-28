namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableCustomerIdInElements : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Elements", "CustomerId");
            AddColumn("dbo.Elements", "CustomerId", c => c.Int());
            AlterColumn("dbo.TextMediaElements", "MediaType", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TextMediaElements", "MediaType", c => c.Int(nullable: false));
            DropColumn("dbo.Elements", "CustomerId");
        }
    }
}
