namespace DeviceService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeviceTypeAndDeviceIdType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "DeviceType", c => c.String());
            AddColumn("dbo.Devices", "DeviceIdType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "DeviceIdType");
            DropColumn("dbo.Devices", "DeviceType");
        }
    }
}
