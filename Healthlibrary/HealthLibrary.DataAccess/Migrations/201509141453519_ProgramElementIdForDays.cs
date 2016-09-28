namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgramElementIdForDays : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM dbo.ProgramDayElements");
            AddColumn("dbo.ProgramDayElements", "ProgramElementId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ProgramDayElements", "ProgramElementId");
            AddForeignKey("dbo.ProgramDayElements", "ProgramElementId", "dbo.ProgramElements", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
        }
    }
}
