namespace DeviceService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastConnectedUtc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "LastConnectedUtc", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "LastConnectedUtc");
        }
    }
}
