namespace DeviceService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PedometerSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "Settings_IsPedometerAutomated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "Settings_IsPedometerManual", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "Settings_IsPedometerManual");
            DropColumn("dbo.Devices", "Settings_IsPedometerAutomated");
        }
    }
}
