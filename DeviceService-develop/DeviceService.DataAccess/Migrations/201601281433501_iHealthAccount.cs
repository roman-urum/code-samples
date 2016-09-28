namespace DeviceService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iHealthAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "Settings_iHealthAccount", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "Settings_iHealthAccount");
        }
    }
}
