namespace VitalsService.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoringAlerts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AlertDetails", "AlertId", "dbo.Alerts");
            DropForeignKey("dbo.Alerts", "HealthSessionId", "dbo.HealthSessions");
            DropForeignKey("dbo.HealthSessionElementAlertDetails", "AlertId", "dbo.AlertDetails");
            DropForeignKey("dbo.HealthSessionElementAlertDetails", "HealthSessionElement_Id", "dbo.HealthSessionElements");
            DropForeignKey("dbo.VitalAlertDetails", "AlertId", "dbo.AlertDetails");
            DropForeignKey("dbo.VitalAlertDetails", "VitalId", "dbo.Vitals");
            DropForeignKey("dbo.VitalAlertDetails", "ThresholdId", "dbo.Thresholds");
            DropForeignKey("dbo.Vitals", "Alert_Id", "dbo.Alerts");
            DropForeignKey("dbo.HealthSessionElements", "AlertId", "dbo.Alerts");
            DropIndex("dbo.AlertDetails", new[] { "AlertId" });
            DropIndex("dbo.Alerts", new[] { "HealthSessionId" });
            DropIndex("dbo.Vitals", new[] { "Alert_Id" });
            DropIndex("dbo.HealthSessionElements", new[] { "AlertId" });
            DropIndex("dbo.HealthSessionElementAlertDetails", new[] { "AlertId" });
            DropIndex("dbo.HealthSessionElementAlertDetails", new[] { "HealthSessionElement_Id" });
            DropIndex("dbo.VitalAlertDetails", new[] { "AlertId" });
            DropIndex("dbo.VitalAlertDetails", new[] { "VitalId" });
            DropIndex("dbo.VitalAlertDetails", new[] { "ThresholdId" });
            //RenameColumn(table: "dbo.VitalAlerts", name: "Alert_Id", newName: "Id");
            //RenameColumn(table: "dbo.HealthSessionElementAlerts", name: "AlertId", newName: "Id");
            CreateTable(
                "dbo.HealthSessionElementAlerts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alerts", t => t.Id)
                .ForeignKey("dbo.HealthSessionElements", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.VitalAlerts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ThresholdId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alerts", t => t.Id)
                .ForeignKey("dbo.Thresholds", t => t.ThresholdId, cascadeDelete: true)
                .ForeignKey("dbo.Vitals", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.ThresholdId);
            
            DropColumn("dbo.Vitals", "Alert_Id");
            DropColumn("dbo.HealthSessionElements", "AlertId");
            DropColumn("dbo.Alerts", "HealthSessionId");
            DropTable("dbo.AlertDetails");
            DropTable("dbo.HealthSessionElementAlertDetails");
            DropTable("dbo.VitalAlertDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VitalAlertDetails",
                c => new
                    {
                        AlertId = c.Guid(nullable: false),
                        VitalId = c.Guid(nullable: false),
                        ThresholdId = c.Guid(),
                    })
                .PrimaryKey(t => t.AlertId);
            
            CreateTable(
                "dbo.HealthSessionElementAlertDetails",
                c => new
                    {
                        AlertId = c.Guid(nullable: false),
                        HealthSessionElement_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.AlertId);
            
            CreateTable(
                "dbo.AlertDetails",
                c => new
                    {
                        AlertId = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlertId);
            
            AddColumn("dbo.Alerts", "HealthSessionId", c => c.Guid());
            AddColumn("dbo.HealthSessionElements", "AlertId", c => c.Guid());
            AddColumn("dbo.Vitals", "Alert_Id", c => c.Guid());
            DropForeignKey("dbo.HealthSessionElementAlerts", "Id", "dbo.HealthSessionElements");
            DropForeignKey("dbo.VitalAlerts", "Id", "dbo.Vitals");
            DropForeignKey("dbo.VitalAlerts", "ThresholdId", "dbo.Thresholds");
            DropForeignKey("dbo.VitalAlerts", "Id", "dbo.Alerts");
            DropForeignKey("dbo.HealthSessionElementAlerts", "Id", "dbo.Alerts");
            DropIndex("dbo.VitalAlerts", new[] { "ThresholdId" });
            DropIndex("dbo.VitalAlerts", new[] { "Id" });
            DropIndex("dbo.HealthSessionElementAlerts", new[] { "Id" });
            DropTable("dbo.VitalAlerts");
            DropTable("dbo.HealthSessionElementAlerts");
            //RenameColumn(table: "dbo.HealthSessionElementAlerts", name: "Id", newName: "AlertId");
            //RenameColumn(table: "dbo.VitalAlerts", name: "Id", newName: "Alert_Id");
            CreateIndex("dbo.VitalAlertDetails", "ThresholdId");
            CreateIndex("dbo.VitalAlertDetails", "VitalId");
            CreateIndex("dbo.VitalAlertDetails", "AlertId");
            CreateIndex("dbo.HealthSessionElementAlertDetails", "HealthSessionElement_Id");
            CreateIndex("dbo.HealthSessionElementAlertDetails", "AlertId");
            CreateIndex("dbo.HealthSessionElements", "AlertId");
            CreateIndex("dbo.Vitals", "Alert_Id");
            CreateIndex("dbo.Alerts", "HealthSessionId");
            CreateIndex("dbo.AlertDetails", "AlertId");
            AddForeignKey("dbo.HealthSessionElements", "AlertId", "dbo.Alerts", "Id");
            AddForeignKey("dbo.Vitals", "Alert_Id", "dbo.Alerts", "Id");
            AddForeignKey("dbo.VitalAlertDetails", "ThresholdId", "dbo.Thresholds", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VitalAlertDetails", "VitalId", "dbo.Vitals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VitalAlertDetails", "AlertId", "dbo.AlertDetails", "AlertId");
            AddForeignKey("dbo.HealthSessionElementAlertDetails", "HealthSessionElement_Id", "dbo.HealthSessionElements", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HealthSessionElementAlertDetails", "AlertId", "dbo.AlertDetails", "AlertId");
            AddForeignKey("dbo.Alerts", "HealthSessionId", "dbo.HealthSessions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AlertDetails", "AlertId", "dbo.Alerts", "Id", cascadeDelete: true);
        }
    }
}
