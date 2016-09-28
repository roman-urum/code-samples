namespace VitalsService.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Conditions_Remove_Unnique_Name_Index : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Conditions", "IX_NAME");
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Conditions", "Name", unique: true, name: "IX_NAME");
        }
    }
}
