namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgramDayElementsMappingChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProgramDayElementsToProgramElements", "ProgramDayElementId", "dbo.ProgramDayElements");
            DropForeignKey("dbo.ProgramDayElementsToProgramElements", "ProgramElementId", "dbo.ProgramElements");
            DropIndex("dbo.ProgramDayElementsToProgramElements", new[] { "ProgramDayElementId" });
            DropIndex("dbo.ProgramDayElementsToProgramElements", new[] { "ProgramElementId" });
            DropTable("dbo.ProgramDayElementsToProgramElements");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProgramDayElementsToProgramElements",
                c => new
                    {
                        ProgramDayElementId = c.Guid(nullable: false),
                        ProgramElementId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProgramDayElementId, t.ProgramElementId });
            
            DropForeignKey("dbo.ProgramDayElements", "ProgramElementId", "dbo.ProgramElements");
            DropIndex("dbo.ProgramDayElements", new[] { "ProgramElementId" });
            DropColumn("dbo.ProgramDayElements", "ProgramElementId");
            CreateIndex("dbo.ProgramDayElementsToProgramElements", "ProgramElementId");
            CreateIndex("dbo.ProgramDayElementsToProgramElements", "ProgramDayElementId");
            AddForeignKey("dbo.ProgramDayElementsToProgramElements", "ProgramElementId", "dbo.ProgramElements", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProgramDayElementsToProgramElements", "ProgramDayElementId", "dbo.ProgramDayElements", "Id", cascadeDelete: true);
        }
    }
}
