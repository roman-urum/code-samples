namespace VitalsService.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssessmentValues : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssessmentValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AssessmentMediaId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HealthSessionElementValues", t => t.Id)
                .ForeignKey("dbo.AssessmentMedias", t => t.AssessmentMediaId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.AssessmentMediaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssessmentValues", "AssessmentMediaId", "dbo.AssessmentMedias");
            DropForeignKey("dbo.AssessmentValues", "Id", "dbo.HealthSessionElementValues");
            DropIndex("dbo.AssessmentValues", new[] { "AssessmentMediaId" });
            DropIndex("dbo.AssessmentValues", new[] { "Id" });
            DropTable("dbo.AssessmentValues");
        }
    }
}
