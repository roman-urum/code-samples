namespace VitalsService.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Replace_relation_NoteVital_with_relation_NoteMeasurement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notes", "VitalId", "dbo.Vitals");
            DropIndex("dbo.Notes", new[] { "VitalId" });
            AddColumn("dbo.Notes", "MeasurementId", c => c.Guid());
            CreateIndex("dbo.Notes", "MeasurementId");
            DropColumn("dbo.Notes", "VitalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notes", "VitalId", c => c.Guid());
            DropIndex("dbo.Notes", new[] { "MeasurementId" });
            DropColumn("dbo.Notes", "MeasurementId");
            CreateIndex("dbo.Notes", "VitalId");
            AddForeignKey("dbo.Notes", "VitalId", "dbo.Vitals", "Id", cascadeDelete: true);
        }
    }
}
