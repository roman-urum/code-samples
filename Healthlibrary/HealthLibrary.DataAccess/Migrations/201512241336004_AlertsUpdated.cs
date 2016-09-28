using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    public partial class AlertsUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alerts", "AlertSeverityId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alerts", "AlertSeverityId");
        }
    }
}
