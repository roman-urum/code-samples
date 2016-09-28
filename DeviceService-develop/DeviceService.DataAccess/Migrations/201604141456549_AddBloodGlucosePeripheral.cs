namespace DeviceService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBloodGlucosePeripheral : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "Settings_BloodGlucosePeripheral", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "Settings_BloodGlucosePeripheral");
        }
    }
}
