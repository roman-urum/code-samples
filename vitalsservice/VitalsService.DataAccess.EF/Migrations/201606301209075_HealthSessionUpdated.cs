using System.Data.Entity.Migrations;

namespace VitalsService.DataAccess.EF.Migrations
{
    public partial class HealthSessionUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HealthSessions", "ProtocolId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HealthSessions", "ProtocolId", c => c.Guid(nullable: false));
        }
    }
}