namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameForMeasurementElements : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeasurementElements", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MeasurementElements", "Name");
        }
    }
}
