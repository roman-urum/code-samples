namespace DeviceService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeDeviceIdTypeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Devices", "DeviceIdType", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Devices", "DeviceIdType", c => c.Int(nullable: false));
        }
    }
}
