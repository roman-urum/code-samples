using System.Data.Entity.Migrations;

namespace VitalsService.DataAccess.EF.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlertDetails",
                c => new
                    {
                        AlertId = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlertId)
                .ForeignKey("dbo.Alerts", t => t.AlertId, cascadeDelete: true)
                .Index(t => t.AlertId);
            
            CreateTable(
                "dbo.Alerts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PatientId = c.Guid(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Type = c.Int(),
                        Title = c.String(nullable: false, maxLength: 250),
                        Body = c.String(),
                        Acknowledged = c.Boolean(nullable: false),
                        AcknowledgedUtc = c.DateTime(),
                        AcknowledgedBy = c.Guid(),
                        OccurredUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(),
                        Weight = c.Int(nullable: false),
                        HealthSessionId = c.Guid(),
                        AlertSeverityId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlertSeverities", t => t.AlertSeverityId)
                .ForeignKey("dbo.HealthSessions", t => t.HealthSessionId, cascadeDelete: true)
                .Index(t => t.HealthSessionId)
                .Index(t => t.AlertSeverityId);
            
            CreateTable(
                "dbo.AlertSeverities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Name = c.String(),
                        ColorCode = c.String(),
                        Severity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Thresholds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Type = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 100),
                        Unit = c.String(nullable: false, maxLength: 50),
                        MinValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AlertSeverityId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlertSeverities", t => t.AlertSeverityId, cascadeDelete: true)
                .Index(t => t.AlertSeverityId);
            
            CreateTable(
                "dbo.Vitals",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MeasurementId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Unit = c.String(maxLength: 50),
                        Alert_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Measurements", t => t.MeasurementId, cascadeDelete: true)
                .ForeignKey("dbo.Alerts", t => t.Alert_Id)
                .Index(t => t.MeasurementId)
                .Index(t => t.Alert_Id);
            
            CreateTable(
                "dbo.Measurements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PatientId = c.Guid(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        UpdatedUtc = c.DateTime(nullable: false),
                        ObservedUtc = c.DateTime(nullable: false),
                        IsInvalidated = c.Boolean(nullable: false),
                        IsAutomated = c.Boolean(nullable: false),
                        ProcessingType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        MeasurementId = c.Guid(nullable: false),
                        UniqueIdentifier = c.String(maxLength: 50),
                        BatteryPercent = c.Decimal(precision: 18, scale: 2),
                        BatteryMillivolts = c.Int(),
                        Model = c.String(maxLength: 100),
                        Version = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.MeasurementId)
                .ForeignKey("dbo.Measurements", t => t.MeasurementId, cascadeDelete: true)
                .Index(t => t.MeasurementId);
            
            CreateTable(
                "dbo.HealthSessionElementValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HealthSessionElementId = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HealthSessionElements", t => t.HealthSessionElementId, cascadeDelete: true)
                .Index(t => t.HealthSessionElementId);
            
            CreateTable(
                "dbo.HealthSessionElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HealthSessionId = c.Guid(nullable: false),
                        AnsweredUtc = c.DateTime(),
                        ElementId = c.Guid(nullable: false),
                        Text = c.String(maxLength: 1000),
                        Type = c.Int(nullable: false),
                        InternalId = c.String(maxLength: 100),
                        ExternalId = c.String(maxLength: 100),
                        AlertId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alerts", t => t.AlertId)
                .ForeignKey("dbo.HealthSessions", t => t.HealthSessionId, cascadeDelete: true)
                .Index(t => t.HealthSessionId)
                .Index(t => t.AlertId);
            
            CreateTable(
                "dbo.HealthSessions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        PatientId = c.Guid(nullable: false),
                        ProtocolId = c.Guid(nullable: false),
                        ProtocolName = c.String(maxLength: 1000),
                        IsPrivate = c.Boolean(nullable: false),
                        IsIncomplete = c.Boolean(nullable: false),
                        ProcessingType = c.Int(nullable: false),
                        ScheduledUtc = c.DateTime(nullable: false),
                        CalendarItemId = c.Guid(),
                        StartedUtc = c.DateTime(nullable: false),
                        CompletedUtc = c.DateTime(nullable: false),
                        SubmittedUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        PatientId = c.Guid(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 100),
                        Text = c.String(nullable: false, maxLength: 1000),
                        CreatedUtc = c.DateTime(nullable: false),
                        UpdatedUtc = c.DateTime(nullable: false),
                        VitalId = c.Guid(),
                        HealthSessionElementId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HealthSessionElements", t => t.HealthSessionElementId, cascadeDelete: true)
                .ForeignKey("dbo.Vitals", t => t.VitalId, cascadeDelete: true)
                .Index(t => t.VitalId)
                .Index(t => t.HealthSessionElementId);
            
            CreateTable(
                "dbo.NoteNotables",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        NoteId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notes", t => t.NoteId, cascadeDelete: true)
                .Index(t => t.NoteId);
            
            CreateTable(
                "dbo.MeasurementNotes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 250),
                        Text = c.String(maxLength: 1000),
                        MeasurementId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Measurements", t => t.MeasurementId, cascadeDelete: true)
                .Index(t => t.MeasurementId);
            
            CreateTable(
                "dbo.SuggestedNotables",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        CustomerId = c.Int(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        UpdatedUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HealthSessionElementAlertDetails",
                c => new
                    {
                        AlertId = c.Guid(nullable: false),
                        HealthSessionElement_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.AlertId)
                .ForeignKey("dbo.AlertDetails", t => t.AlertId)
                .ForeignKey("dbo.HealthSessionElements", t => t.HealthSessionElement_Id, cascadeDelete: true)
                .Index(t => t.AlertId)
                .Index(t => t.HealthSessionElement_Id);
            
            CreateTable(
                "dbo.VitalAlertDetails",
                c => new
                    {
                        AlertId = c.Guid(nullable: false),
                        VitalId = c.Guid(nullable: false),
                        ThresholdId = c.Guid(),
                    })
                .PrimaryKey(t => t.AlertId)
                .ForeignKey("dbo.AlertDetails", t => t.AlertId)
                .ForeignKey("dbo.Vitals", t => t.VitalId, cascadeDelete: true)
                .ForeignKey("dbo.Thresholds", t => t.ThresholdId, cascadeDelete: true)
                .Index(t => t.AlertId)
                .Index(t => t.VitalId)
                .Index(t => t.ThresholdId);
            
            CreateTable(
                "dbo.DefaultThresholds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DefaultType = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Thresholds", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.PatientThresholds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Thresholds", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.FreeFormAnswers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.String(),
                        InternalId = c.String(maxLength: 100),
                        ExternalId = c.String(maxLength: 100),
                        ExternalScore = c.Int(),
                        InternalScore = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HealthSessionElementValues", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.MeasurementValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MeasurementId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HealthSessionElementValues", t => t.Id)
                .ForeignKey("dbo.Measurements", t => t.MeasurementId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.MeasurementId);
            
            CreateTable(
                "dbo.ScaleAnswers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.Int(nullable: false),
                        InternalId = c.String(maxLength: 100),
                        ExternalId = c.String(maxLength: 100),
                        ExternalScore = c.Int(),
                        InternalScore = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HealthSessionElementValues", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SelectionAnswers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(maxLength: 1000),
                        Value = c.Guid(nullable: false),
                        InternalId = c.String(maxLength: 100),
                        ExternalId = c.String(maxLength: 100),
                        ExternalScore = c.Int(),
                        InternalScore = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HealthSessionElementValues", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SelectionAnswers", "Id", "dbo.HealthSessionElementValues");
            DropForeignKey("dbo.ScaleAnswers", "Id", "dbo.HealthSessionElementValues");
            DropForeignKey("dbo.MeasurementValues", "MeasurementId", "dbo.Measurements");
            DropForeignKey("dbo.MeasurementValues", "Id", "dbo.HealthSessionElementValues");
            DropForeignKey("dbo.FreeFormAnswers", "Id", "dbo.HealthSessionElementValues");
            DropForeignKey("dbo.PatientThresholds", "Id", "dbo.Thresholds");
            DropForeignKey("dbo.DefaultThresholds", "Id", "dbo.Thresholds");
            DropForeignKey("dbo.VitalAlertDetails", "ThresholdId", "dbo.Thresholds");
            DropForeignKey("dbo.VitalAlertDetails", "VitalId", "dbo.Vitals");
            DropForeignKey("dbo.VitalAlertDetails", "AlertId", "dbo.AlertDetails");
            DropForeignKey("dbo.HealthSessionElementAlertDetails", "HealthSessionElement_Id", "dbo.HealthSessionElements");
            DropForeignKey("dbo.HealthSessionElementAlertDetails", "AlertId", "dbo.AlertDetails");
            DropForeignKey("dbo.Vitals", "Alert_Id", "dbo.Alerts");
            DropForeignKey("dbo.Alerts", "HealthSessionId", "dbo.HealthSessions");
            DropForeignKey("dbo.AlertDetails", "AlertId", "dbo.Alerts");
            DropForeignKey("dbo.Vitals", "MeasurementId", "dbo.Measurements");
            DropForeignKey("dbo.MeasurementNotes", "MeasurementId", "dbo.Measurements");
            DropForeignKey("dbo.HealthSessionElementValues", "HealthSessionElementId", "dbo.HealthSessionElements");
            DropForeignKey("dbo.Notes", "VitalId", "dbo.Vitals");
            DropForeignKey("dbo.NoteNotables", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.Notes", "HealthSessionElementId", "dbo.HealthSessionElements");
            DropForeignKey("dbo.HealthSessionElements", "HealthSessionId", "dbo.HealthSessions");
            DropForeignKey("dbo.HealthSessionElements", "AlertId", "dbo.Alerts");
            DropForeignKey("dbo.Devices", "MeasurementId", "dbo.Measurements");
            DropForeignKey("dbo.Thresholds", "AlertSeverityId", "dbo.AlertSeverities");
            DropForeignKey("dbo.Alerts", "AlertSeverityId", "dbo.AlertSeverities");
            DropIndex("dbo.SelectionAnswers", new[] { "Id" });
            DropIndex("dbo.ScaleAnswers", new[] { "Id" });
            DropIndex("dbo.MeasurementValues", new[] { "MeasurementId" });
            DropIndex("dbo.MeasurementValues", new[] { "Id" });
            DropIndex("dbo.FreeFormAnswers", new[] { "Id" });
            DropIndex("dbo.PatientThresholds", new[] { "Id" });
            DropIndex("dbo.DefaultThresholds", new[] { "Id" });
            DropIndex("dbo.VitalAlertDetails", new[] { "ThresholdId" });
            DropIndex("dbo.VitalAlertDetails", new[] { "VitalId" });
            DropIndex("dbo.VitalAlertDetails", new[] { "AlertId" });
            DropIndex("dbo.HealthSessionElementAlertDetails", new[] { "HealthSessionElement_Id" });
            DropIndex("dbo.HealthSessionElementAlertDetails", new[] { "AlertId" });
            DropIndex("dbo.MeasurementNotes", new[] { "MeasurementId" });
            DropIndex("dbo.NoteNotables", new[] { "NoteId" });
            DropIndex("dbo.Notes", new[] { "HealthSessionElementId" });
            DropIndex("dbo.Notes", new[] { "VitalId" });
            DropIndex("dbo.HealthSessionElements", new[] { "AlertId" });
            DropIndex("dbo.HealthSessionElements", new[] { "HealthSessionId" });
            DropIndex("dbo.HealthSessionElementValues", new[] { "HealthSessionElementId" });
            DropIndex("dbo.Devices", new[] { "MeasurementId" });
            DropIndex("dbo.Vitals", new[] { "Alert_Id" });
            DropIndex("dbo.Vitals", new[] { "MeasurementId" });
            DropIndex("dbo.Thresholds", new[] { "AlertSeverityId" });
            DropIndex("dbo.Alerts", new[] { "AlertSeverityId" });
            DropIndex("dbo.Alerts", new[] { "HealthSessionId" });
            DropIndex("dbo.AlertDetails", new[] { "AlertId" });
            DropTable("dbo.SelectionAnswers");
            DropTable("dbo.ScaleAnswers");
            DropTable("dbo.MeasurementValues");
            DropTable("dbo.FreeFormAnswers");
            DropTable("dbo.PatientThresholds");
            DropTable("dbo.DefaultThresholds");
            DropTable("dbo.VitalAlertDetails");
            DropTable("dbo.HealthSessionElementAlertDetails");
            DropTable("dbo.SuggestedNotables");
            DropTable("dbo.MeasurementNotes");
            DropTable("dbo.NoteNotables");
            DropTable("dbo.Notes");
            DropTable("dbo.HealthSessions");
            DropTable("dbo.HealthSessionElements");
            DropTable("dbo.HealthSessionElementValues");
            DropTable("dbo.Devices");
            DropTable("dbo.Measurements");
            DropTable("dbo.Vitals");
            DropTable("dbo.Thresholds");
            DropTable("dbo.AlertSeverities");
            DropTable("dbo.Alerts");
            DropTable("dbo.AlertDetails");
        }
    }
}