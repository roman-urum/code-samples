namespace VitalsService.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unique_Condition_Name : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Conditions", "Name", unique: true, name: "IX_NAME");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Conditions", "IX_NAME");
        }
    }
}
