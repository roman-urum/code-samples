namespace DeviceService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceType : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.Devices SET DeviceType = 'Other'");
            AlterColumn("dbo.Devices", "DeviceType", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Devices", "DeviceType", c => c.String());
        }
    }
}
