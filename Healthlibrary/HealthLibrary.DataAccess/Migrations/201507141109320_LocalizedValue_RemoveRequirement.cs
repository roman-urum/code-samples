namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocalizedValue_RemoveRequirement : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LocalizedStrings", "Value", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LocalizedStrings", "Value", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
