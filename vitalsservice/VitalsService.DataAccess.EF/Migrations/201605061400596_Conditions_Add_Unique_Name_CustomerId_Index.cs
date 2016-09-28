namespace VitalsService.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Conditions_Add_Unique_Name_CustomerId_Index : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Conditions", new[] { "Name", "CustomerId" }, unique: true, name: "IX_UQ_NAME_PER_CUSTOMER");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Conditions", "IX_UQ_NAME_PER_CUSTOMER");
        }
    }
}
