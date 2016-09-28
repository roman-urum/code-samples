namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssessmentElements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssessmentElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AssessmentType = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Elements", t => t.Id)
                .Index(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssessmentElements", "Id", "dbo.Elements");
            DropIndex("dbo.AssessmentElements", new[] { "Id" });
            DropTable("dbo.AssessmentElements");
        }
    }
}
