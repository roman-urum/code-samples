namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendLocalizedStringsDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LocalizedStrings", "Description", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LocalizedStrings", "Description", c => c.String(maxLength: 250));
        }
    }
}
