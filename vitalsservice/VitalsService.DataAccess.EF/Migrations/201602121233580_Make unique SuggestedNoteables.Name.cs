namespace VitalsService.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeuniqueSuggestedNoteablesName : DbMigration
    {
        public override void Up()
        {
            Sql("delete from dbo.SuggestedNotables");
            CreateIndex("dbo.SuggestedNotables", "Name", unique: true, name: "IX_NAME");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SuggestedNotables", "IX_NAME");
        }
    }
}
