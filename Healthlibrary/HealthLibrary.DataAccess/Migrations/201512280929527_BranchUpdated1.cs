using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    public partial class BranchUpdated1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "ThresholdAlertSeverityId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "ThresholdAlertSeverityId");
        }
    }
}