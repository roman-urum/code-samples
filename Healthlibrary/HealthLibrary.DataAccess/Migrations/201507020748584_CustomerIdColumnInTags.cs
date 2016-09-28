namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerIdColumnInTags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tags", "CustomerId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tags", "CustomerId");
        }
    }
}
