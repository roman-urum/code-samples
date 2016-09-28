using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    /// <summary>
    /// ProtocolElementAlertsAdded.
    /// </summary>
    public partial class ProtocolElementAlertsAdded : DbMigration
    {
        /// <summary>
        /// Ups this instance.
        /// </summary>
        public override void Up()
        {
            DropIndex("dbo.Conditions", new[] { "BranchId" });
            CreateTable(
                "dbo.Alerts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProtocolElementId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProtocolElements", t => t.ProtocolElementId, cascadeDelete: true)
                .Index(t => t.ProtocolElementId);
            
            AddColumn("dbo.Conditions", "AlertId", c => c.Guid());
            AlterColumn("dbo.Conditions", "BranchId", c => c.Guid());
            CreateIndex("dbo.Conditions", "BranchId");
            CreateIndex("dbo.Conditions", "AlertId");
            AddForeignKey("dbo.Conditions", "AlertId", "dbo.Alerts", "Id");
        }

        /// <summary>
        /// Downs this instance.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Alerts", "ProtocolElementId", "dbo.ProtocolElements");
            DropForeignKey("dbo.Conditions", "AlertId", "dbo.Alerts");
            DropIndex("dbo.Conditions", new[] { "AlertId" });
            DropIndex("dbo.Conditions", new[] { "BranchId" });
            DropIndex("dbo.Alerts", new[] { "ProtocolElementId" });
            AlterColumn("dbo.Conditions", "BranchId", c => c.Guid(nullable: false));
            DropColumn("dbo.Conditions", "AlertId");
            DropTable("dbo.Alerts");
            CreateIndex("dbo.Conditions", "BranchId");
        }
    }
}