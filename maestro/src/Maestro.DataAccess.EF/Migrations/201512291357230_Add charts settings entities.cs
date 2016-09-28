namespace Maestro.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addchartssettingsentities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChartsSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TrendSettingId = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrendsSettings", t => t.TrendSettingId, cascadeDelete: true)
                .Index(t => t.TrendSettingId);
            
            CreateTable(
                "dbo.TrendsSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        LastDays = c.Int(),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DisplayThresholdsSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ThresholdId = c.Guid(nullable: false),
                        VitalChartSettingId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VitalChartsSettings", t => t.VitalChartSettingId)
                .Index(t => t.VitalChartSettingId);
            
            CreateTable(
                "dbo.QuestionChartsSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChartsSettings", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.VitalChartsSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VitalName = c.Int(nullable: false),
                        ShowMin = c.Boolean(nullable: false),
                        ShowMax = c.Boolean(nullable: false),
                        ShowAverage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChartsSettings", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VitalChartsSettings", "Id", "dbo.ChartsSettings");
            DropForeignKey("dbo.QuestionChartsSettings", "Id", "dbo.ChartsSettings");
            DropForeignKey("dbo.DisplayThresholdsSettings", "VitalChartSettingId", "dbo.VitalChartsSettings");
            DropForeignKey("dbo.ChartsSettings", "TrendSettingId", "dbo.TrendsSettings");
            DropIndex("dbo.VitalChartsSettings", new[] { "Id" });
            DropIndex("dbo.QuestionChartsSettings", new[] { "Id" });
            DropIndex("dbo.DisplayThresholdsSettings", new[] { "VitalChartSettingId" });
            DropIndex("dbo.ChartsSettings", new[] { "TrendSettingId" });
            DropTable("dbo.VitalChartsSettings");
            DropTable("dbo.QuestionChartsSettings");
            DropTable("dbo.DisplayThresholdsSettings");
            DropTable("dbo.TrendsSettings");
            DropTable("dbo.ChartsSettings");
        }
    }
}
