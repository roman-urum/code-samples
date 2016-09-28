using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    public partial class MeasurementsUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MeasurementElements", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MeasurementElements", "Name", c => c.String());
        }
    }
}